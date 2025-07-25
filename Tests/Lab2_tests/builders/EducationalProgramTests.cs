namespace DefaultNamespace;

public class EducationalProgramBuilderTests
{
    [Fact]
    public void Build_WithValidData_ShouldBuildSuccessfully()
    {
        var builder = new EducationalProgramBuilder();
        var leader = new User("Vova");
        var subjects = new Dictionary<int, List<ISubject>>
        {
            { 1, new List<ISubject>() },
        };

        EducationalProgram program = builder
            .SetName("Computer Science")
            .AddProgramLeader(leader)
            .SetSubjectsBySemester(subjects)
            .Build();

        Assert.NotNull(program);
        Assert.Equal("Computer Science", program.Name);
        Assert.Single(program.ProgramLeaders);
        Assert.Contains(leader.Id, program.ProgramLeaders);
        Assert.Equal(subjects, program.SubjectsBySemester);
    }

    [Fact]
    public void Build_WithEmptyName_ShouldThrowInvalidOperationException()
    {
        var builder = new EducationalProgramBuilder();
        var leader = new User("Misha");
        var subjects = new Dictionary<int, List<ISubject>>
        {
            { 1, new List<ISubject>() },
        };

        Assert.Throws<InvalidOperationException>(() =>
            builder
                .AddProgramLeader(leader)
                .SetSubjectsBySemester(subjects)
                .Build());
    }

    [Fact]
    public void Build_WithNoProgramLeaders_ShouldThrowInvalidOperationException()
    {
        var builder = new EducationalProgramBuilder();
        var subjects = new Dictionary<int, List<ISubject>>
        {
            { 1, new List<ISubject>() },
        };

        Assert.Throws<InvalidOperationException>(() =>
            builder
                .SetName("Mathematics")
                .SetSubjectsBySemester(subjects)
                .Build());
    }

    [Fact]
    public void Build_WithNoSubjects_ShouldThrowInvalidOperationException()
    {
        var builder = new EducationalProgramBuilder();
        var leader = new User("Pasha");

        Assert.Throws<InvalidOperationException>(() =>
            builder
                .SetName("Physics")
                .AddProgramLeader(leader)
                .Build());
    }

    [Fact]
    public void SetName_WithEmptyString_ShouldThrowArgumentException()
    {
        var builder = new EducationalProgramBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetName(string.Empty));
    }

    [Fact]
    public void AddProgramLeader_WithExistingLeader_ShouldThrowArgumentException()
    {
        var builder = new EducationalProgramBuilder();
        var leader = new User("Denis");
        builder.AddProgramLeader(leader);

        Assert.Throws<ArgumentException>(() => builder.AddProgramLeader(leader));
    }

    [Fact]
    public void RemoveProgramLeader_WithNonExistentLeader_ShouldThrowArgumentException()
    {
        var builder = new EducationalProgramBuilder();
        var leader = new User("Roma");
        builder.AddProgramLeader(leader);

        Assert.Throws<ArgumentException>(() => builder.RemoveProgramLeader(new User("Leonid")));
    }

    private class MockSubject : ExamSubject
    {
        public MockSubject(Guid id, string name, double examPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId) : base(id, name, examPoints, labs, lectureMaterials, leaders, copyFromId) { }

        private class SubjectBuilderMock : ExamSubjectBuilder
        {
            public ISubject Build(Guid id, string name, double examPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId)
            {
                throw new NotImplementedException();
            }

            public override bool IsBuilderCorrect() => true;
        }
    }
}