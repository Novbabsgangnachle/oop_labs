using System;
using System.Collections.ObjectModel;
using lab_2.dto;
using lab_2.interfaces.entities;
using lab_2.utils;

namespace lab_2.entities;

public class TestSubject : Subject
{
    public double MinNeededPoints { get; private set; }

    internal TestSubject(Guid id, string name, double minNeededPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId)
    {
        Id = id;
        Name = name;
        MinNeededPoints = minNeededPoints;
        LaboratoryWorks = labs;
        LectureMaterials = lectureMaterials;
        SubjectLeaders = leaders;
        CopyFromId = copyFromId;
    }

    public SuccessAndMessage TryMinNeededPoints(double points, Guid changerId)
    {
        if (points <= 0)
            throw new ArgumentException("Points must to be positive");

        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        MinNeededPoints = points;
        return new SuccessAndMessage(true, $"Set min needed points {MinNeededPoints}");
    }
}