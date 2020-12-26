using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts
{
    public class CompanyPerson
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public DateTime? StartDate { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        
    }
}
