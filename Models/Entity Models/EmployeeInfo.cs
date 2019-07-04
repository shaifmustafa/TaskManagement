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
    public class EmployeeInfo : Entity
    {
        public Int64 Id { get; set; }

        [Index]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }

        public String ProfilePhoto { get; set; }
        public String SignaturePhoto { get; set; }

        [Index]
        [MaxLength(100)]
        public String Mobile { get; set; }

        public String EmergencyMobile { get; set; }

        [Required]
        public String Address { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime Dob { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }
        public String Email { get; set; }

        [Required]
        public String Designation { get; set; }

        public Int64 DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }        


        public Int64 CompanyListId { get; set; }
        [ForeignKey("CompanyListId")]
        public virtual CompanyList Company { get; set; }


        public Int64 RankListId { get; set; }
        [ForeignKey("RankListId")]
        public virtual RankList RankList { get; set; }        
    }
}
