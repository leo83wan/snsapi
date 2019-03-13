using com.leo83.apis.sns.Data.v1.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace com.leo83.apis.sns.Data.v1
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class SnsContext : DbContext
    {
        /// <summary>
        /// 实例化数据上下文
        /// </summary>
        /// <param name="options">options</param>
        public SnsContext(DbContextOptions<SnsContext> options)
            : base(options)
        { }

        /// <summary>
        /// 帐号
        /// </summary>
        /// <value></value>
        public DbSet<Account> Accounts { get; set; }
        /// <summary>
        /// 帐号信息
        /// </summary>
        /// <value></value>
        public DbSet<AccountInfo> AccountInfos { get; set; }
        /// <summary>
        /// 帐户角色
        /// </summary>
        /// <value></value>
        public DbSet<AccountRole> AccountRoles { get; set; }

        // /// <summary>
        // /// 视频分类
        // /// </summary>
        // /// <value></value>
        // public DbSet<VideoCategory> VideoCategories { get; set; }
        // /// <summary>
        // /// 视频
        // /// </summary>
        // /// <value></value>
        // public DbSet<Video> Videos { get; set; }
        // /// <summary>
        // /// 视频文件
        // /// </summary>
        // /// <value></value>
        // public DbSet<VideoFile> VideoFiles { get; set; }
        // /// <summary>
        // /// 视频评论
        // /// </summary>
        // /// <value></value>
        // public DbSet<VideoComment> VideoComments { get; set; }

        /// <summary>
        /// 创建的条件
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.AccountId);
            modelBuilder.Entity<Account>().Property(a => a.AccountId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>().HasIndex(a => a.Username).IsUnique(true);
            modelBuilder.Entity<Account>().HasOne(a => a.Info).WithOne(ai => ai.Account).HasForeignKey<AccountInfo>(ai => ai.AccountId);
            modelBuilder.Entity<Account>().HasMany(a => a.Roles).WithOne(ar => ar.Account).HasForeignKey(ar => ar.AccountId);
            modelBuilder.Entity<AccountInfo>().HasKey(ai => ai.AccountId);
            modelBuilder.Entity<AccountRole>().HasKey(ar => ar.AccountId);
        

            // modelBuilder.Entity<VideoCategory>()
            // .HasIndex(t => t.Name).IsUnique();

            // modelBuilder.Entity<AccountCollectionVideo>()
            // .HasKey(acv => new { acv.AccountId, acv.VideoId });

            // modelBuilder.Entity<AccountCollectionVideo>()
            // .HasOne(acv => acv.Account)
            // .WithMany(a => a.AccountCollectionVideos)
            // .HasForeignKey(acv => acv.AccountId);

            // modelBuilder.Entity<AccountCollectionVideo>()
            // .HasOne(acv => acv.Video)
            // .WithMany(v => v.AccountCollectionVideos)
            // .HasForeignKey(acv => acv.VideoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}