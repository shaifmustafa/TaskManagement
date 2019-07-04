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
    public class ChatMessageVm : ChatMessage
    {
        public string CreatedDateText { get { return Created.ToString("yyyy-MM-dd hh:mm"); } }
    }
}
