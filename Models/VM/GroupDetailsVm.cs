using Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.VM
{
    [NotMapped]
    public class GroupDetailsVm: GroupDetails
    {      
        //public Int64 EmployeeInfoId { get; set; }
        //public Int64 GroupId { get; set; }
        public string EmployeeInfoName { get; set; }

        public string GroupName { get; set; }

    }
}
