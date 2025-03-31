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

        [Test]
        public async Task CreateStudyGroup_Returns_Ok()
        {
            var group = new StudyGroup(1, "Math Masters", Subject.Math, System.DateTime.Now, new List<User>());
            _repoMock.Setup(r => r.CreateStudyGroup(group)).Returns(Task.CompletedTask);

            var result = await _controller.CreateStudyGroup(group);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task JoinStudyGroup_Works()
        {
            _repoMock.Setup(r => r.JoinStudyGroup(1, 10)).Returns(Task.CompletedTask);

            var result = await _controller.JoinStudyGroup(1, 10);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task LeaveStudyGroup_Works()
        {
            _repoMock.Setup(r => r.LeaveStudyGroup(1, 10)).Returns(Task.CompletedTask);

            var result = await _controller.LeaveStudyGroup(1, 10);

            Assert.IsInstanceOf<OkResult>(result);
        }

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
