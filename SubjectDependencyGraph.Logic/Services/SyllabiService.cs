using Newtonsoft.Json;
using SubjectDependencyGraph.Shared.Exceptions;
using SubjectDependencyGraph.Shared.Models;

namespace SubjectDependencyGraph.Shared.Services
{
    /// <summary>
    /// A service for managing syllabi and specializations.
    /// </summary>
    public class SyllabiService : ISyllabiService
    {
        /// <inheritdoc/>
        public List<Syllabus> Syllabi => _syllabi;

        /// <inheritdoc/>
        public IReadOnlyList<EqualTable> EqualityTables { get; private set; }

        private readonly static List<Syllabus> defaultSyllabi = JsonConvert.DeserializeObject<List<Syllabus>>(Resources.Resource.OENIK_E) ?? [];
        // Needs rework lol
        private readonly static List<EqualTableDto> defaultEquals = JsonConvert.DeserializeObject<List<EqualTableDto>>(Resources.Resource.OENIK_E_equals) ?? [];

        private readonly List<Syllabus> _syllabi;

        /// <inheritdoc/>
        public SyllabiService() : this(defaultSyllabi, defaultEquals)
        {

        }

        /// <inheritdoc/>
        public SyllabiService(Dictionary<string, List<string>> completedSubjects) : this(defaultSyllabi, defaultEquals, completedSubjects)
        {

        }

        /// <inheritdoc/>
        public SyllabiService(List<Syllabus> avalaibleSyllabi, List<EqualTableDto> equalTables, Dictionary<string, List<string>>? completedSubjects = null)
        {
            _syllabi = avalaibleSyllabi;
            if (completedSubjects != null)
            {
                ImportCompletedSubjects(completedSubjects);
            }
            // Check if all subjects are unique per syllabus
            foreach (var syllabus in _syllabi)
            {
                if (syllabus.Subjects.Distinct().Count() != syllabus.Subjects.Count)
                {
                    throw new SyllabusContainsDuplicateSubjectsException(syllabus);
                }
                foreach (var spec in syllabus.Specialisations)
                {
                    if (spec.Subjects.Distinct().Count() != spec.Subjects.Count)
                    {
                        throw new SyllabusContainsDuplicateSubjectsException(spec);
                    }
                }
            }
            EqualityTables = equalTables.Select(x => new EqualTable(x, _syllabi)).ToList();
        }

        /// <inheritdoc/>
        public void AddSyllabus(Syllabus syllabus)
        {
            _syllabi.Add(syllabus);
        }

        /// <inheritdoc/>
        public Dictionary<string, List<string>> ExportCompletedSubjects()
        {
            Dictionary<string, List<string>> completedSubjects = [];
            foreach (var syllabus in _syllabi)
            {
                completedSubjects
                    .Add(syllabus.Id,
                    GetAllSubjectsFromSyllabi(syllabus.Id).Where(x => x.Finished).Select(x => x.Id).ToList());
            }
            return completedSubjects;
        }

        /// <inheritdoc/>
        public void ClearFinishStatus()
        {
            foreach (var item in _syllabi.SelectMany(x => x.GetSubjectsWithSpec()))
            {
                item.Finished = false;
            }
        }
        /// <inheritdoc/>
        public void ImportCompletedSubjects(Dictionary<string, List<string>> completedSubjects)
        {
            // Reset completed
            ClearFinishStatus();
            foreach (KeyValuePair<string, List<string>> keyValue in completedSubjects)
            {
                foreach (var item in GetAllSubjectsFromSyllabi(keyValue.Key)
                    .Where(x => keyValue.Value.Contains(x.Id)))
                {
                    item.Finished = true;
                }
            }
        }

        /// <summary>
        /// Gets all avalaible subjects including specialisation subjects.
        /// It could contain duplicates, if 1 subject is contained in multiple specs.
        /// </summary>
        /// <param name="syllabusID"></param>
        /// <returns></returns>
        private List<Subject> GetAllSubjectsFromSyllabi(string syllabusID)
        {
            return _syllabi.First(x => x.Id == syllabusID).GetSubjectsWithSpec();
        }
    }
}
