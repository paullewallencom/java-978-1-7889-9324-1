using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class AggregationsTests : TestCore
    {

        [TestMethod]
        public void MinTest()
        {
            var minHeight = (from peson in _context.Persons select peson).Min(p => p.Height);
            Trace.WriteLine(JsonConvert.SerializeObject(minHeight));
        }

        [TestMethod]
        public void MaxTest()
        {
            var maxHeight = _context.Persons.Where(p => p.IsActive).Max(p => p.Height);
            Trace.WriteLine(JsonConvert.SerializeObject(maxHeight));
        }



        [TestMethod]
        public void SumTest()
        {
            var totalActive = _context.Persons.Sum(p => p.Height);

            Trace.WriteLine(JsonConvert.SerializeObject(totalActive));

        }

        [TestMethod]
        public void AverageTest()
        {
            var totalActive = _context.Persons.Average(p => p.Height);

            Trace.WriteLine(JsonConvert.SerializeObject(totalActive));

        }

        [TestMethod]
        public void CountTest()
        {
            var countActive = _context.Persons.Count(p => p.IsActive && p.Height > 1);
        }


        [TestMethod]
        public void GroupQueryTest()
        {

            var query = from person in _context.Persons
                        group person by person.LastName into lastNameGroup
                        select lastNameGroup;

            foreach (var item in query)
            {
                Trace.WriteLine(item.Key + " " + JsonConvert.SerializeObject(item));
            }


        }

        [TestMethod]
        public void GroupMethodTest()
        {
            var query = _context.Persons.GroupBy(p => p.LastName);

            foreach (var item in query)
            {
                Trace.WriteLine(item.Key + " " + JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void GroupByMultipleFieldsTest()
        {
            var query = from person in _context.Persons
                        group person by new { person.LastName, person.IsActive }  into lastNameGroup
                        select lastNameGroup;

            foreach (var item in query)
            {
                Trace.WriteLine(item.Key + " " + JsonConvert.SerializeObject(item));
            }
        }
    }
}
