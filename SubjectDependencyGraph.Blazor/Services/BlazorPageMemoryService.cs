using SubjectDependencyGraph.Shared.Models;
using SubjectDependencyGraph.Shared.Services;

namespace SubjectDependencyGraph.Blazor.Services
{
    /// <summary>
    /// Saves data across blazor Pages
    /// </summary>
    /// <param name="syllabiService">Needs Syllabi service for default values</param>
    public class BlazorPageMemoryService(ISyllabiService syllabiService) : IBlazorPageMemoryService
    {
        /// <summary>
        /// The SelectedSyllabus
        /// </summary>
        public Syllabus SyllabusSelected { get; set; } = syllabiService.Syllabi[0];

        /// <summary>
        /// The selected equalTable
        /// </summary>
        public EqualTable EqualTableSelected { get; set; } = syllabiService.EqualityTables[0];

        /// <summary>
        /// True if shows only Finished subjects on EquivalencePage.
        /// </summary>
        public bool ShowOnlyFinishedSubjects { get; set; } = false;
    }
}
