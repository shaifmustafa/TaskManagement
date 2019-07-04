using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity_Models
{
    public class Group : Entity
    {
        public Int64 Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String ShortName { get; set; }
        public String Details { get; set; }
        public String Logo { get; set; }
        public String Banner { get; set; }       

        public String[] EmployeeArray { get; set; }
    }
}
