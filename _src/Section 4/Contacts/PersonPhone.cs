using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts
{
    public class PersonPhone
    {
        public int PersonPhoneId { get; set; }
        public string PhoneNumber { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
