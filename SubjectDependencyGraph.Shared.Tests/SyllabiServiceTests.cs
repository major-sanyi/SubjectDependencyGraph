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
            var syllabiService = new SyllabiService(new Dictionary<string, HashSet<string>>() { { SYLLABUSID, [SUBJECTID] } });
            var selectedSyllabus = syllabiService.Syllabi.First(x => x.Id == SYLLABUSID);

            Assert.That(selectedSyllabus.Subjects, Has.Exactly(1).Matches<Subject>(x => x.Finished));
            Assert.That(selectedSyllabus.Subjects, Has.Exactly(1).Matches<Subject>(x => x.Finished && x.Id == SUBJECTID));
        }

        [Test]
        public void Syllabi_ShouldReturnCorrectDictionary()
        {

            Assert.That(_syllabiService.Syllabi, Has.Exactly(1).Matches<SyllabusParent>(x => x.Id == SYLLABUSID));
        }

        [Test]
        public void Specialisations_ShouldReturnCorrectDictionary()
        {
            Assert.That(_syllabiService.Syllabi.First(x => x.Id == SYLLABUSID).Specialisations, Has.Exactly(1).Matches<Specialisation>(x => x.Id == SPECID1));
        }


        [Test]
        public void AddSyllabus_ShouldAddSyllabusToList()
        {
            // Arrange
            var syllabus = new Syllabus() { Id = "Syllabus2", };

            // Act
            _syllabiService.Syllabi.Add(syllabus);

            // Assert
            Assert.That(_syllabiService.Syllabi, Has.Exactly(1).Matches<SyllabusParent>(x => x.Id == "Syllabus2"));
        }

        [Test]
        public void ExportData_ShouldReturnCorrectCompletedSubjects()
        {
            // Arrange
            Subject subject = _syllabiService.Syllabi.First(x => x.Id == SYLLABUSID).Subjects.First();
            subject.Finished = true;

            // Act
            var actualCompletedSubjects = _syllabiService.ExportCompletedSubjects();

            // Assert
            Assert.That(actualCompletedSubjects, Does.ContainValue(new List<string>([subject.Id])));
        }


    }
}
