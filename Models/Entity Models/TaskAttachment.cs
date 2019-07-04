using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models.Entity_Models
{
    public class TaskAttachment : Entity
    {
        public Int64 Id { get; set; }


        //public Int64 TaskAssignId { get; set; }
        //[ForeignKey("TaskAssignId")]
        //public virtual TaskAssign TaskAssign { get; set; }

        [Index]
        [MaxLength(100)]
        public String TaskAttachmentName { get; set; }

        public String TaskAttachmentFileName { get; set; }

        //[DisplayName("Select Image")]
        //public String ImagePath { get; set; }

        //public HttpPostedFileBase ImageFile { get; set; }
        public String TaskAttachmentStatus { get; set; }
    }
}
