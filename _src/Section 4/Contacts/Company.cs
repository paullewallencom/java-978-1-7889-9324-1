using System;
using System.Collections.Generic;

namespace Contacts
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual ICollection<CompanyPerson> CompanyPersons { get; set; } = new HashSet<CompanyPerson>();
    }
}
