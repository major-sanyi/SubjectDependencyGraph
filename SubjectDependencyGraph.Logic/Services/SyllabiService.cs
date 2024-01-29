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
        public IReadOnlyList<Syllabus> Syllabi => _syllabi;

        /// <inheritdoc/>
        public IReadOnlyList<Specialisation> Specialisations => SelectedSyllabus.Specialisations;

        /// <inheritdoc/>
        public Syllabus SelectedSyllabus { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyList<Specialisation> SelectedSpecialisations => _selectedSpecialisations;

        private readonly static List<Syllabus> defaultSyllabi = JsonConvert.DeserializeObject<List<Syllabus>>(Resources.Resource.OENIK_E) ?? [];

        private readonly List<Syllabus> _syllabi;
        private List<Specialisation> _selectedSpecialisations;

        /// <inheritdoc/>
        public SyllabiService() : this(defaultSyllabi)
        {

        }

        /// <inheritdoc/>
        public SyllabiService(Dictionary<string, List<string>> completedSubjects) : this(defaultSyllabi, completedSubjects)
        {

        }

        /// <inheritdoc/>
        public SyllabiService(List<Syllabus> avalaibleSyllabi, Dictionary<string, List<string>>? completedSubjects = null)
        {
            _syllabi = avalaibleSyllabi;
            SelectedSyllabus = _syllabi[0];
            _selectedSpecialisations = [];
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
        }

        /// <inheritdoc/>
        public void SelectSyllabus(string syllabusId)
        {
            SelectedSyllabus = _syllabi.First(x => x.Id == syllabusId);
            _selectedSpecialisations = [];
        }

        /// <inheritdoc/>
        public void SelectSpecialisation(string specId)
        {
            if (!_selectedSpecialisations.Any(x => x.Id == specId))
                _selectedSpecialisations.Add(SelectedSyllabus.Specialisations.First(x => x.Id == specId));
        }

        /// <inheritdoc/>
        public void SelectMultipleSpecialisations(string[] specId)
        {
            _selectedSpecialisations.AddRange(SelectedSyllabus.Specialisations.Where(x => specId.Contains(x.Id)));
        }

        /// <inheritdoc/>
        public void AddSyllabus(Syllabus syllabus)
        {
            _syllabi.Add(syllabus);
        }

        /// <inheritdoc/>
        public Dictionary<string, List<string>> ExportData()
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

        private void ImportCompletedSubjects(Dictionary<string, List<string>> completedSubjects)
        {
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
        private IReadOnlyList<Subject> GetAllSubjectsFromSyllabi(string syllabusID)
        {
            return _syllabi.First(x => x.Id == syllabusID).GetSubjectsWithSpec();
        }
    }
}
