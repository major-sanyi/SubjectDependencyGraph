
namespace SubjectDependencyGraph.Models
{
    public class SyllabusParent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public List<Subject> Subjects { get; }

        protected SyllabusParent(string id, string name, string length, List<Subject>? subjects = null)
        {
            Id = id;
            Name = name;
            Length = length;
            Subjects = subjects = new List<Subject>();
        }
    }
}
