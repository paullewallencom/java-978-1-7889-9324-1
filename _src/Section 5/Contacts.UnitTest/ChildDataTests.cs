using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Contacts.UnitTest
{
    [TestClass]
    public class ChildDataTests : TestCore
    {
        [TestMethod]
        public void ChildDataIncluded()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = _context.CompanyPersons.Include(p => p.Company).Include(p => p.Person)
                    .ThenInclude(p => p.PersonPnones)
                .Select(e => e);

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }

            var query1 = _context.CompanyPersons;
            foreach (var item in query1)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }
        }

        [TestMethod]
        public void ChildDataIncludedQuerySyntax()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = from entity in _context.CompanyPersons
                        .Include(p => p.Company)
                        .Include(p => p.Person)
                        .ThenInclude(p => p.PersonPnones)
                        orderby entity.Person.LastName
                        select entity;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }

        }

        [TestMethod]
        public void ChildDataProjected()
        {
            var query = _context.CompanyPersons
                .Select(e => new
                {
                    e.Person.LastName,
                    e.Person.FirstName,
                    Phones = e.Person.PersonPnones.OrderBy(ph => ph.PhoneNumber).Select(ph => ph.PhoneNumber),
                    e.Person.PersonType.PersonTypeName
                });

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void SelectMany()
        {
            var query = _context.Persons
                .SelectMany(e => e.PersonPnones)
                .Select(p => new
                {
                    p.PhoneNumber,
                    p.Person.FirstName,
                    p.Person.LastName
                });

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }
    }
}
