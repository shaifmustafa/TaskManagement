using Bll.Base;
using Models.Entity_Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Managers
{
    public class TaskAssignManager : Manager<TaskAssign>
    {
        public TaskAssignManager() : base(new TaskAssignRepository())
        {

        }
        public override bool Save(TaskAssign entity)
        {
            //entity.Id = Guid.NewGuid().ToString();
            entity.CreatedBy = "Admin";
            entity.Created = System.DateTime.Now;
            entity.ModifiedBy = "Admin";
            entity.Modified = System.DateTime.Now;
            entity.IsActive = true;
            return base.Save(entity);
        }

        public override bool SaveOrUpdate(TaskAssign entity)
        {
            var exist = Repository.GetById(entity.Id);

            if (exist == null)
            {
                entity.Status = "Ongoing";
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
        


        public bool Delete(Int64 Id)
        {
            var exist = Repository.GetById(Id);
            exist.ModifiedBy = exist.Id.ToString();
            exist.Modified = System.DateTime.Now;
            exist.IsActive = false;
            Repository.SaveOrUpdate(exist);
            return Repository.Done();
        }

    }
}
