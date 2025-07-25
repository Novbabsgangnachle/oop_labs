namespace DefaultNamespace;

public class EducationalProgramTests
{
    [Fact]
    public void SetName_WithAuthorizedChanger_ShouldSetSuccessfully()
    {
        var leader = new User("Ivan");
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Biology")
            .AddProgramLeader(leader)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TrySetName("Advanced Biology", leader.Id);

        Assert.True(result.Success);
        Assert.Equal($"Set name Advanced Biology", result.Message);
        Assert.Equal("Advanced Biology", program.Name);
    }

    [Fact]
    public void SetName_WithUnauthorizedChanger_ShouldReturnFailure()
    {
        var leader = new User("Vika");
        var unauthorizedId = Guid.NewGuid();
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Chemistry")
            .AddProgramLeader(leader)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TrySetName("Organic Chemistry", unauthorizedId);

        Assert.False(result.Success);
        Assert.Equal("Person without access", result.Message);
        Assert.Equal("Chemistry", program.Name);
    }

    [Fact]
    public void AddSubjectBySemester_WithAuthorizedChanger_ShouldAddSuccessfully()
    {
        var leader = new User("Kirill");
        int semester = 2;
        ISubject subject = new Mock<ISubject>().Object;
        var subject1 = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Physics")
            .AddProgramLeader(leader)
            .AddSubject(1, subject1.Object)
            .Build();

        SuccessAndMessage result = program.TryAddSubjectBySemester(semester, subject, leader.Id);

        Assert.True(result.Success);
        Assert.Equal($"Added subject {subject.Id} into {semester} semester", result.Message);
        Assert.True(program.SubjectsBySemester.ContainsKey(semester));
        Assert.Contains(subject, program.SubjectsBySemester[semester]);
    }

    [Fact]
    public void AddSubjectBySemester_WithExistingSubject_ShouldReturnFailure()
    {
        var leader = new User("Leonid");
        int semester = 2;
        ISubject subject = new Mock<ISubject>().Object;
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Mathematics")
            .AddProgramLeader(leader)
            .AddSubject(semester, subject)
            .Build();

        SuccessAndMessage result = program.TryAddSubjectBySemester(semester, subject, leader.Id);

        Assert.False(result.Success);
        Assert.Equal("Subject already exists in this semester", result.Message);
    }

    [Fact]
    public void DeleteSubjectBySemester_WithAuthorizedChanger_ShouldRemoveSuccessfully()
    {
        var leader = new User("Dima");
        int semester = 3;
        var subjectId = Guid.NewGuid();
        var subjectMock = new Mock<ISubject>();
        subjectMock.Setup(s => s.Id).Returns(subjectId);
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Computer Science")
            .AddProgramLeader(leader)
            .AddSubject(semester, subjectMock.Object)
            .Build();

        SuccessAndMessage result = program.TryDeleteSubjectBySemester(semester, subjectId, leader.Id);

        Assert.True(result.Success);
        Assert.Equal($"Deleted subject {subjectId} from {semester} semester", result.Message);
        Assert.Empty(program.SubjectsBySemester[semester]);
    }

    [Fact]
    public void DeleteSubjectBySemester_WithNonExistentSemester_ShouldReturnFailure()
    {
        var leader = new User("Kui");
        int semester = 4;
        var subjectId = Guid.NewGuid();
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("History")
            .AddProgramLeader(leader)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TryDeleteSubjectBySemester(semester, subjectId, leader.Id);

        Assert.False(result.Success);
        Assert.Equal("Non-existent semester", result.Message);
    }

    [Fact]
    public void AddProgramLeader_WithAuthorizedChanger_ShouldAddSuccessfully()
    {
        var leader = new User("asa");
        var newLeader = new User("Henry");
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Geography")
            .AddProgramLeader(leader)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TryAddProgramLeader(newLeader, leader.Id);

        Assert.True(result.Success);
        Assert.Equal($"Added leader {newLeader.Id}", result.Message);
        Assert.Contains(newLeader.Id, program.ProgramLeaders);
    }

    [Fact]
    public void AddProgramLeader_WithExistingLeader_ShouldReturnFailure()
    {
        var user1 = new User("Vova");
        var user = new User("Isabella");
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Philosophy")
            .AddProgramLeader(user1)
            .AddProgramLeader(user)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TryAddProgramLeader(user, user1.Id);

        Assert.False(result.Success);
        Assert.Equal("Leader is already part of the program", result.Message);
    }

    [Fact]
    public void DeleteProgramLeader_WithAuthorizedChanger_ShouldRemoveSuccessfully()
    {
        var user = new User("Jack");
        var user1 = new User("Valera");
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Art")
            .AddProgramLeader(user1)
            .AddProgramLeader(user)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TryDeleteProgramLeader(user, user1.Id);

        Assert.True(result.Success);
        Assert.Equal($"Deleted leader {user.Id}", result.Message);
        Assert.DoesNotContain(user.Id, program.ProgramLeaders);
    }

    [Fact]
    public void DeleteProgramLeader_WithNonExistentLeader_ShouldReturnFailure()
    {
        var user = new User("Karen");
        var subject = new Mock<ISubject>();
        EducationalProgram program = new EducationalProgramBuilder()
            .SetName("Economics")
            .AddProgramLeader(user)
            .AddSubject(1, subject.Object)
            .Build();

        SuccessAndMessage result = program.TryDeleteProgramLeader(new User("Miki"), user.Id);

        Assert.False(result.Success);
        Assert.Equal("Leader not found", result.Message);
    }
}