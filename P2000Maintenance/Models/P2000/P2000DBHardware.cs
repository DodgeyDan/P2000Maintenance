using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class P2000DBHardware : Maintainable
    {
        public int dbId { get; set; }
        public string Guid { get; set; }
        public virtual Partition partition { get; set; }
        //public virtual Hardware.Asset Controller { get; set; }
    }
}