using Models.DatabaseContext;
using Models.Entity_Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompanyListRepository : Repository<CompanyList>
    {
        public CompanyListRepository() : base(new TaskMDbContext())
        {

        }
    }
}
