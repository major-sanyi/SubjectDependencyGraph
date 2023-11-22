
namespace SubjectDependencyGraph.Models
{
    /// <summary>
    /// This class represents a syllabus used for modelling university courses.
    /// </summary>
    public class Syllabus : SyllabusParent
    {
        /// <summary>
        /// The required must-choose credit for the syllabus.
        /// </summary>
        public string RequiredMustChoseCredit { get; set; }

        /// <summary>
        /// The required chosable credit for the syllabus.
        /// </summary>
        public string RequiredChosableCredit { get; set; }

        /// <summary>
        /// The starting semester for the syllabus specialization.
        /// </summary>
        public string StartingSpecSemester { get; set; }

        /// <summary>
        /// The list of specializations for the syllabus.
        /// </summary>
        public List<Specialisation> Specialisations { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Syllabus"/> class.
        /// </summary>
        /// <param name="id">The syllabus ID.</param>
        /// <param name="name">The syllabus name.</param>
        /// <param name="length">The syllabus length.</param>
        /// <param name="requiredMustChoseCredit">The required must-choose credit for the syllabus.</param>
        /// <param name="requiredChosableCredit">The required chosable credit for the syllabus.</param>
        /// <param name="startingSpecSemester">The starting semester for the syllabus specialization.</param>
        /// <param name="specialisations">The list of specializations for the syllabus.</param>
        /// <param name="subjects">The list of subjects for the syllabus.</param>
        public Syllabus(string id, string name, string length, string requiredMustChoseCredit, string requiredChosableCredit, string startingSpecSemester, List<Specialisation>? specialisations = null, List<Subject>? subjects = null) : base(id, name, length, subjects)
        {
            Id = id;
            Name = name;
            Length = length;
            RequiredMustChoseCredit = requiredMustChoseCredit;
            RequiredChosableCredit = requiredChosableCredit;
            StartingSpecSemester = startingSpecSemester;
            Specialisations = specialisations ?? new List<Specialisation>();
        }
    }
}
