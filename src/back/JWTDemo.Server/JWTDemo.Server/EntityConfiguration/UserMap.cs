using JWTDemo.Server.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTDemo.Server.EntityConfiguration
{
    /// <summary>
    /// 实现IEntityTypeConfiguration接口
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // 配置生成的表名
            builder.ToTable("User");
            // 设置主键
            builder.HasKey(p => p.Id);
            // 设置属性
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.UserCode).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(40);
        }
    }
}
