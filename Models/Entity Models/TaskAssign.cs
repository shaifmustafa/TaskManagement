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
    public class TaskAssign : Entity
    {
        public Int64 Id { get; set; }

        [Required]
        public String TaskAssignType { get; set; }  /* g = group, d = department, e = employee, m = member */


        [Required]
        public Int64 TaskAssignTypeId { get; set; }  /* group_id, department_id, employee_id, member_id */

        [Index]
        [MaxLength(100)]
        [Required]
        public String TaskName { get; set; }

        public String TaskShortName { get; set; }

        public String Logo { get; set; }

        public String Banner { get; set; }

        public String TaskType { get; set; }

        public String Priority { get; set; }

        public String TaskDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Status { get; set; }
    }
}
