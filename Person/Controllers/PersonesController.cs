using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Person.Models.DTO;
using Person.Repositories.Interfaces;

namespace Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonesController : ControllerBase
    {
        private readonly IPerson _person;

        public PersonesController(IPerson context)
        {
            _person = context;
        }

        // GET: api/Persones
        [Route("/person/GetAllPersons")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persone>>> Getpersons()
        {
          return await _person.GetAllPerson();
        }

        // GET: api/Persones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persone>> GetPersone(int id)
        {
          return await _person.GetPersonById(id);
        }

        // PUT: api/Persones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersone(int id, Persone persone)
        {
            var updatePerson = await _person.UpdatePerson(id, persone);
            return Ok(updatePerson);
        }

        // POST: api/Persones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Persone>> PostPersone(PersonDtoRequest persone)
        {
            var createdPerson = await _person.CreatePerson(persone);
            return CreatedAtAction(nameof(GetPersone), new { id = createdPerson.Id }, createdPerson);
        }


        // DELETE: api/Persones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersone(int id)
        {
            var deletePerson =  await _person.DeletePerson(id);
            return Ok(deletePerson);
        }

        // POST: api/Persones/{personId}/projects/{projectId}
        [HttpPost("{personId}/projects/{projectId}")]
        public async Task<IActionResult> AssignProjectToPerson(int personId, int projectId)
        {
            await _person.AssignProjectToPerson(personId, projectId);
            return Ok("Assign Project To Person Successefully");
        }

        // GET: api/Persones/{personId}/projects
        [HttpGet("{personId}/projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByPersonId(int personId)
        {
            var projects = await _person.GetProjectsByPersonId(personId);
            return Ok(projects);
        }
    }
}
