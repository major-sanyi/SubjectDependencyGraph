﻿using SubjectDependencyGraph.Shared.Models;

namespace SubjectDependencyGraph.Shared.Services
{
    /// <summary>
    /// Represents a service for managing syllabi and specializations.
    /// </summary>
    public interface ISyllabiService
    {
        /// <summary>
        /// Gets the dictionary of syllabi with their IDs and names.
        /// </summary>
        List<Syllabus> Syllabi { get; }

        /// <summary>
        /// Gets the list of Equality tables.
        /// </summary>
        IReadOnlyList<EqualTable> EqualityTables { get; }

        /// <summary>
        /// Adds a new syllabus.
        /// </summary>
        /// <param name="syllabus">The syllabus to add.</param>
        void AddSyllabus(Syllabus syllabus);

        /// <summary>
        /// Clears finished status of subjects.
        /// </summary>
        void ClearFinishStatus();

        /// <summary>
        /// Exports the completed subjects data.
        /// </summary>
        /// <returns>The dictionary of syllabus IDs and their completed subject IDs.</returns>
        Dictionary<string, List<string>> ExportCompletedSubjects();

        /// <summary>
        /// Imports compleeted subjects.
        /// </summary>
        /// <param name="completedSubjects"></param>
        void ImportCompletedSubjects(Dictionary<string, List<string>> completedSubjects);
    }
}