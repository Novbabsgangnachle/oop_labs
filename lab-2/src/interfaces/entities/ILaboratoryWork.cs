using System;
using System.Collections.ObjectModel;
using lab_2.dto;

namespace lab_2.interfaces.entities;

public interface ILaboratoryWork
{
    Guid Id { get; }

    string Name { get; }

    string Description { get; }

    string EvaluationCriterion { get; }

    double Points { get; }

    Collection<Guid> LaboratoryLeaders { get; }

    Guid CopyFromId { get; }

    SuccessAndMessage TrySetName(string newName, Guid changerId);

    SuccessAndMessage TrySetDescription(string newDescription, Guid changerId);

    SuccessAndMessage TrySetEvaluationCriterion(string newEvaluationCriterion, Guid changerId);

    SuccessAndMessage TrySetPoints(double points, Guid changerId);

    SuccessAndMessage TryAddLaboratoryWorkLeader(User user, Guid changerId);

    SuccessAndMessage TryDeleteLaboratoryWorkLeader(User user, Guid changerId);

    ILaboratoryWork Clone();
}