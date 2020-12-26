using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private ContactsContext _context;
        public PersonsController([FromServices]ContactsContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<PersonInfo>), 200)]
        public IActionResult Get()
        {
            var data = _context.Persons
                .OrderBy(one => one.LastName)
                .Select(one => new PersonInfo
                {
                    PersonId = one.PersonId,
                    FirstName = one.FirstName,
                    LastName = one.LastName
                }).ToList();
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}", Name = "GetPerson")]
        [ProducesResponseType(typeof(Person), 200)]
        public IActionResult Get(int id)
        {
            var data = _context.Persons.Include(p => p.PersonPnones)
                .FirstOrDefault(one => one.PersonId == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
            return CreatedAtRoute("GetPerson", new { id = person.PersonId }, person);
        }

        // PUT api/values/5
        [HttpPut("")]
        [Route("")]
        public IActionResult Put([FromBody]Person person)
        {
            //var data = _context.Persons.Include(p => p.PersonPnones)
            //    .FirstOrDefault(one => one.PersonId == person.PersonId);
            //if (data == null)
            //{
            //    return NotFound();
            //}
            //data.LastName = person.LastName;
            _context.Update(person);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var data = _context.Persons.Include(p => p.PersonPnones)
            //    .FirstOrDefault(one => one.PersonId == id);
            var data = new Person { PersonId = id };
            _context.Remove(data);
            //data.PersonPnones.ToList().ForEach(p => _context.Remove(p));
            _context.SaveChanges();
            return NoContent();
        }
    }
}
