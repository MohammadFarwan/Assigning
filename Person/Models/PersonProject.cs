namespace Person.Models
{
    public class PersonProject
    {
        public int PersonId { get; set; }
        public Persone Person { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
