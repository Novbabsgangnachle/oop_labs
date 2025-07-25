using System;
using System.Collections.ObjectModel;
using System.Linq;
using lab_2.dto;
using lab_2.interfaces;
using lab_2.interfaces.entities;
using lab_2.utils;

namespace lab_2.entities;

public abstract class Subject : ISubject, IEntity
{
    public Guid Id { get; protected set; }

    public string Name { get; protected set; } = string.Empty;

    public double TotalPoints { get; } = 100;

    public Collection<ILaboratoryWork> LaboratoryWorks { get; protected set; } = new Collection<ILaboratoryWork>();

    public Collection<ILectureMaterial> LectureMaterials { get; protected set; } = new Collection<ILectureMaterial>();

    public Collection<Guid> SubjectLeaders { get; protected set; } = new Collection<Guid>();

    public Guid CopyFromId { get; protected set; }

    public SuccessAndMessage TrySetName(string newName, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (string.IsNullOrWhiteSpace(newName))
            return new SuccessAndMessage(false, "Name cannot be empty");

        Name = newName;
        return new SuccessAndMessage(true, $"Name set {Name}");
    }

    public SuccessAndMessage TryAddLaboratoryWork(ILaboratoryWork lab, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (LaboratoryWorks.Any(l => l.Id == lab.Id))
            return new SuccessAndMessage(false, "This laboratory work already in");

        if (CanAddLaboratory(lab))
        {
            LaboratoryWorks.Add(lab);
            return new SuccessAndMessage(true, $"Laboratory added {lab.Id}");
        }
        else
        {
            return new SuccessAndMessage(false, "This laboratory weight too much");
        }
    }

    public SuccessAndMessage TryDeleteLaboratoryWork(ILaboratoryWork lab, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        ILaboratoryWork? existingLab = LaboratoryWorks.FirstOrDefault(l => l.Id == lab.Id);
        if (existingLab == null)
            return new SuccessAndMessage(false, "Laboratory work not found");

        LaboratoryWorks.Remove(existingLab);
        return new SuccessAndMessage(true, $"Deleted laboratory {lab.Id}");
    }

    public SuccessAndMessage TryAddLectureMaterial(ILectureMaterial material, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (LectureMaterials.Any(m => m.Id == material.Id))
            return new SuccessAndMessage(false, "This lecture material already in");

        LectureMaterials.Add(material);
        return new SuccessAndMessage(true, $"Added lecture material {material.Id}");
    }

    public SuccessAndMessage TryDeleteLectureMaterial(ILectureMaterial material, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        ILectureMaterial? existingMaterial = LectureMaterials.FirstOrDefault(m => m.Id == material.Id);
        if (existingMaterial == null)
            return new SuccessAndMessage(false, "Lecture material not found");

        LectureMaterials.Remove(existingMaterial);
        return new SuccessAndMessage(true, $"Deleted lecture material{material.Id}");
    }

    public SuccessAndMessage TryAddSubjectLeader(User user, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (SubjectLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "This leader is already in.");

        SubjectLeaders.Add(user.Id);
        return new SuccessAndMessage(true, $"Added leader {user.Id}");
    }

    public SuccessAndMessage TryDeleteSubjectLeader(User user, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (!SubjectLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "Leader not found.");

        SubjectLeaders.Remove(user.Id);
        return new SuccessAndMessage(true, $"Deleted leader {user.Id}");
    }

    private bool CanAddLaboratory(ILaboratoryWork laboratoryWork)
    {
        double currentPoints = LaboratoryWorks.Sum(work => work.Points);
        return (currentPoints + laboratoryWork.Points) <= TotalPoints;
    }
}