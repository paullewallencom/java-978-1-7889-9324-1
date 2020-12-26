using Microsoft.EntityFrameworkCore;

namespace Contacts
{
    public class ContactsContext: DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options): base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}