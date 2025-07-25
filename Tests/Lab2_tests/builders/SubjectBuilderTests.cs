namespace DefaultNamespace;

public class SubjectBuilderTests
{
    private class ConcreteSubjectBuilder : ExamSubjectBuilder
    {
         public ISubject Build(Guid id, string name, double examPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId)
        {
            if (!IsBuilderCorrect())
                throw new InvalidOperationException("Builder isn't correct.");

            return new MockSubject(Id, Name, ExamPoints, LaboratoryWorks, LectureMaterials, SubjectLeaders, CopyFromId);
        }

         private class MockSubject : ExamSubject
        {
            public MockSubject(Guid id, string name, double examPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId) : base(id, name, examPoints, labs, lectureMaterials, leaders, copyFromId)
            { }
        }
    }

    [Fact]
    public void SetName_WithValidName_ShouldSetSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();

        builder.SetName("Operating Systems");

        Assert.Equal("Operating Systems", builder.Name);
    }

    [Fact]
    public void SetName_WithEmptyString_ShouldThrowArgumentException()
    {
        var builder = new ConcreteSubjectBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetName(string.Empty));
    }

    [Fact]
    public void AddLaboratoryWork_WithValidLab_ShouldAddSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var labMock = new Mock<ILaboratoryWork>();
        labMock.Setup(l => l.Id).Returns(Guid.NewGuid());

        builder.AddLaboratoryWork(labMock.Object);

        Assert.Single(builder.LaboratoryWorks);
        Assert.Contains(labMock.Object, builder.LaboratoryWorks);
    }

    [Fact]
    public void AddLaboratoryWork_WithExistingLab_ShouldThrowArgumentException()
    {
        var builder = new ConcreteSubjectBuilder();
        var labId = Guid.NewGuid();
        var labMock = new Mock<ILaboratoryWork>();
        labMock.Setup(l => l.Id).Returns(labId);
        builder.AddLaboratoryWork(labMock.Object);

        var anotherLabMock = new Mock<ILaboratoryWork>();
        anotherLabMock.Setup(l => l.Id).Returns(labId);

        Assert.Throws<ArgumentException>(() => builder.AddLaboratoryWork(anotherLabMock.Object));
    }

    [Fact]
    public void DeleteLaboratoryWork_WithExistingLab_ShouldRemoveSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var labMock = new Mock<ILaboratoryWork>();
        labMock.Setup(l => l.Id).Returns(Guid.NewGuid());
        builder.AddLaboratoryWork(labMock.Object);

        builder.DeleteLaboratoryWork(labMock.Object);

        Assert.Empty(builder.LaboratoryWorks);
    }

    [Fact]
    public void DeleteLaboratoryWork_WithNonExistentLab_ShouldThrowKeyNotFoundException()
    {
        var builder = new ConcreteSubjectBuilder();
        var labMock = new Mock<ILaboratoryWork>();
        labMock.Setup(l => l.Id).Returns(Guid.NewGuid());

        Assert.Throws<KeyNotFoundException>(() => builder.DeleteLaboratoryWork(labMock.Object));
    }

    [Fact]
    public void AddLectureMaterial_WithValidMaterial_ShouldAddSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var materialMock = new Mock<ILectureMaterial>();
        materialMock.Setup(m => m.Id).Returns(Guid.NewGuid());

        builder.AddLectureMaterial(materialMock.Object);

        Assert.Single(builder.LectureMaterials);
        Assert.Contains(materialMock.Object, builder.LectureMaterials);
    }

    [Fact]
    public void AddLectureMaterial_WithExistingMaterial_ShouldThrowArgumentException()
    {
        var builder = new ConcreteSubjectBuilder();
        var materialId = Guid.NewGuid();
        var materialMock = new Mock<ILectureMaterial>();
        materialMock.Setup(m => m.Id).Returns(materialId);
        builder.AddLectureMaterial(materialMock.Object);

        var anotherMaterialMock = new Mock<ILectureMaterial>();
        anotherMaterialMock.Setup(m => m.Id).Returns(materialId);

        Assert.Throws<ArgumentException>(() => builder.AddLectureMaterial(anotherMaterialMock.Object));
    }

    [Fact]
    public void DeleteLectureMaterial_WithExistingMaterial_ShouldRemoveSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var materialMock = new Mock<ILectureMaterial>();
        materialMock.Setup(m => m.Id).Returns(Guid.NewGuid());
        builder.AddLectureMaterial(materialMock.Object);

        builder.DeleteLectureMaterial(materialMock.Object);

        Assert.Empty(builder.LectureMaterials);
    }

    [Fact]
    public void DeleteLectureMaterial_WithNonExistentMaterial_ShouldThrowKeyNotFoundException()
    {
        var builder = new ConcreteSubjectBuilder();
        var materialMock = new Mock<ILectureMaterial>();
        materialMock.Setup(m => m.Id).Returns(Guid.NewGuid());

        Assert.Throws<KeyNotFoundException>(() => builder.DeleteLectureMaterial(materialMock.Object));
    }

    [Fact]
    public void AddSubjectLeader_WithValidLeader_ShouldAddSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var user = new User("George");

        builder.AddSubjectLeader(user);

        Assert.Single(builder.SubjectLeaders);
        Assert.Contains(user.Id, builder.SubjectLeaders);
    }

    [Fact]
    public void AddSubjectLeader_WithExistingLeader_ShouldThrowArgumentException()
    {
        var builder = new ConcreteSubjectBuilder();
        var user = new User("Hannah");
        builder.AddSubjectLeader(user);

        Assert.Throws<ArgumentException>(() => builder.AddSubjectLeader(user));
    }

    [Fact]
    public void DeleteSubjectLeader_WithExistingLeader_ShouldRemoveSuccessfully()
    {
        var builder = new ConcreteSubjectBuilder();
        var user = new User("Ian");
        builder.AddSubjectLeader(user);

        builder.DeleteSubjectLeader(user);

        Assert.Empty(builder.SubjectLeaders);
    }

    [Fact]
    public void DeleteSubjectLeader_WithNonExistentLeader_ShouldThrowArgumentException()
    {
        var builder = new ConcreteSubjectBuilder();
        var user = new User("Jane");

        Assert.Throws<KeyNotFoundException>(() => builder.DeleteSubjectLeader(user));
    }

    [Fact]
    public void IsBuilderCorrect_WithValidData_ShouldReturnTrue()
    {
        var builder = new ConcreteSubjectBuilder();
        var user = new User("Kevin");
        var labMock = new Mock<ILaboratoryWork>();
        labMock.Setup(lab => lab.Points).Returns(50.0);
        var materialMock = new Mock<ILectureMaterial>();
        builder.SetName("Databases")
            .AddSubjectLeader(user)
            .AddLaboratoryWork(labMock.Object)
            .AddLectureMaterial(materialMock.Object);

        bool isCorrect = builder.IsBuilderCorrect();

        Assert.True(isCorrect);
    }
}