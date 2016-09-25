using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class Output:P2000DBHardware
    {
        public int? Number { get; set; }
        public bool Enabled { get; set; }
        
        public virtual Panel Panel { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}