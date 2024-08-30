namespace Person.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public ICollection<PersonProject> PersonProjects { get; set; } = new List<PersonProject>();
    }
}
