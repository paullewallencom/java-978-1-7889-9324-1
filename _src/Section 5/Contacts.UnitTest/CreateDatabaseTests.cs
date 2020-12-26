using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Contacts.UnitTest
{
    [TestClass]
    public class CreateDatabaseTests
    {
        private ContactsContext _context;

        [TestMethod]
        public void Should_Create_Database()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var personType = new PersonType
            {
                PersonTypeName = "Friend"
            };
            _context.PersonTypes.Add(personType);

            var company = new Company
            {
                CompanyName = "Company A"
            };
            _context.Companies.Add(company);

            company = new Company
            {
                CompanyName = "Company B"
            };
            _context.Companies.Add(company);

            var person = new Person
            {
                BirthDate = DateTime.Today,
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                Height = 6,
                PersonType = personType
            };
            person.PersonPnones.Add(new PersonPhone { PhoneNumber = "222-333-4444" });
            person.PersonPnones.Add(new PersonPhone { PhoneNumber = "333-444-5555" });
            _context.Persons.Add(person);
            company.CompanyPersons.Add(new CompanyPerson { Company = company, Person = person });

            person = new Person
            {
                BirthDate = DateTime.Today,
                FirstName = "Jane",
                LastName = "Doe",
                IsActive = true,
                Height = 5.5m
            };

            person.PersonPnones.Add(new PersonPhone { PhoneNumber = "777-888-9999" });
            person.PersonPnones.Add(new PersonPhone { PhoneNumber = "778-990-1234" });
            _context.Persons.Add(person);
            company.CompanyPersons.Add(new CompanyPerson { Company = company, Person = person });

            _context.Persons.Add(new Person
            {
                BirthDate = DateTime.Today.AddYears(-1),
                FirstName = "Jane",
                LastName = "Aimes",
                IsActive = true,
                Height = 5
            });
            _context.SaveChanges();
        }

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer("Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ContactsContext(builder.Options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
