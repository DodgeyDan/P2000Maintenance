using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class Partition:BaseModel
    {
        public int dbId { get; set; }
        public string Site { get; set; }
        public string Guid { get; set; }

        public Partition()
        {
            this.p2000Devices = new HashSet<P2000DBHardware>();
            /*
        this.inputs = new HashSet<Input>();
        this.outputs = new HashSet<Output>();
        this.panels = new HashSet<Panel>();
        this.workstations = new HashSet<Workstation>();
        this.terminals = new HashSet<Terminal>();
        */
        }

        public override string ToString()
        {
            string return_string = this.Id.ToString() + ", " + this.Name + ", " + this.Site + ", " + this.Guid;
            return return_string;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<P2000DBHardware> p2000Devices { get; set; }
        
    }
}