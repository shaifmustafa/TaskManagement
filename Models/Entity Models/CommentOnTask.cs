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
    public class CommentOnTask : Entity
    {
        public Int64 Id { get; set; }

        [Index]
        [MaxLength(100)]
        public String TaskComment { get; set; }

        public Int64 TaskAssignId { get; set; }
        [ForeignKey("TaskAssignId")]
        public virtual TaskAssign TaskAssign { get; set; }



        //public String CommentText { get; set; }

        //public String Status { get; set; }
    }
}
