namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// This class represents a syllabus used for modelling university courses.
    /// </summary>
    public class Syllabus : SyllabusParent
    {
        /// <summary>
        /// The required must-choose credit for the syllabus.
        /// </summary>
        public int RequiredMustChoseCredit { get; }

        /// <summary>
        /// The required chosable credit for the syllabus.
        /// </summary>
        public int RequiredChosableCredit { get; }

        /// <summary>
        /// The starting semester for the syllabus specialization.
        /// </summary>
        public int StartingSpecSemester { get; }

        /// <summary>
        /// The list of specializations for the syllabus.
        /// </summary>
        public IReadOnlyList<Specialisation> Specialisations { get; }

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
        public Syllabus(string id, string name, int length, int requiredMustChoseCredit, int requiredChosableCredit, int startingSpecSemester, List<Specialisation>? specialisations = null, List<Subject>? subjects = null) : base(id, name, length, subjects)
        {
            Id = id;
            Name = name;
            Length = length;
            RequiredMustChoseCredit = requiredMustChoseCredit;
            RequiredChosableCredit = requiredChosableCredit;
            StartingSpecSemester = startingSpecSemester;
            Specialisations = specialisations ?? [];
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects from selected specs.
        /// </summary>
        /// <param name="selectedSpecs">SelectedSpecs. If left null it doesn't filter</param>
        /// <returns></returns>
        public IReadOnlyList<Subject> GetSubjectsWithSpec(string[]? selectedSpecs = null)
        {
            return [.. Subjects, .. FilterSpecSubjects(selectedSpecs)];
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects. Specialisations marked as true.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<Subject, bool> GetSubjectsWithSpecMarked()
        {
            return GetAllSubjectsMarked();
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects from selected specs. Specialisations marked as true.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<Subject, bool> GetSubjectsWithSpecMarked(string[] selectedSpecs)
        {
            return GetAllSubjectsMarked(selectedSpecs);
        }


        private Dictionary<Subject, bool> GetAllSubjectsMarked(string[]? selectedSpecs = null)
        {
            Dictionary<Subject, bool> output = [];

            // If the main syllabus contains a Subject in the Spec...
            // My question would be why is that subject in a spec.
            foreach (var subject in Subjects)
            {
                output[subject] = false;
            }
            foreach (var subject in FilterSpecSubjects(selectedSpecs))
            {
                output[subject] = true;
            }
            return output;
        }
        private IEnumerable<Subject> FilterSpecSubjects(string[]? selectedSpecs = null)
        {
            IEnumerable<Subject> specSubjects;
            if (selectedSpecs == null)
            {
                specSubjects = Specialisations.SelectMany(x => x.Subjects);
            }
            else
            {
                specSubjects = Specialisations.Where(x => selectedSpecs.Contains(x.Id)).SelectMany(x => x.Subjects);
            }
            return specSubjects;
        }
    }
}
