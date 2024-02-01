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
    /// 
    /// </summary>
    public class EqualTable
    {
        public EqualTable(EqualTableDto dto, List<Syllabus> syllabi)
        {

            Syllabus fromSyllabus = syllabi.First(x => dto.FromSyllabus == x.Id);
            Syllabus toSyllabus = syllabi.First(x => dto.ToSyllabus == x.Id);

            FromSyllabus = fromSyllabus;
            ToSyllabus = toSyllabus;

            // If you think about it it's pretty simple. Just getting the needed subject. And the reference for the other.
            // Not that simple considering I fucked up first.
            Subjects = dto.Subjects.Select(subjectStringArray =>
            (neededSubject: toSyllabus.GetSubjectsWithSpec().First(y => y.Id == subjectStringArray.Key),
            requiredSubjects: fromSyllabus.GetSubjectsWithSpec().Where(Subject => subjectStringArray.Value.Contains(Subject.Id)).ToList()))
            .ToDictionary(x => x.neededSubject, x => x.requiredSubjects);
        }

        public Syllabus FromSyllabus { get; }
        public Syllabus ToSyllabus { get; }
        public Dictionary<Subject, List<Subject>> Subjects { get; }

        public override bool Equals(object obj)
        {
            if (obj is not EqualTable equalTable) return false;
            return FromSyllabus.Equals(equalTable.FromSyllabus) && ToSyllabus.Equals(equalTable.ToSyllabus);
        }

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
