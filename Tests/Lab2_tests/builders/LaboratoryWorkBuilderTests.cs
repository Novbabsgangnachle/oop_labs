namespace DefaultNamespace;

public class LaboratoryWorkBuilderTests
{
    [Fact]
    public void SetName_WithValidName_ShouldSetSuccessfully()
    {
        var builder = new LaboratoryWorkBuilder();

        builder.SetName("Lab 1");

        Assert.Equal("Lab 1", builder.Name);
    }

    [Fact]
    public void SetName_WithEmptyString_ShouldThrowArgumentException()
    {
        var builder = new LaboratoryWorkBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetName(string.Empty));
    }

    [Fact]
    public void SetPoints_WithValidValue_ShouldSetSuccessfully()
    {
        var builder = new LaboratoryWorkBuilder();

        builder.SetPoints(50.0);

        Assert.Equal(50.0, builder.Points);
    }

    [Fact]
    public void SetPoints_WithInvalidValue_ShouldThrowArgumentException()
    {
        var builder = new LaboratoryWorkBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetPoints(0));
    }

    [Fact]
    public void AddLaboratoryWorkLeader_WithValidLeader_ShouldAddSuccessfully()
    {
        var builder = new LaboratoryWorkBuilder();
        var user = new User("Alice");

        builder.AddLaboratoryWorkLeader(user);

        Assert.Single(builder.LaboratoryLeaders);
        Assert.Contains(user.Id, builder.LaboratoryLeaders);
    }

    [Fact]
    public void AddLaboratoryWorkLeader_WithExistingLeader_ShouldThrowArgumentException()
    {
        var builder = new LaboratoryWorkBuilder();
        var user = new User("Bob");
        builder.AddLaboratoryWorkLeader(user);

        Assert.Throws<ArgumentException>(() => builder.AddLaboratoryWorkLeader(user));
    }

    [Fact]
    public void Build_WithValidData_ShouldBuildLaboratoryWorkSuccessfully()
    {
        var builder = new LaboratoryWorkBuilder();
        var user = new User("Charlie");
        builder.SetName("Lab 2")
            .SetDescription("Description of Lab 2")
            .SetEvaluationCriterion("Evaluation Criterion")
            .SetPoints(80.0)
            .AddLaboratoryWorkLeader(user);

        ILaboratoryWork lab = builder.Build();

        Assert.IsType<LaboratoryWork>(lab);
        Assert.Equal("Lab 2", lab.Name);
        Assert.Equal("Description of Lab 2", lab.Description);
        Assert.Equal("Evaluation Criterion", lab.EvaluationCriterion);
        Assert.Equal(80.0, lab.Points);
        Assert.Single(lab.LaboratoryLeaders);
        Assert.Contains(user.Id, lab.LaboratoryLeaders);
    }

    [Fact]
    public void Build_WithIncompleteData_ShouldThrowInvalidOperationException()
    {
        var builder = new LaboratoryWorkBuilder();

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Clone_ShouldReturnNewLaboratoryWorkWithSameProperties()
    {
        var builder = new LaboratoryWorkBuilder();
        var user = new User("Diana");
        builder.SetName("Lab Genetics")
            .SetDescription("Genetics Lab")
            .SetEvaluationCriterion("Genetics Criterion")
            .SetPoints(85.0)
            .AddLaboratoryWorkLeader(user);
        ILaboratoryWork lab = builder.Build();

        ILaboratoryWork clonedLab = lab.Clone();

        Assert.NotSame(lab, clonedLab);
        Assert.Equal(lab.Name, clonedLab.Name);
        Assert.Equal(lab.Description, clonedLab.Description);
        Assert.Equal(lab.EvaluationCriterion, clonedLab.EvaluationCriterion);
        Assert.Equal(lab.Points, clonedLab.Points);
        Assert.Equal(lab.LaboratoryLeaders, clonedLab.LaboratoryLeaders);
    }
}