

namespace Contacts.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using System;

    namespace Contacts.UnitTest
    {
        [TestClass]
        public class ProjectionTests : TestCore
        {
            [TestMethod]
            public void QuerySyntax()
            {
                var query = from person in _context.Persons
                            orderby person.LastName
                            select new
                            {
                                person.LastName,
                                person.FirstName,
                                person.PersonId
                            };

                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }


            }

            [TestMethod]
            public void MethodSyntax()
            {
                var query = _context.Persons.OrderBy(p => p.LastName)
                     .Select(person => new {
                         person.LastName,
                         person.FirstName,
                         person.PersonId
                     });
                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }
            }

            [TestMethod]
            public void QuerySyntaxExplicitType()
            {
                var query = from person in _context.Persons
                            orderby person.LastName
                            select new PersonInfo
                            {
                                LastName = person.LastName,
                                FirstName = person.FirstName,
                                PersonId = person.PersonId
                            };

                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }
            }

            [TestMethod]
            public void MethodSyntaxExplicitType()
            {
                var query = _context.Persons.OrderBy(p => p.LastName)
                      .Select(person => new PersonInfo {
                          LastName = person.LastName,
                          FirstName = person.FirstName,
                          PersonId = person.PersonId
                      });
                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }
            }

            [TestMethod]
            public void SingleField()
            {
                var query = _context.Persons.OrderBy(p => p.LastName)
                      .Select(person => person.LastName);
                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }

            }

            [TestMethod]
            public void Distinct()
            {
                var query = _context.Persons.OrderBy(p => p.LastName)
                      .Select(person => person.LastName).Distinct();
                foreach (var item in query)
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(item));
                }
            }
        }
    }
}
