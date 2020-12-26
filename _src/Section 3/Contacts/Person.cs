using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts
{

    public class Person
    {

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<PersonPhone> PersonPnones { get; set; } = new HashSet<PersonPhone>();

        public int? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; }

        public virtual ICollection<CompanyPerson> CompanyPersons { get; set; } = new HashSet<CompanyPerson>();

        public virtual PersonResume PersonResume { get; set; }
    }

}
