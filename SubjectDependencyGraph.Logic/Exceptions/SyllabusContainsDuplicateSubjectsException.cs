using SubjectDependencyGraph.Shared.Models;

namespace SubjectDependencyGraph.Shared.Exceptions
{
    [Serializable]
    internal class SyllabusContainsDuplicateSubjectsException : Exception
    {
        public override string Message => overriddenMessage;
        private readonly string overriddenMessage;

        public SyllabusContainsDuplicateSubjectsException()
        {
            overriddenMessage = "Not every subject is unique.";
        }
        public SyllabusContainsDuplicateSubjectsException(SyllabusParent syllabusParent)
        {
            overriddenMessage = $"Not every subject is unique in {syllabusParent.Id} : {syllabusParent.Name}";
        }
    }
}