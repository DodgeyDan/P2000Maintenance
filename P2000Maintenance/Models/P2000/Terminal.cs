using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models.P2000
{
    public class Terminal : P2000DBHardware
    {
        public Terminal()
        {
            this.inputs = new HashSet<Input>();
            this.outputs = new HashSet<Output>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Input> inputs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Output> outputs { get; set; }

        public int Index { get; set; }
        public string ReaderType { get; set; }
        public bool ReaderEnabled { get; set; }
        public int? ReaderModule { get; set; }
        public int? ReaderIndex { get; set; }
        public string IOType { get; set; }
        public bool InputEnabled { get; set; }
        public int? InputModule { get; set; }
        public int? InputIndex { get; set; }
        public bool OutputEnabled { get; set; }
        public int? OutputModule { get; set; }
        public int? OutputIndex { get; set; }
        public bool Enabled { get; set; }
        
        public virtual Panel Panel { get; set; }


        public override string ToString()
        {
            string return_string = this.Id.ToString() + ", " + this.Name;
            return_string += this.ReaderEnabled + ", " + this.ReaderModule + ", " + this.ReaderIndex;
            return_string += ", " + this.InputEnabled + ", " + this.InputModule + ", " + this.InputIndex;
            return_string += ", " + this.OutputEnabled + ", " + this.OutputModule + ", " + this.OutputIndex;
            return_string += ", " + this.Guid;
            return_string += ", " + this.partition.Name + ", " + this.Panel.Name;

            return base.ToString();
        }
    }
}