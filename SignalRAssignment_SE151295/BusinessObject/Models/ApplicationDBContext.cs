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
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):
                base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server =(local); database = ApplicationDBContext;uid=sa;pwd=112233;");
            }
        }
        public virtual  DbSet<Posts> Posts { set; get; }
        public virtual  DbSet<PostCategories> PostCategories { set; get; }
        public virtual  DbSet<AppUsers> AppUsers { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(p => p.PostID)
                      .HasColumnName("PostID")
                      .IsRequired();

                entity.Property(p => p.AuthorID).HasColumnName("AuthorID");
                entity.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                entity.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
                entity.Property(p => p.Title).HasColumnName("Title");
                entity.Property(p => p.Content).HasColumnName("Content");
                entity.Property(p => p.PublishStatus).HasColumnName("PublishStatus");
                entity.Property(p => p.CategoryID).HasColumnName("CategoryID");


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

                entity.Property(p => p.CategoryName).HasColumnName("CategoryName");
                entity.Property(p => p.Description).HasColumnName("Description");

            });

            modelBuilder.Entity<AppUsers>(entity =>
            {
                entity.Property(p => p.UserID)
                      .IsRequired();
                entity.Property(p => p.FullName).HasColumnName("FullName");
                entity.Property(p => p.Address).HasColumnName("Address");
                entity.Property(p => p.Email).HasColumnName("Email");
                entity.Property(p => p.Password).HasColumnName("Password");

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
