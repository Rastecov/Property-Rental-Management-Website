using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class PropertyRentalDBContext : DbContext
    {
        public PropertyRentalDBContext()
        {
        }

        public PropertyRentalDBContext(DbContextOptions<PropertyRentalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Apartments> Apartments { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<Rentals> Rentals { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Tenants1> Tenants1 { get; set; }
        public virtual DbSet<Users1> Users1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ERASTE-THE-BREA\\SQLEXPRESS;Initial Catalog=PropertyRentalDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.AddressId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Apartments>(entity =>
            {
                entity.HasKey(e => e.ApartmentNumber);

                entity.Property(e => e.ApartmentNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ApartmentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_Apartments_Buildings");
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.Property(e => e.AppointmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AppointmentDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TenantId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Appointments_Employees");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_Appointments_Tenants1");
            });

            modelBuilder.Entity<Buildings>(entity =>
            {
                entity.HasKey(e => e.BuildingId);

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AdressId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BuildingName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.AdressId)
                    .HasConstraintName("FK_Buildings_Addresses1");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Users1");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Employees");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Tenants1");
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AdressId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BuildingId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.AdressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Properties_Addresses");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Properties_Buildings");
            });

            modelBuilder.Entity<Rentals>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PK_Rentals_1");

                entity.Property(e => e.RentalId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ApartmentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Enddate).HasColumnType("date");

                entity.Property(e => e.RentalPrice)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rentals_Apartments1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rentals_Employees");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rentals_Tenants1");
            });

            modelBuilder.Entity<Schedules>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK_Schedules_1");

                entity.Property(e => e.ScheduleId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AppointmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ScheduleDate).HasColumnType("date");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Schedules_Appointments1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Schedules_Employees");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_Schedules_Tenants1");
            });

            modelBuilder.Entity<Tenants1>(entity =>
            {
                entity.HasKey(e => e.TenantId)
                    .HasName("PK_Tenants1_1");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tenant)
                    .WithOne(p => p.Tenants1)
                    .HasForeignKey<Tenants1>(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tenants1_Users1");
            });

            modelBuilder.Entity<Users1>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
