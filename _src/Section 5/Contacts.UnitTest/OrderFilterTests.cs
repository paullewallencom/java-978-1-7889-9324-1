using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class OrderFilterTests : TestCore
    {

        [TestMethod]
        public void FIlterQuerySyntax()
        {
            var query = from person in _context.Persons
                        where person.LastName.Contains("a") && person.Height > 1 && person.PersonPnones.Any()
                        select person;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        [TestMethod]
        public void FilterMethodSyntax()
        {
            var query = _context.Persons
                .Where(person => person.LastName.Contains("a") && person.Height > 1)
                .Select(e => e);
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void OrderByQuerySyntax()
        {
            var query = from person in _context.Persons
                        where person.LastName.Contains("e")
                        orderby person.LastName ascending, person.FirstName descending
                        select person;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        [TestMethod]
        public void OrderByMethodSyntax()
        {
            var query = _context.Persons
                .Where(person => person.LastName.Contains("e"))
                .OrderBy(p => p.LastName)
                .ThenByDescending(p => p.FirstName)
                .Select(e => e);
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void Quantifiers()
        {
            var query = _context.Persons.All(person => person.LastName.Contains("e"));
            Trace.WriteLine(JsonConvert.SerializeObject(query));

        }

    }
}
