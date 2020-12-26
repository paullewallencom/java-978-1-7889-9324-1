using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
