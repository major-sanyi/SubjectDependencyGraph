namespace SubjectDependencyGraph.Shared.Models
{
    /// <summary>
    /// Dto
    /// </summary>
    /// <param name="FromSyllabus"></param>
    /// <param name="ToSyllabus"></param>
    /// <param name="Subjects"></param>
    public record EqualTableDto(string FromSyllabus, string ToSyllabus, Dictionary<string, string[]> Subjects);


    /// <summary>
    /// Class for handling subjects equality between syllabi.
    /// </summary>
    public class EqualTable
    {
        /// <summary>
        /// Creates an Equal Table from a dto and the syllabi list.
        /// </summary>
        /// <param name="dto">The dto for the equality table.</param>
        /// <param name="syllabi">The list of syllabi.</param>
        /// <exception cref="InvalidOperationException"> Throws if the correct syllabi not present.</exception>
        public EqualTable(EqualTableDto dto, HashSet<Syllabus> syllabi)
        {
            Syllabus fromSyllabus = syllabi.First(x => dto.FromSyllabus == x.Id);
            Syllabus toSyllabus = syllabi.First(x => dto.ToSyllabus == x.Id);
            FromSyllabus = fromSyllabus;
            ToSyllabus = toSyllabus;

            // If you think about it it's pretty simple. Just getting the needed subject. And the reference for the other.
            // Not that simple considering I fucked up first.
            Subjects = dto.Subjects.Select(subjectStringArray =>
            (neededSubject: toSyllabus.GetSubjectsWithSpec().First(y => y.Id == subjectStringArray.Key),
            requiredSubjects: fromSyllabus.GetSubjectsWithSpec().Where(Subject => subjectStringArray.Value.Contains(Subject.Id)).ToHashSet()))
            .ToDictionary(x => x.neededSubject, x => x.requiredSubjects);
        }
        /// <summary>
        /// Constructor for creating new EqualTable.
        /// </summary>
        /// <param name="fromSyllabus"></param>
        /// <param name="toSyllabus"></param>
        public EqualTable(Syllabus fromSyllabus, Syllabus toSyllabus)
        {
            FromSyllabus = fromSyllabus;
            ToSyllabus = toSyllabus;
            Subjects = new Dictionary<Subject, HashSet<Subject>>();
        }


        /// <summary>
        /// The Syllabus we want to accredit subjects from.
        /// </summary>
        public Syllabus FromSyllabus { get; }

        /// <summary>
        /// The syllabus we want to get credit.
        /// </summary>
        public Syllabus ToSyllabus { get; }

        /// <summary>
        /// The table of equvalence
        /// The key is the subject we want, and the list are the required subjects.
        /// </summary>
        public IReadOnlyDictionary<Subject, HashSet<Subject>> Subjects { get; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not EqualTable equalTable) return false;
            return FromSyllabus.Equals(equalTable.FromSyllabus) && ToSyllabus.Equals(equalTable.ToSyllabus);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return FromSyllabus.GetHashCode() ^ ToSyllabus.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{FromSyllabus} -> {ToSyllabus}";
        }

    }
}
