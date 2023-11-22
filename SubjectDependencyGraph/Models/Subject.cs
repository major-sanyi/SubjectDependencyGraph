
using System.Text.Json.Serialization;

namespace SubjectDependencyGraph.Models
{ 
   /// <summary>
    /// Represents a subject in a university course.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Gets or sets the ID of the subject.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the credit value of the subject.
        /// </summary>
        public string Kredit { get; set; }

        /// <summary>
        /// Gets or sets the recommended semester for the subject.
        /// </summary>
        public string RecommendedSemester { get; set; }

        /// <summary>
        /// Gets or sets the ID of the syllabus for the subject.
        /// </summary>
        public string SyllabusId { get; set; }

        /// <summary>
        /// Gets or sets the language of the subject.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets the list of prerequisites for the subject.
        /// It's the ID of the prerequisites subjects.
        /// </summary>
        public List<string> PreRequisiteSubjects { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the subject is finished.
        /// </summary>
        [JsonIgnore]
        public bool Finished { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Subject"/> class.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <param name="name">The name of the subject.</param>
        /// <param name="kredit">The credit value of the subject.</param>
        /// <param name="recommendedSemester">The recommended semester for the subject.</param>
        /// <param name="syllabusId">The ID of the syllabus for the subject.</param>
        /// <param name="language">The language of the subject.</param>
        /// <param name="preRequisiteSubjects">The list of prerequisites for the subject.</param>
        public Subject(string id, string name, string kredit, string recommendedSemester, string syllabusId, string language, List<string>? preRequisiteSubjects = null)
        {
            Id = id;
            Name = name;
            Kredit = kredit;
            RecommendedSemester = recommendedSemester;
            SyllabusId = syllabusId;
            Language = language;
            PreRequisiteSubjects = preRequisiteSubjects ?? new List<string>();
        }
    }
}
