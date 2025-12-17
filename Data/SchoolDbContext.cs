using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace SchoolJournal.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<GroupData> Groups => Set<GroupData>();
        public DbSet<TeacherData> Teachers => Set<TeacherData>();
        public DbSet<ChildData> Children => Set<ChildData>();
        public DbSet<GradeData> Grades => Set<GradeData>();
        public DbSet<UserAccount> Users => Set<UserAccount>();

        private static string DbPath =>
            Path.Combine(AppContext.BaseDirectory, "school.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupData>(e =>
            {
                e.ToTable("Groups");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.AgeCategory).IsRequired().HasMaxLength(50);

                // поле сумісності (див. GroupData.cs)
                e.Property(x => x.Teacher).IsRequired().HasMaxLength(200);

                e.Property(x => x.Room).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<TeacherData>(e =>
            {
                e.ToTable("Teachers");
                e.HasKey(x => x.Id);

                e.Property(x => x.FullName).IsRequired().HasMaxLength(120);
                e.Property(x => x.Phone).HasMaxLength(30);
                e.Property(x => x.Email).HasMaxLength(120);
                e.Property(x => x.Position).IsRequired().HasMaxLength(60);

                e.HasOne(x => x.Group)
                    .WithMany(g => g.Teachers)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Щоб швидко знаходити основного вчителя класу
                e.HasIndex(x => new { x.GroupId, x.IsPrimary });
            });

            modelBuilder.Entity<ChildData>(e =>
            {
                e.ToTable("Children");
                e.HasKey(x => x.Id);

                e.Property(x => x.FullName).IsRequired().HasMaxLength(120);
                e.Property(x => x.ParentFullName).HasMaxLength(120);
                e.Property(x => x.ParentPhone).HasMaxLength(30);
                e.Property(x => x.Address).HasMaxLength(200);
                e.Property(x => x.MedicalNotes).HasMaxLength(300);
                e.Property(x => x.NotesForParents).HasMaxLength(500);

                e.HasOne(x => x.Group)
                    .WithMany(g => g.Children)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => x.GroupId);
            });

            modelBuilder.Entity<GradeData>(e =>
            {
                e.ToTable("Grades");
                e.HasKey(x => x.Id);

                e.Property(x => x.Subject).IsRequired().HasMaxLength(80);
                e.Property(x => x.Comment).HasMaxLength(200);

                e.Property(x => x.Value).IsRequired();
                e.Property(x => x.DateAssigned).IsRequired();

                e.HasOne(x => x.Student)
                    .WithMany(s => s.Grades)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Teacher)
                    .WithMany(t => t.Grades)
                    .HasForeignKey(x => x.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => x.StudentId);
                e.HasIndex(x => x.TeacherId);
                e.HasIndex(x => new { x.StudentId, x.Subject });
            });


            modelBuilder.Entity<UserAccount>(e =>
            {
                e.ToTable("Users");
                e.HasKey(x => x.Id);

                e.Property(x => x.Username).IsRequired().HasMaxLength(60);
                e.HasIndex(x => x.Username).IsUnique();

                e.Property(x => x.PasswordHash).IsRequired();
                e.Property(x => x.PasswordSalt).IsRequired();

                e.Property(x => x.Role).IsRequired();

                e.HasOne(x => x.Teacher)
                    .WithMany()
                    .HasForeignKey(x => x.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Child)
                    .WithMany()
                    .HasForeignKey(x => x.ChildId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => x.TeacherId);
                e.HasIndex(x => x.ChildId);
            });
        }
    }
}
