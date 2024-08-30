using Person.Data;
using Person.Models;
using Person.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Person.Data.Person.Data;
using Person.Models.DTO;

namespace Person.Repositories.Services
{
    public class PersonService : IPerson
    {
        private readonly AppDbContext _context;

        public PersonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Persone> CreatePerson(PersonDtoRequest personDto)
        {
            var person = new Persone()
            {
                //Id = personDto.Id,
                Name = personDto.Name,
                Description = personDto.Description,
            };

            try
            {
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
                return person;
            }
            catch (Exception ex)
            {
                // Log the error (consider using a logging framework)
                throw new Exception("An error occurred while creating the person.", ex);
            }
        }
        public async Task<Persone> DeletePerson(int personId)
        {
            var getPeson = await GetPersonById(personId);
            _context.Persons.Remove(getPeson);
            await _context.SaveChangesAsync();
            return getPeson;
        }

        public async Task<List<Persone>> GetAllPerson()
        {
            var allPerson = await _context.Persons.ToListAsync();
            return allPerson;
        }

        public async Task<Persone> GetPersonById(int personId)
        {
            var person = await _context.Persons.FindAsync(personId);
            return person;
        }

        public async Task<Persone> UpdatePerson(int personId, Persone person)
        {
            var existingPerson = await _context.Persons.FindAsync(personId);
            if (existingPerson != null)
            {
                existingPerson.Name = person.Name;
                existingPerson.Description = person.Description;
                await _context.SaveChangesAsync();
            }
            return existingPerson;
        }

        // New methods for managing projects
        public async Task AssignProjectToPerson(int personId, int projectId)
        {
            var personProject = new PersonProject
            {
                PersonId = personId,
                ProjectId = projectId
            };

            _context.PersonProjects.Add(personProject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjectsByPersonId(int personId)
        {
            var projects = await _context.PersonProjects
                                         .Where(pp => pp.PersonId == personId)
                                         .Select(pp => pp.Project)
                                         .ToListAsync();

            return projects;
        }


        //public async Task<List<Project>> GetProjectsByPersonId(int personId)
        //{
        //    var projects = await _context.Persons
        //                                 .Where(p => p.Id == personId)
        //                                 .Include(p => p.PersonProjects)
        //                                     .ThenInclude(pp => pp.Project)
        //                                 .SelectMany(p => p.PersonProjects.Select(pp => pp.Project))
        //                                 .ToListAsync();

        //    return projects;
        //}
    }
}
