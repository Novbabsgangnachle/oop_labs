namespace DefaultNamespace;

public class LaboratoryWork : ILaboratoryWork, IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string EvaluationCriterion { get; private set; }

    public double Points { get; private set; }

    public Collection<Guid> LaboratoryLeaders { get; }

    public Guid CopyFromId { get; private set; }

    public LaboratoryWork(Guid id, string name, string description, string evaluationCriterion, double points, Collection<Guid> laboratoryLeaders, Guid copyFromId)
    {
        Id = id;
        Name = name;
        Description = description;
        EvaluationCriterion = evaluationCriterion;
        Points = points;
        CopyFromId = copyFromId;
        LaboratoryLeaders = laboratoryLeaders;
    }

    public LaboratoryWork(ILaboratoryWork lab)
    {
        Id = Guid.NewGuid();
        Name = lab.Name;
        Description = lab.Description;
        EvaluationCriterion = lab.EvaluationCriterion;
        Points = lab.Points;
        CopyFromId = lab.Id;
        LaboratoryLeaders = new Collection<Guid>(lab.LaboratoryLeaders);
    }

    public SuccessAndMessage TrySetName(string newName, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (string.IsNullOrWhiteSpace(newName))
            return new SuccessAndMessage(false, "Name cannot be empty");

        Name = newName;
        return new SuccessAndMessage(true, $"Set name {Name}");
    }

    public SuccessAndMessage TrySetDescription(string newDescription, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (string.IsNullOrWhiteSpace(newDescription))
            return new SuccessAndMessage(false, "Description cannot be empty");

        Description = newDescription;
        return new SuccessAndMessage(true, $"Set description {Description}");
    }

    public SuccessAndMessage TrySetEvaluationCriterion(string newEvaluationCriterion, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (string.IsNullOrWhiteSpace(newEvaluationCriterion))
            return new SuccessAndMessage(false, "Evaluation Criterion cannot be empty");

        EvaluationCriterion = newEvaluationCriterion;
        return new SuccessAndMessage(true, $"Set evaluation criterion {EvaluationCriterion}");
    }

    public SuccessAndMessage TrySetPoints(double points, Guid changerId)
    {
        if (points <= 0 || points > 100)
            return new SuccessAndMessage(false, "Points should be in (0;100]");

        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        Points = points;
        return new SuccessAndMessage(true, $"Set points {points}");
    }

    public SuccessAndMessage TryAddLaboratoryWorkLeader(User user, Guid changerId)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (LaboratoryLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "This leader is already in");

        LaboratoryLeaders.Add(user.Id);
        return new SuccessAndMessage(true, $"Added leader {user.Id}");
    }

    public SuccessAndMessage TryDeleteLaboratoryWorkLeader(User user, Guid changerId)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!IdChecker.HasAccess(changerId, LaboratoryLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (!LaboratoryLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "Leader not found");

        LaboratoryLeaders.Remove(user.Id);
        return new SuccessAndMessage(true, $"Deleted leader {user.Id}");
    }

    public ILaboratoryWork Clone()
    {
        return new LaboratoryWork(this);
    }
}