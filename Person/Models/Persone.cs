namespace Person.Models
{
    public class Persone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PersonProject> PersonProjects { get; set; } 
    }
}
