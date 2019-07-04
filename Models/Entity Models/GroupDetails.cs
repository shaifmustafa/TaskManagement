using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity_Models
{
    public class GroupDetails : Entity
    {
        public Int64 Id { get; set; }

        public Int64 GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public Int64 EmployeeInfoId { get; set; }

        [ForeignKey("EmployeeInfoId")]
        public EmployeeInfo EmployeeInfo { get; set; }


        public String[] EmployeeArray { get; set; }

        //public static Int64 GroupDetailsId = 0;
    }
}
