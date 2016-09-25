using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class Panel : P2000DBHardware
    {
        public Panel()
        {
            this.terminals = new HashSet<Terminal>();
            this.inputs = new HashSet<Input>();
            this.outputs = new HashSet<Output>();
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terminal> terminals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Input> inputs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Output> outputs { get; set; }
        
        public string _Model;
        public string Model { get
            {
                switch (_Model)
                {
                    case "12":
                        return "S321-IP";
                    case "14":
                        return "CK721-A";
                    default:
                        return _Model;
                }
            }
            set
            {
                _Model = value;
            }
        }
        public string Version { get; set; }
        [Display(Name = "IP Address")]
        public string Ip_Address { get; set; }

        public override string ToString()
        {
            string return_string = this.Id.ToString() + ", " + this.Name + ", " + this.Model;
            return_string += this.Version + ", " + this.Ip_Address + ", " + this.Guid;
            return_string += this.partition.Name;

            return base.ToString();
        }
        
    }
}