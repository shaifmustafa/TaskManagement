using Models.Entity_Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Bll.Base;
using Repository.Repositories;

namespace Bll.Managers
{
    public class ChatMessageManager : Manager<ChatMessage>
    {
        public ChatMessageManager() : base(new ChatMessageRepository())
        {

        }


        public override bool SaveOrUpdate(ChatMessage entity)
        {
            var exist = Repository.GetById(entity.Id);

            if (exist == null)
            {                
                entity.CreatedBy = "Admin";
                entity.Created = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
                entity.Modified = System.DateTime.Now;
                entity.IsActive = true;
            }
            else
            {
                entity.CreatedBy = exist.CreatedBy;

                entity.Created = exist.Created;

                entity.ModifiedBy = "Admin";

                entity.Modified = DateTime.Now;
                entity.IsActive = true;
            }


            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }
    }
}
