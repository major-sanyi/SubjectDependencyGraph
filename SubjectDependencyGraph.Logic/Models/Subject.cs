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
        /// Gets or sets a value indicating whether the subject is finished.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public bool Finished { get; set; } = false;

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
    }
}
