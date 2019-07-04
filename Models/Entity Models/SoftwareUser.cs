using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity_Models
{
    public class SoftwareUser : Entity
    {
        public Int64 Id { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public String Password { get; set; }

        public String Email { get; set; }
        public String RecoveryEmail { get; set; }
    }
}
