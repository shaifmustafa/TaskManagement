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
    public class ChatMessage : Entity
    {
        public Int64 Id { get; set; }


        [Index]
        [MaxLength(100)]
        public String ChatMessageText { get; set; }        

        public Int64 TaskAssignId { get; set; }
        [ForeignKey("TaskAssignId")]
        public virtual TaskAssign TaskAssign { get; set; }


        //[Index]
        //[MaxLength(100)]
        //public String TargetUserId { get; set; }

        //[Index]
        //[MaxLength(100)]
        //public String Status { get; set; }
    }
}
