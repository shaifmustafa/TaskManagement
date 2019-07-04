using Models.Entity_Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models.DatabaseContext;

namespace Repository.Repositories
{
    public class CommentOnTaskRepository : Repository<CommentOnTask>
    {
        public CommentOnTaskRepository() : base(new TaskMDbContext())
        {

        }
    }
}
