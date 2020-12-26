using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace Contacts
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyPerson> CompanyPersons { get; set; }
        public DbSet<PersonResume> PersonResumes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(a =>
            {
                a.GetProperties().Where(p => p.ClrType == typeof(string)).ToList().ForEach(pp =>
                {
                    pp.IsUnicode(false);
                });
            });
            modelBuilder.HasDefaultSchema("Contacts");

            modelBuilder.Entity<Person>().ToTable("People", "Contacts").HasKey(a => a.PersonId);

            modelBuilder.Entity<Person>().HasIndex(a => a.LastName).HasName("IX_LNAME");

            //update alternate key to unquie index
            modelBuilder.Entity<Person>().HasIndex(a => new { a.LastName, a.FirstName }).IsUnique().HasName("UQ_LNAME");

            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnName("LastName");
            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired().HasMaxLength(40).IsUnicode(false);
            modelBuilder.Entity<Person>().Property(p => p.BirthDate).ForSqlServerHasColumnType("date").ForSqlServerHasDefaultValueSql("getdate()");
            modelBuilder.Entity<Person>().Property(p => p.IsActive).ForSqlServerHasDefaultValue(true);
            modelBuilder.Entity<Person>().Property(p => p.Height).ForSqlServerHasDefaultValue(0).ForSqlServerHasColumnType("decimal(6,2)");

           

            modelBuilder.Entity<Person>()
                .HasMany(p => p.PersonPnones)
                .WithOne(a => a.Person)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.PersonId)
                .HasConstraintName("FK_PHONE_PERSON");

            modelBuilder.Entity<Person>()
                .HasOne(e => e.PersonType)
                .WithMany(e => e.Persons)
                .HasForeignKey(e => e.PersonTypeId)
                .HasConstraintName("FK_PERSON_PERSONTYPE");


            modelBuilder.Entity<CompanyPerson>()
                .HasKey(e => new { e.PersonId, e.CompanyId });

            modelBuilder.Entity<CompanyPerson>()
                .HasOne(e => e.Company).WithMany(e => e.CompanyPersons).HasForeignKey(e => e.CompanyId)
                .ForSqlServerHasConstraintName("FK_COMPANYPERSON_COMPANY").OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyPerson>()
                .HasOne(e => e.Person).WithMany(e => e.CompanyPersons).HasForeignKey(e => e.PersonId)
                .ForSqlServerHasConstraintName("FK_COMPANYPERSON_PERSON").OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonResume>()
                .HasKey(e => e.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(e => e.PersonResume)
                .WithOne(e => e.Person)
                .HasForeignKey<PersonResume>(e => e.PersonId)
                .ForSqlServerHasConstraintName("FK_RESUME_PERSON");

        }
    }
}