using Bll.Base;
using Models.Entity_Models;
using Repository.Repositories;
using System;
//using Repo = Repository.Base.Repository<Models.Entity_Models.Department>;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Managers
{
    public class DepartmentManager : Manager<Department>
    {
        public DepartmentManager() : base(new DepartmentRepository())
        {

        }

        public override bool SaveOrUpdate(Department entity)
        {            
            if (entity.Id == 0)
            {
                entity.CreatedBy = "Admin";
                entity.Created = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
                entity.Modified = System.DateTime.Now;
                entity.IsActive = true;
                Repository.SaveOrUpdate(entity);
            }
            else
            {
                var exist = Repository.GetById(entity.Id);
                entity.CreatedBy = exist.CreatedBy;

                //DateTime createdOn = Convert.ToDateTime(System.DateTime.Now);
                //String createdOnText = createdOn.ToString("yyyy-MM-dd");
                //entity.Created = Convert.ToDateTime(createdOnText);
                entity.Created = exist.Created;

                entity.ModifiedBy = exist.ModifiedBy;

                //DateTime modifiedOn = Convert.ToDateTime(System.DateTime.Now);
                //String modifiedOnText = createdOn.ToString("yyyy-MM-dd");
                //entity.Modified = Convert.ToDateTime(modifiedOnText);

                entity.Modified = exist.Modified;
                entity.IsActive = true;
            }


            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }

        public override bool Save(Department entity)
        {
            //entity.Id = Guid.NewGuid().ToString();
            entity.CreatedBy = "Admin";
            entity.Created = System.DateTime.Now;
            entity.ModifiedBy = "Admin";
            entity.Modified = System.DateTime.Now;
            entity.IsActive = true;
            return base.Save(entity);
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
