using SubjectDependencyGraph.Shared.Models;

namespace SubjectDependencyGraph.Shared.Services
{
    /// <summary>
    /// Represents a service for managing syllabi and specializations.
    /// </summary>
    public interface ISyllabiService
    {
        /// <summary>
        /// Gets the selected syllabus.
        /// </summary>
        Syllabus SelectedSyllabus { get; }

        /// <summary>
        /// Gets the selected specializations.
        /// </summary>
        IReadOnlyList<Specialisation> SelectedSpecialisations { get; }

        /// <summary>
        /// Gets the dictionary of specializations with their IDs and names.
        /// </summary>
        IReadOnlyList<Specialisation> Specialisations { get; }

        /// <summary>
        /// Gets the dictionary of syllabi with their IDs and names.
        /// </summary>
        IReadOnlyList<Syllabus> Syllabi { get; }

        /// <summary>
        /// Adds a new syllabus.
        /// </summary>
        /// <param name="syllabus">The syllabus to add.</param>
        void AddSyllabus(Syllabus syllabus);

        /// <summary>
        /// Exports the completed subjects data.
        /// </summary>
        /// <returns>The dictionary of syllabus IDs and their completed subject IDs.</returns>
        Dictionary<string, List<string>> ExportData();

        /// <summary>
        /// Selects multiple specializations.
        /// </summary>
        /// <param name="specId">The array of specialization IDs to select.</param>
        void SelectMultipleSpecialisations(string[] specId);

        /// <summary>
        /// Selects a specialization.
        /// </summary>
        /// <param name="specId">The ID of the specialization to select.</param>
        void SelectSpecialisation(string specId);

        /// <summary>
        /// Selects a syllabus.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus to select.</param>
        void SelectSyllabus(string syllabusId);
    }
}