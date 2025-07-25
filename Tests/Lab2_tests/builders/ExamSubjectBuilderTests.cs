namespace DefaultNamespace;

public class ExamSubjectBuilderTests
{
    [Fact]
    public void SetExamPoints_WithPositiveValue_ShouldSetSuccessfully()
    {
        var builder = new ExamSubjectBuilder();

        builder.SetExamPoints(50.0);

        Assert.Equal(50.0, builder.ExamPoints);
    }

    [Fact]
    public void SetExamPoints_WithNonPositiveValue_ShouldThrowArgumentException()
    {
        var builder = new ExamSubjectBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetExamPoints(0));
    }

    [Fact]
    public void Build_WithValidData_ShouldBuildExamSubjectSuccessfully()
    {
        var examLeader = new User("Alice");

        ILectureMaterial lectureMaterial = new LectureMaterialBuilder()
            .SetName("Some lecture")
            .SetContent("Some content")
            .SetSummary("Some summary")
            .AddLectureMaterialLeader(new User("Some leader"))
            .Build();

        ILaboratoryWork laboratoryWork = new LaboratoryWorkBuilder()
            .SetName("Some laboratory")
            .SetDescription("Some description")
            .SetPoints(30)
            .SetEvaluationCriterion("Some criterion")
            .AddLaboratoryWorkLeader(new User("Some leader"))
            .Build();

        var examSubjectBuilder = new ExamSubjectBuilder();
        examSubjectBuilder.SetName("Algorithms")
            .AddSubjectLeader(examLeader)
            .SetExamPoints(70.0)
            .AddLectureMaterial(lectureMaterial)
            .AddLaboratoryWork(laboratoryWork);

        ISubject examSubject = examSubjectBuilder.Build();

        Assert.IsType<ExamSubject>(examSubject);
        Assert.Equal("Algorithms", examSubject.Name);
        Assert.Equal(70.0, ((ExamSubject)examSubject).ExamPoints);
        Assert.Contains(
            ((ExamSubject)examSubject).SubjectLeaders,
            id => id != Guid.Empty);
    }

    [Fact]
    public void Build_WithInvalidExamPoints_ShouldThrowArgumentException()
    {
        var builder = new ExamSubjectBuilder();
        builder.SetName("Data Structures")
            .AddSubjectLeader(new User("Bob"));

        Assert.Throws<ArgumentException>(() => builder.SetExamPoints(-10.0));
    }

    [Fact]
    public void Build_WithIncompleteData_ShouldThrowInvalidOperationException()
    {
        var builder = new ExamSubjectBuilder();

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }
}