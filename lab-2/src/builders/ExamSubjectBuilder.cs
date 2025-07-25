namespace DefaultNamespace;

public class ExamSubjectBuilder
{
    public double ExamPoints { get; private set; }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public Collection<ILaboratoryWork> LaboratoryWorks { get; private set; } = new Collection<ILaboratoryWork>();

    public Collection<ILectureMaterial> LectureMaterials { get; private set; } = new Collection<ILectureMaterial>();

    public Collection<Guid> SubjectLeaders { get; private set; } = new Collection<Guid>();

    public Guid CopyFromId { get; private set; } = Guid.Empty;

    public virtual ExamSubjectBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        Name = name;
        return this;
    }

    public virtual ExamSubjectBuilder AddLaboratoryWork(ILaboratoryWork lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        if (LaboratoryWorks.Any(l => l.Id == lab.Id))
            throw new ArgumentException("This lab is already in.");

        LaboratoryWorks.Add(lab);
        return this;
    }

    public virtual ExamSubjectBuilder DeleteLaboratoryWork(ILaboratoryWork lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        ILaboratoryWork? existingLab = LaboratoryWorks.FirstOrDefault(l => l.Id == lab.Id);
        if (existingLab == null)
            throw new KeyNotFoundException("Laboratory work not found.");

        LaboratoryWorks.Remove(existingLab);
        return this;
    }

    public virtual ExamSubjectBuilder AddLectureMaterial(ILectureMaterial material)
    {
        ArgumentNullException.ThrowIfNull(material);

        if (LectureMaterials.Any(m => m.Id == material.Id))
            throw new ArgumentException("This material is already in.");

        LectureMaterials.Add(material);
        return this;
    }

    public virtual ExamSubjectBuilder DeleteLectureMaterial(ILectureMaterial material)
    {
        ArgumentNullException.ThrowIfNull(material);

        ILectureMaterial? existingMaterial = LectureMaterials.FirstOrDefault(m => m.Id == material.Id);
        if (existingMaterial == null)
            throw new KeyNotFoundException("Lecture material not found.");

        LectureMaterials.Remove(existingMaterial);
        return this;
    }

    public virtual ExamSubjectBuilder AddSubjectLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (SubjectLeaders.Contains(user.Id))
            throw new ArgumentException("This leader is already in.");

        SubjectLeaders.Add(user.Id);
        return this;
    }

    public virtual ExamSubjectBuilder DeleteSubjectLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!SubjectLeaders.Contains(user.Id))
            throw new KeyNotFoundException("Leader not found.");

        SubjectLeaders.Remove(user.Id);
        return this;
    }

    public virtual ExamSubjectBuilder CopyFrom(ISubject subject)
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

    public ExamSubjectBuilder SetExamPoints(double points)
    {
        if (points <= 0)
            throw new ArgumentException("Exam points must be positive.");

        ExamPoints = points;
        return this;
    }

    public ISubject Build()
    {
        if (!IsBuilderCorrect())
            throw new InvalidOperationException("Builder isn't correct.");

        if (ExamPoints <= 0)
            throw new ArgumentException("Exam points must be positive.");

        return new ExamSubject(Id, Name, ExamPoints, LaboratoryWorks, LectureMaterials, SubjectLeaders, CopyFromId);
    }
}