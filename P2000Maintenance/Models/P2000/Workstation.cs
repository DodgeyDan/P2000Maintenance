using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class Workstation : P2000DBHardware
    {
        public bool Enabled { get; set; }
        public bool Badging { get; set; }
        public bool Server { get; set; }
        public string Version { get; set; }
        public string OnlineDate { get; set; }
        [Display(Name = "IP Address")]
        public string Ip_Address { get; set; }
        [Display(Name = "Ping Time")]
        public long PingTime { get; set; }
        [Display(Name = "Online")]
        public bool Online { get; set; }

        public override string ToString()
        {
            string return_string = this.Id.ToString() + ", " + this.Name + ", " + this.Enabled + ", " + this.Badging + ", " + this.Server;
            return_string += this.Version + ", " + this.Ip_Address + ", " + this.Guid;

            return base.ToString();
        }
    }
}