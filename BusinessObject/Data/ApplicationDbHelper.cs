using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Data
{
    internal static class ApplicationDbHelper
    {
        public static void SetUp(this ModelBuilder modelBuilder)
        {
            // set table
            modelBuilder.Entity<Customer>().ToTable("Customers");

            // set key with fluentAPI
            modelBuilder.Entity<Customer>().HasKey(c => new { c.Id });

            #region Configuration to props
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            #endregion
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Fullname = "Kaiz Nguyenn",
                    Username = "kaiznguyenn",
                    Password = "123456",
                    Gender = Gender.Male,
                    Birthday = new DateTime(2001, 2, 4).ToString("dd/MM/yyyy"),
                    Address = "Vietnam"
                },
                new Customer
                {
                    Id = 2,
                    Fullname = "Annie",
                    Username = "annie12",
                    Password = "654321",
                    Gender = Gender.Female,
                    Birthday = new DateTime(2000, 10, 19).ToString("dd/MM/yyyy"),
                    Address = "Germany"
                }
            ); ;
        }
    }
}
