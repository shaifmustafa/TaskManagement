using Bll.Base;
using Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using Repository.Repositories;

namespace Bll.Managers
{
    public class CommentOnTaskManager : Manager<CommentOnTask>
    {
        public CommentOnTaskManager() : base(new CommentOnTaskRepository())
        {

        }


        public bool SaveComments(CommentOnTask entity)
        {
            var exist = Repository.GetById(entity.Id);

            if (exist != null)
            {
                entity.Id = exist.Id;
                entity.TaskAssign = exist.TaskAssign;
                entity.TaskAssignId = exist.TaskAssignId;
                entity.TaskComment = exist.TaskComment;
                entity.Created = exist.Created;
                entity.IsActive = exist.IsActive;
                entity.Modified = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
            }

            else
            {
                entity.Created = System.DateTime.Now;
                entity.CreatedBy = "Admin";
                entity.IsActive = true;
                entity.Modified = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
            }

            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }
    }
}
