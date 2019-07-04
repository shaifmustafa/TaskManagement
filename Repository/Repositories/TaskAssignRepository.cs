using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entity_Models;
using Models.DatabaseContext;

namespace Repository.Repositories
{
    public class TaskAssignRepository : Repository<TaskAssign>
    {
        public TaskAssignRepository() : base(new TaskMDbContext())
        {

        }
    }
}
