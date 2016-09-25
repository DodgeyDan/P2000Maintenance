using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models
{
    public class Maintainable : BaseModel
    {
        public Nullable<DateTime> LastMaintained { get; set; }
        public string Technician { get; set; }
        [Display(Name = "In Contract")]
        public bool Included { get; set; }
    }
    
}