using NUnit.Framework;
using SubjectDependencyGraph.Shared.Models;
using SubjectDependencyGraph.Shared.Services;

namespace SubjectDependencyGraph.Shared.Tests
{
    [TestFixture]
    public class SyllabiServiceTests
    {
        private SyllabiService _syllabiService;
        const string SYLLABUSID = "NBNEMI";
        const string SPECID1 = "NBNEMI-AI-BD";
        const string SPECID2 = "NBNEMI-AI-IOT";
        const string SUBJECTID = "GGXJA1HBEE";
        [SetUp]
        public void Setup()
        {
            _syllabiService = new SyllabiService();
        }

        [Test]
        public void ServiceIsCreated()
        {
            Assert.That(_syllabiService, Is.Not.Null);
        }

        [Test]
        public void CanImportData()
        {
            //Arrange
            var syllabiService = new SyllabiService(new Dictionary<string, List<string>>() { { SYLLABUSID, [SUBJECTID] } });
            syllabiService.SelectSyllabus(SYLLABUSID);

            Assert.That(syllabiService.SelectedSyllabus.Subjects, Has.Exactly(1).Matches<Subject>(x => x.Finished));
            Assert.That(syllabiService.SelectedSyllabus.Subjects, Has.Exactly(1).Matches<Subject>(x => x.Finished && x.Id == SUBJECTID));
        }

        [Test]
        public void Syllabi_ShouldReturnCorrectDictionary()
        {

            Assert.That(_syllabiService.Syllabi, Does.ContainKey(SYLLABUSID));
        }

        [Test]
        public void Specialisations_ShouldReturnCorrectDictionary()
        {
            Assert.That(_syllabiService.Specialisations, Does.ContainKey(SPECID1));
        }

        [Test]
        public void SelectedSyllabus_ShouldBeSetCorrectly()
        {
            // Arrange

            // Act
            _syllabiService.SelectSyllabus(SYLLABUSID);

            // Assert
            Assert.That(_syllabiService.SelectedSyllabus.Id, Is.EqualTo(SYLLABUSID));
        }

        [Test]
        public void SelectedSpecialisations_ShouldBeEmptyInitially()
        {
            // Assert
            Assert.That(_syllabiService.SelectedSpecialisations, Is.Empty);
        }

        [Test]
        public void SelectSpecialisation_ShouldAddSpecialisation()
        {
            // Arrange

            // Act
            _syllabiService.SelectSpecialisation(SPECID1);

            // Assert
            Assert.That(_syllabiService.SelectedSpecialisations, Has.Exactly(1).Matches<Specialisation>(x => x.Id == SPECID1));
        }
        [Test]
        public void SelectedSpecialisation_ShouldNotContainDuplicate()
        {

            // Act
            _syllabiService.SelectSpecialisation(SPECID1);
            _syllabiService.SelectSpecialisation(SPECID1);
            _syllabiService.SelectSpecialisation(SPECID1);


            // Assert
            Assert.That(_syllabiService.SelectedSpecialisations, Has.Exactly(1).Matches<Specialisation>(x => x.Id == SPECID1));
        }

        [Test]
        public void SelectMultipleSpecialisations_ShouldAddMultipleSpecialisations()
        {
            // Arrange
            var specialisationIds = new string[] { SPECID1, SPECID2 };
            var expectedSpecialisations = _syllabiService.SelectedSyllabus.Specialisations.Where(s => specialisationIds.Contains(s.Id)).ToList();

            // Act
            _syllabiService.SelectMultipleSpecialisations(specialisationIds);

            // Assert
            CollectionAssert.AreEquivalent(_syllabiService.SelectedSpecialisations, expectedSpecialisations);
        }

        [Test]
        public void AddSyllabus_ShouldAddSyllabusToList()
        {
            // Arrange
            var syllabus = new Syllabus("Syllabus2", "Syllabus 2", 5, 0, 0, 0);

            // Act
            _syllabiService.AddSyllabus(syllabus);

            // Assert
            Assert.That(_syllabiService.Syllabi, Does.ContainKey("Syllabus2"));
        }

        [Test]
        public void ExportData_ShouldReturnCorrectCompletedSubjects()
        {
            // Arrange
            Subject subject = _syllabiService.SelectedSyllabus.Subjects[0];
            subject.Finished = true;

            // Act
            var actualCompletedSubjects = _syllabiService.ExportData();

            // Assert
            Assert.That(actualCompletedSubjects, Does.ContainValue(new List<string>([subject.Id])));
        }


    }
}
