using JWTDemo.Api.Entity;
using JWTDemo.Api.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace JWTDemo.Api.Context
{
    public class ApplicationDbContext:DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        #region DbSet
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 应用映射关系
            modelBuilder.ApplyConfiguration<User>(new UserMap());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
