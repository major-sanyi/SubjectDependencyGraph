using SubjectDependencyGraph.Shared.Models;

namespace SubjectDependencyGraph.Blazor.Services
{
    public interface IBlazorPageMemoryService
    {
        EqualTable EqualTableSelected { get; set; }
        bool ShowOnlyFinishedSubjects { get; set; }
        Syllabus SyllabusSelected { get; set; }
    }
}