using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ContactsContext context;

        public ValuesController(ContactsContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var newPerson = new Person
            {
                FirstName = "Sergey",
                LastName = "Barskiy"
            };
            context.Persons.Add(newPerson);

            await context.SaveChangesAsync();


            return context.Persons.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
