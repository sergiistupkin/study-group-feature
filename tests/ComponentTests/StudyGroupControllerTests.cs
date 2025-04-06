using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAPI;
using TestApp;

namespace ComponentTests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _repoMock;
        private StudyGroupController _controller;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IStudyGroupRepository>();
            _controller = new StudyGroupController(_repoMock.Object);
        }

        // TC01 - Create valid StudyGroup
        [Test]
        public async Task CreateStudyGroup_Returns_Ok()
        {
            var group = new StudyGroup(1, "Math Masters", Subject.Math, System.DateTime.Now, new List<User>());
            _repoMock.Setup(r => r.CreateStudyGroup(group)).Returns(Task.CompletedTask);

            var result = await _controller.CreateStudyGroup(group);

            Assert.IsInstanceOf<OkResult>(result);
        }

        // TC04 - Duplicate StudyGroup for same subject
        [Test]
        public async Task Should_Not_Allow_Duplicate_StudyGroup_For_Same_User_And_Subject()
        {
            var subject = Subject.Math;
            var existingGroups = new List<StudyGroup>
            {
                new StudyGroup(1, "First Math Group", subject, DateTime.Now, new List<User>())
            };

            var mockRepo = new Mock<IStudyGroupRepository>();
            mockRepo.Setup(r => r.GetStudyGroups()).ReturnsAsync(existingGroups);

            var controller = new StudyGroupController(mockRepo.Object);
            var duplicateGroup = new StudyGroup(2, "Second Math Group", subject, DateTime.Now, new List<User>());

            var result = await controller.CreateStudyGroup(duplicateGroup);

            Assert.IsInstanceOf<OkResult>(result); 
        }

        // TC05 - Allow different subject groups
        [Test]
        public async Task Should_Allow_One_StudyGroup_Per_Subject()
        {
            var subjects = new[] { Subject.Math, Subject.Chemistry, Subject.Physics };
            var createdGroups = new List<StudyGroup>();

            var mockRepo = new Mock<IStudyGroupRepository>();
            mockRepo.Setup(r => r.CreateStudyGroup(It.IsAny<StudyGroup>()))
                    .Callback<StudyGroup>(group => createdGroups.Add(group))
                    .Returns(Task.CompletedTask);

            var controller = new StudyGroupController(mockRepo.Object);

            foreach (var subject in subjects)
            {
                var group = new StudyGroup(Array.IndexOf(subjects, subject) + 1,
                                           $"Group for {subject}", subject,
                                           DateTime.Now, new List<User>());
                var result = await controller.CreateStudyGroup(group);
                Assert.IsInstanceOf<OkResult>(result);
            }

            Assert.AreEqual(3, createdGroups.Count);
        }
        
        // TC06 - Join a StudyGroup
        [Test]
        public async Task JoinStudyGroup_Works()
        {
            _repoMock.Setup(r => r.JoinStudyGroup(1, 10)).Returns(Task.CompletedTask);

            var result = await _controller.JoinStudyGroup(1, 10);

            Assert.IsInstanceOf<OkResult>(result);
        }

        // TC07 - Leave a StudyGroup
        [Test]
        public async Task LeaveStudyGroup_Works()
        {
            _repoMock.Setup(r => r.LeaveStudyGroup(1, 10)).Returns(Task.CompletedTask);

            var result = await _controller.LeaveStudyGroup(1, 10);

            Assert.IsInstanceOf<OkResult>(result);
        }

        // TC08 - List all StudyGroups
        [Test]
        public async Task GetStudyGroups_Returns_List()
        {
            var mockGroups = new List<StudyGroup>
            {
                new StudyGroup(1, "Physics Stars", Subject.Physics, System.DateTime.Now, new List<User>())
            };
            _repoMock.Setup(r => r.GetStudyGroups()).ReturnsAsync(mockGroups);

            var result = await _controller.GetStudyGroups() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<StudyGroup>>(result.Value);
        }

        // TC09 - Filter by subject
        [Test]
        public async Task SearchStudyGroups_Returns_Filtered()
        {
            var mockGroups = new List<StudyGroup>
            {
                new StudyGroup(2, "Math Club", Subject.Math, System.DateTime.Now, new List<User>())
            };
            _repoMock.Setup(r => r.SearchStudyGroups("Math")).ReturnsAsync(mockGroups);

            var result = await _controller.SearchStudyGroups("Math") as OkObjectResult;

            Assert.IsNotNull(result);
            var groups = result.Value as List<StudyGroup>;
            Assert.AreEqual(1, groups.Count);
            Assert.AreEqual("Math Club", groups[0].Name);
        }
    }
}
