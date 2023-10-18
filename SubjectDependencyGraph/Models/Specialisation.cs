
namespace SubjectDependencyGraph.Models
{
    public class Specialisation: SyllabusParent
    {
        public object StartingSpecSemester { get; set; }

        public Specialisation(string id, string name, string length, object startingSpecSemester, List<Subject>? subjects=null):base(id,name,length,subjects)
        {
            Id = id;
            Name = name;
            Length = length;
            StartingSpecSemester = startingSpecSemester;
        }
    }
}
