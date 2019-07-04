using Bll.Base;
using Models.Entity_Models;
using System;
using Repo = Repository.Base.Repository<Models.Entity_Models.EmployeeInfo>;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;

namespace Bll.Managers
{
    public class EmployeeInfoManager : Manager<EmployeeInfo>
    {
        public EmployeeInfoManager() : base(new EmployeeInfoRepository())
        {

        }

        public override bool SaveOrUpdate(EmployeeInfo entity)
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

                entity.Created = exist.Created;

                entity.ModifiedBy = exist.ModifiedBy;

                entity.Modified = exist.Modified;
                entity.IsActive = true;
                Repository.SaveOrUpdate(entity);
                Repository.Done();
            }


            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }

        public override bool Save(EmployeeInfo entity)
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
