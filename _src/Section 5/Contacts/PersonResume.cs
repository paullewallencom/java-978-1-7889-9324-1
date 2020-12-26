using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts
{
    public class PersonResume
    {
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public string ResumeText { get; set; }
    }
}
