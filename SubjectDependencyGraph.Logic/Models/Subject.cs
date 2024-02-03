using Newtonsoft.Json;

namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Represents a subject in a university course.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Subject"/> class.
    /// </remarks>
    public class Subject()
    {
        private bool _finished = false;
        private bool _highlighted = false;
        private bool preReqsSolved = false;
        private List<string>? preRequisiteSubjects = [];

        /// <summary>
        /// Gets or sets the ID of the subject.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        public string Name { get; set; } = "To be Filled";

        /// <summary>
        /// Gets or sets the credit value of the subject.
        /// </summary>
        public int Kredit { get; set; } = 0;

        /// <summary>
        /// Gets or sets the recommended semester for the subject.
        /// </summary>
        public int RecommendedSemester { get; set; } = 0;

        /// <summary>
        /// Gets or sets the language of the subject.
        /// </summary>
        public string Language { get; set; } = "Hungarian";

        /// <summary>
        /// Gets the list of prerequisites for the subject.
        /// It's the ID of the prerequisites subjects.
        /// </summary>
        public List<string>? PreRequisiteSubjects
        {
            get
            {
                if (!preReqsSolved)
                {
                    return preRequisiteSubjects;
                }
                else
                {
                    return [.. PreRequisiteSubjectsSolved.Select(x => x.Id)];
                }
            }
            set
            {
                if (!preReqsSolved)
                    preRequisiteSubjects = value;
                else
                {
                    Console.Error.WriteLine("Prerequisites are already solved in model, use PreRequisiteSubjectsSolved");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializePreRequisiteSubjects()
        {
            return PreRequisiteSubjects?.Count == 0;
        }
        /// <summary>
        /// Gets the list of prerequisites for the subject.
        /// </summary>
        [JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public HashSet<Subject> PreRequisiteSubjectsSolved { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether the subject is finished.
        /// </summary>
        [JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public bool Finished { get => _finished; set { _finished = value; FinishedChanged(); } }

        /// <summary>
        /// Gets or sets a value indicating if the subject has been highlighted.
        /// </summary>
        [JsonIgnore]
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
            if (preRequisiteSubjects != null)
            {
                foreach (var item in subjects)
                {
                    if (preRequisiteSubjects.Contains(item.Id))
                    {
                        PreRequisiteSubjectsSolved.Add(item);
                    }
                }
            }
            preReqsSolved = true;
        }

        /// <summary>
        /// Call this method to highlight it and it's dependencies.
        /// </summary>
        public void HighLightStart()
        {
            this.Highlighted = true;
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
