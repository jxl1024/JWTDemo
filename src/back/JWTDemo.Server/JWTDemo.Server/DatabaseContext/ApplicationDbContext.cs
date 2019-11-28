using JWTDemo.Server.Entity;
using JWTDemo.Server.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Server.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<User> Users { get; set; }
        #endregion

        /// <summary>
        /// 重写OnModelCreating，配置映射关系
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 应用映射关系
            modelBuilder.ApplyConfiguration<User>(new UserMap());
            #endregion

            #region 添加种子数据
            // 添加种子数据
            modelBuilder.Entity<User>().HasData(
                 new User()
                 {
                     Id = 1,
                     UserCode = "System",
                     UserName = "超级管理员",
                     Password = "System",
                     Age = 23,
                     Sex = 1,
                     CreateUserId = "System",
                     CreateDateTime = DateTime.Now,
                     UpdateUserId = "System",
                     UpdateDateTime = DateTime.Now
                 },
                 new User()
                 {
                     Id = 2,
                     UserCode = "admin",
                     UserName = "普通管理员",
                     Password = "123456",
                     Age = 23,
                     Sex = 1,
                     CreateUserId = "System",
                     CreateDateTime = DateTime.Now,
                     UpdateUserId = "System",
                     UpdateDateTime = DateTime.Now
                 },
                 new User()
                 {
                     Id = 3,
                     UserCode = "345923",
                     UserName = "小红",
                     Age = 25,
                     Sex = 2,
                     Password = "345923",
                     CreateUserId = "System",
                     CreateDateTime = DateTime.Now,
                     UpdateUserId = "System",
                     UpdateDateTime = DateTime.Now
                 }
                );
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
