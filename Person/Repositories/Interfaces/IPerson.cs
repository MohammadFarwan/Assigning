using Person.Models;
using Person.Models.DTO;

namespace Person.Repositories.Interfaces
{
    public interface IPerson
    {
        Task<Persone> CreatePerson(PersonDtoRequest person);
        Task<List<Persone>> GetAllPerson();
        Task<Persone> GetPersonById(int personId);
        Task<Persone> UpdatePerson(int personId, Persone person);
        Task<Persone> DeletePerson(int personId);

        // New methods for handling projects
        Task AssignProjectToPerson(int personId, int projectId);
        Task<List<Project>> GetProjectsByPersonId(int personId);

    }
}
