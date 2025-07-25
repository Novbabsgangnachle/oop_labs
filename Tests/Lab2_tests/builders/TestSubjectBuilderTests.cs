namespace DefaultNamespace;

public class TestSubjectBuilderTests
{
    [Fact]
    public void SetMinPoints_WithValidValue_ShouldSetSuccessfully()
    {
        var builder = new TestSubjectBuilder();

        builder.SetMinPoints(60.0);

        Assert.Equal(60.0, builder.MinNeededPoints);
    }

    [Fact]
    public void SetMinPoints_WithNonPositiveValue_ShouldThrowArgumentException()
    {
        var builder = new TestSubjectBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetMinPoints(0));
    }

    [Fact]
    public void Build_WithValidData_ShouldBuildTestSubjectSuccessfully()
    {
        var builder = new TestSubjectBuilder();
        var user = new User("Michael");
        builder.SetName("Machine Learning")
            .AddSubjectLeader(user)
            .SetMinPoints(70.0);

        ISubject testSubject = builder.Build();

        Assert.IsType<TestSubject>(testSubject);
        Assert.Equal("Machine Learning", testSubject.Name);
        Assert.Equal(70.0, ((TestSubject)testSubject).MinNeededPoints);
        Assert.Contains(user.Id, testSubject.SubjectLeaders);
    }

    [Fact]
    public void Build_WithInvalidMinPoints_ShouldThrowArgumentException()
    {
        var builder = new TestSubjectBuilder();
        builder.SetName("Artificial Intelligence")
            .AddSubjectLeader(new User("Bob"));

        Assert.Throws<ArgumentException>(() => builder.SetMinPoints(-10.0));
    }

    [Fact]
    public void Build_WithIncompleteData_ShouldThrowInvalidOperationException()
    {
        var builder = new TestSubjectBuilder();

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }
}