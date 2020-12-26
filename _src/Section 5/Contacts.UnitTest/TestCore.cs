using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contacts.UnitTest
{
    [TestClass]
    public abstract class TestCore
    {
        protected ContactsContext _context;

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
