using Newtonsoft.Json;

namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Represents a subject in a university course.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Subject"/> class.
    /// </remarks>
    /// <param name="id">The ID of the subject.</param>
    /// <param name="name">The name of the subject.</param>
    /// <param name="kredit">The credit value of the subject.</param>
    /// <param name="recommendedSemester">The recommended semester for the subject.</param>
    /// <param name="language">The language of the subject.</param>
    /// <param name="preRequisiteSubjects">The list of prerequisites for the subject.</param>
    public class Subject(string id, string name, int kredit, int recommendedSemester, string language, List<string>? preRequisiteSubjects = null)
    {
        private readonly List<Subject> _preRequisiteSubjectsSolved = [];
        private bool _finished = false;
        private bool _highlighted = false;

        /// <summary>
        /// Gets or sets the ID of the subject.
        /// </summary>
        public string Id { get; } = id;

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Gets or sets the credit value of the subject.
        /// </summary>
        public int Kredit { get; } = kredit;

        /// <summary>
        /// Gets or sets the recommended semester for the subject.
        /// </summary>
        public int RecommendedSemester { get; } = recommendedSemester;

        /// <summary>
        /// Gets or sets the language of the subject.
        /// </summary>
        public string Language { get; } = language;

        /// <summary>
        /// Gets the list of prerequisites for the subject.
        /// It's the ID of the prerequisites subjects.
        /// </summary>
        public IReadOnlyList<string> PreRequisiteSubjects { get; } = preRequisiteSubjects ?? [];

        /// <summary>
        /// Gets the list of prerequisites for the subject.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public IReadOnlyList<Subject> PreRequisiteSubjectsSolved => _preRequisiteSubjectsSolved;

        /// <summary>
        /// Gets or sets a value indicating whether the subject is finished.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public bool Finished { get => _finished; set { _finished = value; FinishedChanged(); } }

        /// <summary>
        /// Gets or sets a value indicating if the subject has been highlighted.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public bool Highlighted { get => _highlighted; private set { _highlighted = value; HighlightChanged(); } }

        /// <summary>
        /// Raises an event if finished changed
        /// </summary>
        public event Action FinishedChanged = delegate { };

        /// <summary>
        /// Raises an event if highlight changed
        /// </summary>
        public event Action HighlightChanged = delegate { };

        /// <summary>
        /// Adds the subjects to the PreRequisiteSubjectsSolved list.
        /// Only adds them if the prereqs are known inside the PreRequisiteSubjects property.
        /// </summary>
        /// <param name="subjects"></param>
        public void SolvePreREquisites(IEnumerable<Subject> subjects)
        {
            foreach (var item in subjects)
            {
                if (PreRequisiteSubjects.Contains(item.Id))
                {
                    _preRequisiteSubjectsSolved.Add(item);
                }
            }
        }

        /// <summary>
        /// Call this method to highlight it and it's dependencies.
        /// </summary>
        public void HighLightStart()
        {
            this.Highlighted = true;
            Console.WriteLine(this);
            foreach (var item in PreRequisiteSubjectsSolved)
            {
                item.HighLightStart();
            }
        }
        /// <summary>
        /// Call this method to stop highlighting it and it's dependencies.
        /// </summary>
        public void HighLightEnd()
        {
            this.Highlighted = false;
            foreach (var item in PreRequisiteSubjectsSolved)
            {
                item.HighLightEnd();

            }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            var other = obj as Subject;
            if (this.Id == other?.Id)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc/>

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name} - {Id}";
        }
    }
}
