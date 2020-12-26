using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Contacts.UnitTest
{
    [TestClass]
    public class PerformanceConsiderations : TestCore
    {
        [TestMethod]
        public void ClientSideQueries()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = from entity in _context.Persons select entity;

            var sortedQuery = from entity in query
                              orderby entity.LastName
                              select entity;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }

            var tableQuery = _context.Persons.ToList();

            var sortedOnClient = from entity in tableQuery
                                 orderby entity.LastName
                                 select entity;

        }


        [TestMethod]
        public void AsNoTracking()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = _context.Persons.AsNoTracking().Include(p => p.PersonPnones)
                .Select(e => e);

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }

        }

        [TestMethod]
        public void AsNoTrackingQuerySyntax()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = from entity in _context.Persons.AsNoTracking().Include(p => p.PersonPnones)
                        orderby entity.LastName
                        select entity;

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
            }

        }



        [TestMethod]
        public void LazyLoading()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var query = _context.Persons.Select(e => e);

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
                // when lazy loading is done asccess to item.PersonPnones 
                // will auto populate the navigation property 
                Trace.WriteLine(JsonConvert.SerializeObject(item.PersonPnones, Formatting.Indented, settings));
            }

        }
    }
}
