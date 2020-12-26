using System.Collections.Generic;

namespace Contacts
{
    public class PersonType
    {

        public int PersonTypeId { get; set; }
        public string PersonTypeName { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
