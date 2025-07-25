namespace DefaultNamespace;

public class LectureMaterialBuilder
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public string Summary { get; private set; } = string.Empty;

    public string Content { get; private set; } = string.Empty;

    public Collection<Guid> LectureMaterialLeaders { get; private set; } = new Collection<Guid>();

    public Guid CopyFromId { get; private set; } = Guid.Empty;

    public LectureMaterialBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        Name = name;
        return this;
    }

    public LectureMaterialBuilder SetSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary cannot be empty.");

        Summary = summary;
        return this;
    }

    public LectureMaterialBuilder SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.");

        Content = content;
        return this;
    }

    public LectureMaterialBuilder AddLectureMaterialLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (LectureMaterialLeaders.Contains(user.Id))
            throw new ArgumentException("This leader is already in.");

        LectureMaterialLeaders.Add(user.Id);
        return this;
    }

    public LectureMaterialBuilder DeleteLectureMaterialLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!LectureMaterialLeaders.Contains(user.Id))
            throw new KeyNotFoundException("Leader not found.");

        LectureMaterialLeaders.Remove(user.Id);
        return this;
    }

    public LectureMaterialBuilder SetLectureMaterialLeaders(Collection<Guid> leaders)
    {
        ArgumentNullException.ThrowIfNull(leaders);

        LectureMaterialLeaders = new Collection<Guid>(leaders);
        return this;
    }

    public LectureMaterialBuilder CopyFrom(ILectureMaterial lectureMaterial)
    {
        ArgumentNullException.ThrowIfNull(lectureMaterial);

        Id = Guid.NewGuid();
        Name = lectureMaterial.Name;
        Summary = lectureMaterial.Summary;
        Content = lectureMaterial.Content;
        CopyFromId = lectureMaterial.Id;
        LectureMaterialLeaders = new Collection<Guid>(lectureMaterial.LectureMaterialLeaders);

        return this;
    }

    public bool IsBuilderCorrect()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return false;

        if (string.IsNullOrWhiteSpace(Summary))
            return false;

        if (string.IsNullOrWhiteSpace(Content))
            return false;

        if (LectureMaterialLeaders.Count == 0)
            return false;

        return true;
    }

    public ILectureMaterial Build()
    {
        if (!IsBuilderCorrect())
            throw new InvalidOperationException("Builder isn't correct.");

        return new LectureMaterial(Id, Name, Summary, Content, CopyFromId, LectureMaterialLeaders);
    }
}