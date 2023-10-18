
namespace SubjectDependencyGraph.Models
{
    public class Syllabus : SyllabusParent
    {

        public string MustChoseCredit { get; set; }
        public string ChosableCredit { get; set; }
        public string StartingSpecSemester { get; set; }
        public List<Specialisation> Specialisations { get; }

        public Syllabus(string id, string name, string length, string mustChoseCredit, string chosableCredit, string startingSpecSemester, List<Specialisation>? specialisations = null, List<Subject>? subjects = null) : base(id, name, length, subjects)
        {
            Id = id;
            Name = name;
            Length = length;
            MustChoseCredit = mustChoseCredit;
            ChosableCredit = chosableCredit;
            StartingSpecSemester = startingSpecSemester;
            Specialisations = specialisations ?? new List<Specialisation>();
        }

    }
}
