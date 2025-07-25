namespace DefaultNamespace;

public class LaboratoryWorkBuilder
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public string EvaluationCriterion { get; private set; } = string.Empty;

    public double Points { get; private set; } = 1;

    public Collection<Guid> LaboratoryLeaders { get; private set; } = new Collection<Guid>();

    public Guid CopyFromId { get; private set; } = Guid.Empty;

    public LaboratoryWorkBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        Name = name;
        return this;
    }

    public LaboratoryWorkBuilder SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        Description = description;
        return this;
    }

    public LaboratoryWorkBuilder SetEvaluationCriterion(string evaluationCriterion)
    {
        if (string.IsNullOrWhiteSpace(evaluationCriterion))
            throw new ArgumentException("Evaluation Criterion cannot be empty.");

        EvaluationCriterion = evaluationCriterion;
        return this;
    }

    public LaboratoryWorkBuilder SetPoints(double points)
    {
        if (points <= 0 || points > 100)
            throw new ArgumentException("Points should be in (0;100].");

        Points = points;
        return this;
    }

    public LaboratoryWorkBuilder AddLaboratoryWorkLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (LaboratoryLeaders.Contains(user.Id))
            throw new ArgumentException("This leader is already in.");

        LaboratoryLeaders.Add(user.Id);
        return this;
    }

    public LaboratoryWorkBuilder DeleteLaboratoryWorkLeader(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!LaboratoryLeaders.Contains(user.Id))
            throw new KeyNotFoundException("Leader not found.");

        LaboratoryLeaders.Remove(user.Id);
        return this;
    }

    public LaboratoryWorkBuilder SetLaboratoryLeaders(Collection<Guid> leaders)
    {
        ArgumentNullException.ThrowIfNull(leaders);

        LaboratoryLeaders = new Collection<Guid>(leaders);
        return this;
    }

    public LaboratoryWorkBuilder CopyFrom(ILaboratoryWork laboratoryWork)
    {
        ArgumentNullException.ThrowIfNull(laboratoryWork);

        Id = Guid.NewGuid();
        Name = laboratoryWork.Name;
        Description = laboratoryWork.Description;
        EvaluationCriterion = laboratoryWork.EvaluationCriterion;
        Points = laboratoryWork.Points;
        CopyFromId = laboratoryWork.Id;
        LaboratoryLeaders = new Collection<Guid>(laboratoryWork.LaboratoryLeaders);

        return this;
    }

    public bool IsBuilderCorrect()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return false;

        if (string.IsNullOrWhiteSpace(Description))
            return false;

        if (string.IsNullOrWhiteSpace(EvaluationCriterion))
            return false;

        if (Points <= 0 || Points > 100)
            return false;

        if (LaboratoryLeaders.Count == 0)
            return false;

        return true;
    }

    public ILaboratoryWork Build()
    {
        if (!IsBuilderCorrect())
            throw new InvalidOperationException("Builder isn't correct.");

        return new LaboratoryWork(Id, Name, Description, EvaluationCriterion, Points, LaboratoryLeaders, CopyFromId);
    }
}