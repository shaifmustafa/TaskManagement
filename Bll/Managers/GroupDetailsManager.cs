using Bll.Base;
using Models.DatabaseContext;
using Models.Entity_Models;
using Models.VM;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Managers
{
    public class GroupDetailsManager : Manager<GroupDetails>
    {
        //GroupDetailsManager groupDetailsManager = new GroupDetailsManager();        

        public GroupDetailsManager() : base(new GroupDetailsRepository())
        {

        }
        

        public override bool Save(GroupDetails entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedBy = "Admin";
                entity.Created = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
                entity.Modified = System.DateTime.Now;
                entity.IsActive = true;
            }

            else
            {
                var exist = Repository.GetById(entity.Id);

                entity.CreatedBy = exist.Id.ToString();
                entity.Created = System.DateTime.Now;
                entity.ModifiedBy = exist.Id.ToString();
                entity.Modified = System.DateTime.Now;
                entity.IsActive = true;
            }


            return Repository.Save(entity);
        }

        public override bool SaveOrUpdate(GroupDetails entity)
        {
            bool isNew = false;

            var groupDetails = Repository.GetAll();

            var exist = Repository.GetAll().Where(x => x.GroupId == entity.GroupId && x.IsActive).Select(y => y.EmployeeInfoId).ToList();

            var test = Repository.GetAll().Where(x => x.GroupId == entity.GroupId && x.IsActive).ToList();


            if (exist.Count != 0)
            {
                for (var i = 0; i < exist.Count; i++)
                {
                    if (exist[i].Equals(entity.EmployeeInfoId))
                    {
                        var query = Repository.GetAll().Where(x => x.EmployeeInfoId == exist[i] && x.GroupId == entity.GroupId
                                    && x.IsActive).Select(y => y.Id).ToList();


                        var isExist = Repository.GetById(query[0]);

                        if (isExist != null)
                        {
                            entity.CreatedBy = isExist.Id.ToString();

                            entity.Created = isExist.Created;

                            entity.ModifiedBy = isExist.Id.ToString();

                            entity.Modified = isExist.Modified;
                            entity.IsActive = true;

                            Repository.SaveOrUpdate(entity);
                            Repository.Done();
                        }
                    }

                    else
                    {
                        isNew = true;
                        entity.CreatedBy = "Admin";
                        entity.Created = System.DateTime.Now;
                        entity.ModifiedBy = "Admin";
                        entity.Modified = System.DateTime.Now;
                        entity.IsActive = true;

                        Repository.SaveOrUpdate(entity);
                        Repository.Done();
                    }
                }
            }

            else
            {
                entity.CreatedBy = "Admin";
                entity.Created = System.DateTime.Now;
                entity.ModifiedBy = "Admin";
                entity.Modified = System.DateTime.Now;
                entity.IsActive = true;

                Repository.SaveOrUpdate(entity);
            }

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
