using P2000Maintenance.Models.P2000;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace P2000Maintenance.Models
{
    public class P2000ContextModules
    {

        private static short? NullableShort(object input)
        {
            if (Convert.IsDBNull(input))
            {
                return null;
            }
            else
            {
                return (short?)Convert.ToInt32(input);
            }
        }

        private static int? NullableInt(object input)
        {
            if (Convert.IsDBNull(input))
            {
                return null;
            }
            else
            {
                return (int?)Convert.ToInt32(input);
            }
        }

        private static PingReply PingHost(string hostname)
        {

            Ping p = new Ping();
            PingReply r;
            r = p.Send(hostname);

            if (r.Status == IPStatus.Success)
            {
                return r;
            }
            return null;
        }


        private static Context db = new Context();
        private static string connection_string = System.Configuration.ConfigurationManager.ConnectionStrings["Pegasys"].ConnectionString;


        public static void PopulateAllFromP2000()
        {
            PopulateInputsFromP2000();
            PopulateOutputsFromP2000();
            PopulateWorkstationsFromP2000();
        }
        public static void PopulatePartitionsFromP2000()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.partition";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var model = new Partition();
                                model.dbId = (int)reader["part_number"];
                                var dbEntry = db.Partitions.FirstOrDefault(Partition => Partition.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    model.Name = reader["part_name"].ToString().Trim();
                                    model.Site = reader["site"].ToString().Trim();
                                    model.Guid = reader["part_guid"].ToString().Trim();
                                    db.Partitions.Add(model);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
        }


        public static void PopulatePanelsFromP2000()
        {
            PopulatePartitionsFromP2000();
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.panel";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var model = new Panel();
                                model.dbId = (int)reader["p_panel_id"];
                                var dbEntry = db.Panels.FirstOrDefault(P2000Panel => P2000Panel.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    int partitionId = (int)reader["p_partition"];
                                    Partition partition = db.Partitions.FirstOrDefault(Partition => Partition.dbId == partitionId);
                                    if (dbEntry == null)
                                    {
                                        model.partition = partition;
                                    }

                                    model.Name = reader["p_unit_name"].ToString().Trim();
                                    model.Guid = reader["p_guid"].ToString().Trim();
                                    model.Ip_Address = reader["p_ip_pri"].ToString().Trim();
                                    model.Model = reader["p_hardware_model"].ToString().Trim();
                                    model.Version = reader["p_version_text"].ToString().Trim();
                                    System.Diagnostics.Debug.WriteLine("Not Found:" + model.ToString());
                                    db.Panels.Add(model);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
        }


        public static void PopulateTerminalsFromP2000()
        {
            PopulatePanelsFromP2000();
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.terminal left join Pegasys.dbo.terminal_ck on tp_id = tp_term_id";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var model = new Terminal();
                                model.dbId = (int)reader["tp_term_id"];
                                var dbEntry = db.Terminals.FirstOrDefault(Terminal => Terminal.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    int partitionId = (int)reader["tp_partition"];
                                    Partition partition = db.Partitions.FirstOrDefault(Partition => Partition.dbId == partitionId);
                                    if (dbEntry == null)
                                    {
                                        model.partition = partition;
                                    }

                                    int panelId = (int)reader["tp_panel_id"];
                                    System.Diagnostics.Debug.WriteLine(partitionId);
                                    Panel panel = db.Panels.FirstOrDefault(Panel => Panel.dbId == panelId);
                                    if (dbEntry == null)
                                    {
                                        model.Panel = panel;
                                    }

                                    model.Name = reader["tp_term_name"].ToString().Trim();
                                    model.Index = (short)reader["tp_term_index"];
                                    model.ReaderType = reader["tp_rdr_module_type"].ToString().Trim();
                                    model.ReaderEnabled = Convert.ToBoolean(reader["tp_reader_flag"]);
                                    if (model.ReaderEnabled)
                                    {
                                        Input doorContact = new Input();
                                        doorContact.Panel = model.Panel;
                                        doorContact.Number = 1;
                                        doorContact.Name = "Door Contact";
                                        model.inputs.Add(doorContact);

                                        Input rexButton = new Input();
                                        rexButton.Panel = model.Panel;
                                        rexButton.Number = 2;
                                        rexButton.Name = "REX Button";
                                        model.inputs.Add(rexButton);

                                        Input spareInput = new Input();
                                        spareInput.Panel = model.Panel;
                                        spareInput.Number = 3;
                                        spareInput.Name = "Spare Input";
                                        model.inputs.Add(spareInput);

                                        Input tamperInput = new Input();
                                        tamperInput.Panel = model.Panel;
                                        tamperInput.Number = 4;
                                        tamperInput.Name = "Tamper Input";
                                        model.inputs.Add(tamperInput);

                                        Output redLED = new Output();
                                        redLED.Panel = model.Panel;
                                        redLED.Number = 1;
                                        redLED.Name = "Red LED";
                                        model.outputs.Add(redLED);

                                        Output greenLED = new Output();
                                        greenLED.Panel = model.Panel;
                                        greenLED.Number = 2;
                                        greenLED.Name = "Green LED";
                                        model.outputs.Add(greenLED);

                                        Output lockOutput = new Output();
                                        lockOutput.Panel = model.Panel;
                                        lockOutput.Number = 5;
                                        lockOutput.Name = "Lock Output";
                                        model.outputs.Add(lockOutput);

                                        Output shuntOutput = new Output();
                                        shuntOutput.Panel = model.Panel;
                                        shuntOutput.Number = 3;
                                        shuntOutput.Name = "Shunt Output";
                                        model.outputs.Add(shuntOutput);
                                    }
                                    model.ReaderModule = NullableShort(reader["tp_rdr_module_addr"]);
                                    model.ReaderIndex = NullableShort(reader["tp_rdr_module_index"]);
                                    model.IOType = reader["tp_io_module_type"].ToString().Trim();
                                    model.InputEnabled = Convert.ToBoolean(reader["tp_input_flag"]);
                                    model.InputModule = NullableShort(reader["tp_io_module_addr"]);
                                    model.InputIndex = NullableShort(reader["tp_io_module_index"]);
                                    model.OutputEnabled = Convert.ToBoolean(reader["tp_output_flag"]);
                                    model.OutputModule = NullableShort(reader["tp_output_flag"]);
                                    model.OutputIndex = NullableShort(reader["tp_output_flag"]);
                                    model.Enabled = Convert.ToBoolean(reader["tp_enb_dis"]);
                                    model.Guid = reader["tp_guid"].ToString().Trim();
                                    System.Diagnostics.Debug.WriteLine("Not Found:" + model.ToString());
                                    db.Terminals.Add(model);
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("Found:" + dbEntry.ToString());
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
        }
        public static void PopulateInputsFromP2000()
        {
            PopulateTerminalsFromP2000();
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.input";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var model = new Input();
                                model.dbId = (int)reader["ip_point_id"];
                                var dbEntry = db.Inputs.FirstOrDefault(Input => Input.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    int partitionId = (int)reader["ip_partition"];
                                    var partition = db.Partitions.FirstOrDefault(Partition => Partition.dbId == partitionId);
                                    if (partition != null)
                                    {
                                        model.partition = partition;
                                    }
                                    else
                                    {
                                        model.partition = null;
                                    }

                                    int? terminalId = NullableInt(reader["ip_term_id"]);
                                    model.Terminal = null;
                                    if (terminalId != null)
                                    {
                                        var terminal = db.Terminals.FirstOrDefault(Terminal => Terminal.dbId == (int)terminalId);
                                        if (terminal != null)
                                        {
                                            model.Terminal = terminal;
                                        }
                                    }

                                    int? panelId = NullableInt(reader["ip_panel_id"]);
                                    if (panelId != null)
                                    {
                                        var panel = db.Panels.FirstOrDefault(Panel => Panel.dbId == (int)panelId);
                                        if (panel != null)
                                        {
                                            model.Panel = panel;
                                        }
                                    }
                                    else
                                    {
                                        if (model.Terminal != null)
                                        {
                                            model.Panel = model.Terminal.Panel;
                                        }
                                    }


                                    model.Number = (short)reader["ip_point_number"];
                                    model.Enabled = Convert.ToBoolean(reader["ip_enable_flag"]);
                                    model.SoftInput = Convert.ToBoolean(reader["ip_soft_input"]);

                                    model.Name = reader["ip_point_name"].ToString().Trim();
                                    model.Guid = reader["ip_guid"].ToString().Trim();
                                    System.Diagnostics.Debug.WriteLine("Not Found:" + model.ToString());
                                    db.Inputs.Add(model);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
        }


        public static void PopulateOutputsFromP2000()
        {
            PopulateTerminalsFromP2000();
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.output";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var model = new Output();
                                model.dbId = (int)reader["op_output_id"];
                                var dbEntry = db.Outputs.FirstOrDefault(Output => Output.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    int partitionId = (int)reader["op_partition"];
                                    var partition = db.Partitions.FirstOrDefault(Partition => Partition.dbId == partitionId);
                                    if (partition != null)
                                    {
                                        model.partition = partition;
                                    }
                                    else
                                    {
                                        model.partition = null;
                                    }

                                    int? terminalId = NullableInt(reader["op_term_id"]);
                                    model.Terminal = null;
                                    if (terminalId != null)
                                    {
                                        var terminal = db.Terminals.FirstOrDefault(Terminal => Terminal.dbId == (int)terminalId);
                                        if (terminal != null)
                                        {
                                            model.Terminal = terminal;
                                            model.Panel = model.Terminal.Panel;
                                        }
                                    }



                                    model.Number = (short)reader["op_point_number"];
                                    model.Enabled = Convert.ToBoolean(reader["op_enable"]);
                                    model.Name = reader["op_point_name"].ToString().Trim();
                                    model.Guid = reader["op_guid"].ToString().Trim();
                                    db.Outputs.Add(model);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
        }


        public static void PopulateWorkstationsFromP2000()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    connection.Open();
                    string query_string = "select * from Pegasys.dbo.station left join Pegasys.dbo.station_stat on sstat_station_id = stn_id";
                    using (SqlCommand command = new SqlCommand(query_string, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var model = new Workstation();
                                model.dbId = (int)reader["stn_id"];
                                var dbEntry = db.Workstations.FirstOrDefault(Workstation => Workstation.dbId == model.dbId);

                                if (dbEntry == null)
                                {
                                    int partitionId = (int)reader["stn_partition"];
                                    Partition partition = db.Partitions.FirstOrDefault(Partition => Partition.dbId == partitionId);
                                    if (dbEntry == null)
                                    {
                                        model.partition = partition;
                                    }
                                    System.Diagnostics.Debug.WriteLine("Found:" + reader["stn_enable"].ToString() + "|" + reader["stn_name"].ToString().Trim());

                                    model.Enabled = Convert.ToBoolean(reader["stn_enable"]);
                                    model.Badging = Convert.ToBoolean(reader["stn_badge_ws"]);
                                    model.Server = Convert.ToBoolean(reader["stn_server"]);
                                    model.Version = reader["stn_major_version"].ToString().Trim() + ".";
                                    model.Version += reader["stn_minor_version"].ToString().Trim() + ".";
                                    model.Version += reader["stn_build_version"].ToString().Trim();
                                    model.Name = reader["stn_name"].ToString().Trim();
                                    model.LoggedOnUser = reader["sstat_username"].ToString().Trim();
                                    if (model.LoggedOnUser.Length == 0)
                                    {
                                        model.LoggedIn = false;
                                    }
                                    else
                                    {
                                        model.LoggedIn = true;
                                    }
                                    model.LoggedOnUser = model.LoggedOnUser.Length.ToString();
                                    model.PingTime = 0;
                                    model.Ip_Address = "0.0.0.0";
                                    model.Guid = reader["stn_guid"].ToString().Trim();
                                    model.OnlineDate = reader["sstat_timestamp"].ToString().Trim();
                                    db.Workstations.Add(model);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
            TestWorkstations();
        }



        public static void TestWorkstations()
        {
            try
            {
                foreach (var station in db.Workstations.AsEnumerable())
                {
                    string hostname = station.Name;
                    PingReply pr = PingHost(hostname);
                    if (pr != null)
                    {
                        if (pr.Status == IPStatus.Success)
                        {
                            station.Online = true;
                        station.Ip_Address = pr.Address.ToString();
                        station.PingTime = pr.RoundtripTime;
                        }
                        else
                        {
                            station.Online = false;
                        }
                    }
                    else
                    {
                        station.Online = false;
                    }
                }
            }
            catch (SqlException ex) { throw ex; }
            db.SaveChanges();
        }
    }
}

