[TestFixture]
public class StudyGroupTests
{
    [Test]
    public void Should_Not_Allow_Name_Shorter_Than_5()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new StudyGroup(1, "1234", Subject.Math, DateTime.Now, new List<User>())
        );
        Assert.That(ex.Message, Does.Contain("Name must be between 5 and 30 characters"));
    }

    [Test]
    public void Should_Not_Allow_Name_Longer_Than_30()
    {
        var name = new string('a', 31);
        var ex = Assert.Throws<ArgumentException>(() =>
            new StudyGroup(1, name, Subject.Math, DateTime.Now, new List<User>())
        );
        Assert.That(ex.Message, Does.Contain("Name must be between 5 and 30 characters"));
    }

    [Test]
    public void Should_Allow_Valid_StudyGroup()
    {
        var group = new StudyGroup(1, "Physics Stars", Subject.Physics, DateTime.Now, new List<User>());
        Assert.AreEqual("Physics Stars", group.Name);
        Assert.AreEqual(Subject.Physics, group.Subject);
    }
}
