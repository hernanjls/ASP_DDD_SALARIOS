using Microsoft.EntityFrameworkCore;
using Salarios.Dominio.Entidades;

namespace Salarios.Infraestructura.Datos
{
    public class ContextBase : DbContext
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

        public virtual DbSet<Empleado> TblEmployee { get; set; }
        public virtual DbSet<Division> TblDivisiones { get; set; }
        public virtual DbSet<Salario> TblSalarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(StrincConectionConfig());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().ToTable("TblEmployee");
            modelBuilder.Entity<Empleado>().Property(e => e.EmployeeCode)
                .HasColumnType("varchar(10)")
                .IsRequired();

            modelBuilder.Entity<Empleado>().Property(e => e.EmployeeName)
                .HasColumnType("varchar(150)")
                .IsRequired();

            modelBuilder.Entity<Empleado>().Property(e => e.EmployeeSurName)
                .HasColumnType("varchar(150)")
                .IsRequired();

            modelBuilder.Entity<Empleado>().Property(e => e.IdentificationNumber)
                .HasColumnType("varchar(10)")
                .IsRequired();

            modelBuilder.Entity<Empleado>().Property(e => e.BeginDate)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Empleado>().Property(e => e.Birthday)
                .HasColumnType("date")
                .IsRequired();


            modelBuilder.Entity<Division>().HasData(
                new Division { DivisionId = 1, DivisionName = "OPERATION" },
                new Division { DivisionId = 2, DivisionName = "SALES" },
                new Division { DivisionId = 3, DivisionName = "CUSTOMER CARE" },
                new Division { DivisionId = 4, DivisionName = "MARKETING" }
            );


        }


        private string StrincConectionConfig()
        {
            string strCon = "Server=(local)\\sqlexpress2014;Database=asp_core_ddd;Trusted_Connection=True;MultipleActiveResultSets=true";

            return strCon;
        }
    }
}
