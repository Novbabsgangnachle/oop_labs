namespace DefaultNamespace;

public class LectureMaterialBuilderTests
{
    [Fact]
    public void SetName_WithValidName_ShouldSetSuccessfully()
    {
        var builder = new LectureMaterialBuilder();

        builder.SetName("Lecture 1");

        Assert.Equal("Lecture 1", builder.Name);
    }

    [Fact]
    public void SetName_WithEmptyString_ShouldThrowArgumentException()
    {
        var builder = new LectureMaterialBuilder();

        Assert.Throws<ArgumentException>(() => builder.SetName(string.Empty));
    }

    [Fact]
    public void AddLectureMaterialLeader_WithValidLeader_ShouldAddSuccessfully()
    {
        var builder = new LectureMaterialBuilder();
        var user = new User("Alice");

        builder.AddLectureMaterialLeader(user);

        Assert.Single(builder.LectureMaterialLeaders);
        Assert.Contains(user.Id, builder.LectureMaterialLeaders);
    }

    [Fact]
    public void AddLectureMaterialLeader_WithExistingLeader_ShouldThrowArgumentException()
    {
        var builder = new LectureMaterialBuilder();
        var user = new User("Bob");
        builder.AddLectureMaterialLeader(user);

        Assert.Throws<ArgumentException>(() => builder.AddLectureMaterialLeader(user));
    }

    [Fact]
    public void Build_WithValidData_ShouldBuildLectureMaterialSuccessfully()
    {
        var builder = new LectureMaterialBuilder();
        var user = new User("Charlie");
        builder.SetName("Lecture 2")
            .SetSummary("Summary of Lecture 2")
            .SetContent("Content of Lecture 2")
            .AddLectureMaterialLeader(user);

        ILectureMaterial material = builder.Build();

        Assert.IsType<LectureMaterial>(material);
        Assert.Equal("Lecture 2", material.Name);
        Assert.Equal("Summary of Lecture 2", material.Summary);
        Assert.Equal("Content of Lecture 2", material.Content);
        Assert.Single(material.LectureMaterialLeaders);
        Assert.Contains(user.Id, material.LectureMaterialLeaders);
    }

    [Fact]
    public void Build_WithIncompleteData_ShouldThrowInvalidOperationException()
    {
        var builder = new LectureMaterialBuilder();

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Clone_ShouldReturnNewLectureMaterialWithSameProperties()
    {
        var builder = new LectureMaterialBuilder();
        var user = new User("Diana");
        builder.SetName("Lecture Genetics")
            .SetSummary("Genetics Summary")
            .SetContent("Genetics Content")
            .AddLectureMaterialLeader(user);
        ILectureMaterial material = builder.Build();

        ILectureMaterial clonedMaterial = material.Clone();

        Assert.NotSame(material, clonedMaterial);
        Assert.Equal(material.Name, clonedMaterial.Name);
        Assert.Equal(material.Summary, clonedMaterial.Summary);
        Assert.Equal(material.Content, clonedMaterial.Content);
        Assert.Equal(material.LectureMaterialLeaders, clonedMaterial.LectureMaterialLeaders);
    }
}