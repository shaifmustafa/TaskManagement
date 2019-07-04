using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entity_Models;
using System.Data.Entity;

namespace Models.DatabaseContext
{
    public class TaskMDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskMDbContext() : base("ServerDB")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static TaskMDbContext Create()
        {
            return new TaskMDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TaskMDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        //tables
        #region Company

        public DbSet<CompanyList> Companys { get; set; }

        #endregion

        #region ChatMessage

        public DbSet<ChatMessage> ChatMessages { get; set; }

        #endregion

        #region CommentOnTask

        public DbSet<CommentOnTask> CommentOnTasks { get; set; }

        #endregion

        #region EmployeeInfo

        public DbSet<EmployeeInfo> EmployeeInfos { get; set; }

        #endregion


        #region Rank

        public DbSet<RankList> Ranks { get; set; }

        #endregion

        #region SoftwareUser

        public DbSet<SoftwareUser> SoftwareUsers { get; set; }

        #endregion

        #region TaskAttachment

        public DbSet<TaskAttachment> TaskAttachments { get; set; }

        #endregion      

        #region TaskAssign

        public DbSet<TaskAssign> TaskAssigns { get; set; }

        #endregion

        #region Departments

        public DbSet<Department> Departments { get; set; }

        #endregion

        #region Group

        public DbSet<Group> Groups { get; set; }

        #endregion


        #region Member

        public DbSet<GroupDetails> GroupDetails { get; set; }

        #endregion


        #region Member

        public DbSet<Member> Members { get; set; }

        #endregion
    }
}
