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
        public int RequiredMustChoseCredit { get; set; }

        /// <summary>
        /// The required chosable credit for the syllabus.
        /// </summary>
        public int RequiredChosableCredit { get; set; }

        /// <summary>
        /// The starting semester for the syllabus specialization.
        /// </summary>
        public int StartingSpecSemester { get; set; }

        /// <summary>
        /// The list of specializations for the syllabus.
        /// </summary>
        public HashSet<Specialisation> Specialisations { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Syllabus()
        {
            RequiredMustChoseCredit = 0;
            RequiredChosableCredit = 0;
            StartingSpecSemester = 0;
            Specialisations = [];
            ResolveSubjectPreReq();
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects from selected specs.
        /// </summary>
        /// <param name="selectedSpecs">SelectedSpecs. If left null it doesn't filter</param>
        /// <returns></returns>
        public List<Subject> GetSubjectsWithSpec(string[]? selectedSpecs = null)
        {
            return [.. Subjects, .. FilterSpecSubjects(selectedSpecs)];
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects from selected specs. Specialisations marked as true.
        /// </summary>
        ///<param name="selectedSpecs"> Limits the specifications. If <see langword="null"/> subjects from all specs will be returned.</param>
        /// <returns></returns>
        public Dictionary<Subject, bool> GetSubjectsWithSpecMarked(string[]? selectedSpecs = null)
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

        private void ResolveSubjectPreReq()
        {
            List<Subject> subjects = GetSubjectsWithSpecMarked().Select(x => x.Key).ToList();
            foreach (var subject in subjects)
            {
                if (subject.PreRequisiteSubjects != null)
                {
                    IEnumerable<Subject> preReqs = subjects.Where(x => subject.PreRequisiteSubjects.Contains(x.Id));
                    subject.SolvePreREquisites(preReqs);
                }
            }
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
