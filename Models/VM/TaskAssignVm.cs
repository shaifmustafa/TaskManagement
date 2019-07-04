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
    public class TaskAssignVm : TaskAssign
    {
        public string TaskAssignTypeName { get; set; }
        public string StartDateText { get { return StartDate.ToString("yyyy-MM-dd"); } }
        public string EndDateText { get { return EndDate.ToString("yyyy-MM-dd"); } }
    }
}
