using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Category;
using Domain.Model.User;
using Infrastructure.Core.Data;

namespace Domain.Model
{
   public class YhContext:DbContext,IUnitOfWork
    {
       public YhContext():base("name=YhDeboke")
       {
       }

       /// <summary>
       /// 管理员集合
       /// </summary>
       public DbSet<Administrator> Administrators { get; set; }

       /// <summary>
       /// 角色
       /// </summary>
       public DbSet<Role> Roles { get; set; }

       /// <summary>
       /// 用户
       /// </summary>
       public DbSet<User.User> Users { get; set; }

       /// <summary>
       /// 系统日志
       /// </summary>
       public DbSet<SysLog> SysLogs { get; set; }
       #region 栏目

       /// <summary>
       /// 栏目
       /// </summary>
       public DbSet<Category.Category> Categories { get; set; }

       /// <summary>
       /// 常规栏目
       /// </summary>
       public DbSet<CategoryGeneral> CategoryGeneral { get; set; }

       /// <summary>
       /// 单页栏目
       /// </summary>
       public DbSet<CategoryPage> CategoryPages { get; set; }

       /// <summary>
       /// 链接栏目
       /// </summary>
       public DbSet<CategoryLink> CategoryLinks { get; set; }

       #endregion

       #region 内容
       /// <summary>
       /// 内容类型
       /// </summary>
       public DbSet<Content.ContentType> ContentTypes { get; set; }



       #endregion

       #region 博客文章

       public DbSet<Blog.Blog> Blogs { get; set; }

       public DbSet<Blog.LeaveMsg> LeaveMsg { get; set; }
#endregion

       public int Commit()
       {
          return this.SaveChanges();
       }

       public void Rollback()
       {
           throw new NotImplementedException();
       }

       public async Task<int> CommitAsync()
       {
        return  await this.SaveChangesAsync();
       }
    }
}
