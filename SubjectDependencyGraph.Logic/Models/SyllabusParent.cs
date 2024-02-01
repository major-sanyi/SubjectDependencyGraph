namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Abstract class for modelling university course syllabuses.
    /// </summary>
    public abstract class SyllabusParent
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
        public IReadOnlyList<Subject> Subjects { get; }

        /// <summary>
        /// Constructor for the SyllabusParent class.
        /// </summary>
        /// <param name="id">Unique identifier for the syllabus.</param>
        /// <param name="name">Name of the syllabus.</param>
        /// <param name="length">Length of the syllabus.</param>
        /// <param name="subjects">List of subjects included in the syllabus.</param>
        protected SyllabusParent(string id, string name, int length, List<Subject>? subjects = null)
        {
            Id = id;
            Name = name;
            Length = length;
            Subjects = subjects ?? [];
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name} - {Id}";
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Id.Equals(obj);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
