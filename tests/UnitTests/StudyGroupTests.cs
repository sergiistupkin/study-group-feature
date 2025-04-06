using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestApp;

namespace UnitTests
{
    [TestFixture]
    public class StudyGroupTests
    {
        // TC02 - Reject short StudyGroup name
        [Test]
        public void Should_Not_Allow_Name_Shorter_Than_5()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new StudyGroup(1, "1234", Subject.Math, DateTime.Now, new List<User>())
            );
            Assert.That(ex.Message, Does.Contain("Name must be between 5 and 30 characters"));
        }

        // TC03 - Reject long StudyGroup name
        [Test]
        public void Should_Not_Allow_Name_Longer_Than_30()
        {
            var name = new string('a', 31);
            var ex = Assert.Throws<ArgumentException>(() =>
                new StudyGroup(1, name, Subject.Math, DateTime.Now, new List<User>())
            );
            Assert.That(ex.Message, Does.Contain("Name must be between 5 and 30 characters"));
        }

        // TC13 - Check Valid Subject
        [Test]
        public void Should_Allow_Valid_StudyGroup()
        {
            var group = new StudyGroup(1, "Physics Stars", Subject.Physics, DateTime.Now, new List<User>());
            Assert.AreEqual("Physics Stars", group.Name);
            Assert.AreEqual(Subject.Physics, group.Subject);
        }

        // TC14 - Check Invalid Subject
        [Test]
        public void Should_Reject_Invalid_Subject_If_Validated()
        {
            string invalidSubject = "Biology";
            bool isValid = Enum.TryParse(typeof(Subject), invalidSubject, out _);
            Assert.IsFalse(isValid, "Subject 'Biology' should not be accepted as valid enum");
        }
    }
}
