
using System.Text.Json.Serialization;

namespace SubjectDependencyGraph.Models
{
    public class Subject
    {
        public Subject(string id, string name, string kredit, string recommendedSemester, string syllabusId, string language, List<string>? preRequisites = null)
        {
            Id = id;
            Name = name;
            Kredit = kredit;
            RecommendedSemester = recommendedSemester;
            SyllabusId = syllabusId;
            Language = language;
            PreRequisites = preRequisites ?? new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Kredit { get; set; }
        public string RecommendedSemester { get; set; }
        public string SyllabusId { get; set; }
        public string Language { get; set; }
        public List<string> PreRequisites { get; }

        [JsonIgnore]
        public bool Finished { get; set; }


    }
}
