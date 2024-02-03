using Newtonsoft.Json;
using System.Collections;

namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Abstract class for modelling university course syllabuses.
    /// </summary>
    [JsonObject]
    public abstract class SyllabusParent : IEnumerable<Subject>
    {
        /// <summary>
        /// Unique identifier for the syllabus.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the syllabus.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Length of the syllabus.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// List of subjects included in the syllabus.
        /// </summary>
        public HashSet<Subject> Subjects { get; set; }

        /// <summary>
        /// Constructor for SyllabusParent.
        /// </summary>
        protected SyllabusParent()
        {
            Id = Guid.NewGuid().ToString();
            Name = "To be Filled";
            Length = 0;
            Subjects = [];
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            var other = obj as SyllabusParent;
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

        /// <inheritdoc/>
        public IEnumerator<Subject> GetEnumerator()
        {
            return Subjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
