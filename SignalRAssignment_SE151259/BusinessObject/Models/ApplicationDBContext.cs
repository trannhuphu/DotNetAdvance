using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):
                base(options)
        {
        }
        public virtual  DbSet<Posts> Posts { set; get; }
        public virtual  DbSet<PostCategories> PostCategories { set; get; }
        public virtual  DbSet<AppUsers> AppUsers { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(p => p.PostID)
                      .IsRequired();

                entity.HasOne(d => d.PostCategories)
                      .WithMany(p => p.Posts)
                      .HasForeignKey(d => d.CategoryID);

                entity.HasOne(d => d.AppUsers)
                      .WithMany(p => p.Posts)
                      .HasForeignKey(d => d.AuthorID);

            });

            modelBuilder.Entity<PostCategories>(entity =>
            {
                entity.Property(p => p.CategoryID)
                      .IsRequired();
            });

            modelBuilder.Entity<AppUsers>(entity =>
            {
                entity.Property(p => p.UserID)
                      .IsRequired();
            });

            modelBuilder.Entity<AppUsers>().HasData(
                new AppUsers
                {
                    UserID = 1,
                    Address = "HCM city",
                    Email = "user01@gmail.com",
                    FullName = "user01",
                    Password = "123"
                }
            );

            modelBuilder.Entity<PostCategories>().HasData(
                new PostCategories
                {
                    CategoryID = 1,
                    CategoryName = "Science",
                    Description = "Category include info physical, math, etc.",
                }

            );

            modelBuilder.Entity<Posts>().HasData(
                new Posts
                {
                    PostID = 1,
                    AuthorID = 1,
                    CategoryID = 1,
                    CreatedDate = new DateTime(2022, 2, 12),
                    UpdatedDate = new DateTime(2022, 2, 12),
                    Content = "NASA fire a rocket",
                    Title = "Science",
                    PublishStatus = 1
                }
            );

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
