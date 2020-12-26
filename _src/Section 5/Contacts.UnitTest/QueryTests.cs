using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class QueryTests : TestCore
    {

        [TestMethod]
        public void QuerySyntax()
        {
            var query = from person in _context.Persons
                        select person;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void MethodSyntax()
        {
            var query = _context.Persons.Select(e => e);
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecordFunctions()
        {
            var item = _context.Persons.Find(1);
            Trace.WriteLine(JsonConvert.SerializeObject(item));

            item = _context.Persons.FirstOrDefault();
            Trace.WriteLine(JsonConvert.SerializeObject(item));

            item = _context.Persons.Single();
            Trace.WriteLine(JsonConvert.SerializeObject(item));
        }


       
    }
}
