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
    public class CompanyList : Entity
    {
        public Int64 Id { get; set; }

        [Index]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }

        [Required]
        public String ShortName { get; set; }

        public String ContactPerson { get; set; }

        [Required]
        public String ContactMobile { get; set; }

        public String RegisterNo { get; set; }
        public String Address { get; set; }
        public String Status { get; set; }
    }
}
