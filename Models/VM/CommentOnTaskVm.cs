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
    public class CommentOnTaskVm : TaskAssign
    {
        public Int64 TaskId { get; set; }
        public String Comment { get; set; }
    }
}
