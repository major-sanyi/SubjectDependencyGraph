namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Represents a specialization within a syllabus.
    /// </summary>
    public class Specialisation : SyllabusParent
    {
        /// <summary>
        /// The starting semester for this specialization.
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="Specialisation"/> class.
        /// </summary>
        /// <param name="id">The ID of the specialization.</param>
        /// <param name="name">The name of the specialization.</param>
        /// <param name="length">The length of the specialization.</param>
        /// <param name="subjects">The list of subjects for this specialization.</param>
        public Specialisation(string id, string name, int length, List<Subject>? subjects = null) : base(id, name, length, subjects)
        {
            Id = id;
            Name = name;
            Length = length;
        }
    }
}
