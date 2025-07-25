namespace DefaultNamespace;

public class TestSubjectBuilder
{
    public double MinNeededPoints { get; private set; }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public Collection<ILaboratoryWork> LaboratoryWorks { get; private set; } = new Collection<ILaboratoryWork>();

    public Collection<ILectureMaterial> LectureMaterials { get; private set; } = new Collection<ILectureMaterial>();

    public Collection<Guid> SubjectLeaders { get; private set; } = new Collection<Guid>();

    public Guid CopyFromId { get; private set; } = Guid.Empty;

    public virtual TestSubjectBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        Name = name;
        return this;
    }

    public virtual TestSubjectBuilder AddLaboratoryWork(ILaboratoryWork lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        if (LaboratoryWorks.Any(l => l.Id == lab.Id))
            throw new ArgumentException("This lab is already in.");

        LaboratoryWorks.Add(lab);
        return this;
    }

    public virtual TestSubjectBuilder DeleteLaboratoryWork(ILaboratoryWork lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        ILaboratoryWork? existingLab = LaboratoryWorks.FirstOrDefault(l => l.Id == lab.Id);
        if (existingLab == null)
            throw new KeyNotFoundException("Laboratory work not found.");

        LaboratoryWorks.Remove(existingLab);
        return this;
    }

    public virtual TestSubjectBuilder AddLectureMaterial(ILectureMaterial material)
    {
        ArgumentNullException.ThrowIfNull(material);

        if (LectureMaterials.Any(m => m.Id == material.Id))
            throw new ArgumentException("This material is already in.");

        LectureMaterials.Add(material);
        return this;
    }

    public virtual TestSubjectBuilder DeleteLectureMaterial(ILectureMaterial material)
    {
        ArgumentNullException.ThrowIfNull(material);

        ILectureMaterial? existingMaterial = LectureMaterials.FirstOrDefault(m => m.Id == material.Id);
        if (existingMaterial == null)
            throw new KeyNotFoundException("Lecture material not found.");

        LectureMaterials.Remove(existingMaterial);
        return this;
    }

    public virtual TestSubjectBuilder AddSubjectLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (SubjectLeaders.Contains(user.Id))
            throw new ArgumentException("This leader is already in.");

        SubjectLeaders.Add(user.Id);
        return this;
    }

    public virtual TestSubjectBuilder DeleteSubjectLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!SubjectLeaders.Contains(user.Id))
            throw new KeyNotFoundException("Leader not found.");

        SubjectLeaders.Remove(user.Id);
        return this;
    }

    public virtual TestSubjectBuilder CopyFrom(ISubject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        CopyFromId = subject.Id;
        SetName(subject.Name);
        foreach (LaboratoryWork lab in subject.LaboratoryWorks.Cast<LaboratoryWork>())
            AddLaboratoryWork(lab.Clone());

        foreach (LectureMaterial material in subject.LectureMaterials.Cast<LectureMaterial>())
            AddLectureMaterial(material.Clone());

        foreach (Guid leaderId in subject.SubjectLeaders)
            SubjectLeaders.Add(leaderId);

        return this;
    }

    public virtual bool IsBuilderCorrect()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return false;

        if (SubjectLeaders.Count == 0)
            return false;

        return true;
    }

    public TestSubjectBuilder SetMinPoints(double points)
    {
        if (points <= 0)
            throw new ArgumentException("Minimum needed points must be positive.");

        MinNeededPoints = points;
        return this;
    }

    public ISubject Build()
    {
        if (!IsBuilderCorrect())
            throw new InvalidOperationException("Builder isn't correct.");

        if (MinNeededPoints <= 0)
            throw new ArgumentException("Minimum needed points must be positive.");

        return new TestSubject(Id, Name, MinNeededPoints, LaboratoryWorks, LectureMaterials, SubjectLeaders, CopyFromId);
    }
}