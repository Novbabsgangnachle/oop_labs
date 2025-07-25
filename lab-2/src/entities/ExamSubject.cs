using System;
using System.Collections.ObjectModel;
using lab_2.dto;
using lab_2.interfaces.entities;
using lab_2.utils;

namespace lab_2.entities;

public class ExamSubject : Subject
{
    public double ExamPoints { get; private set; }

    public ExamSubject(Guid id, string name, double examPoints, Collection<ILaboratoryWork> labs, Collection<ILectureMaterial> lectureMaterials, Collection<Guid> leaders, Guid copyFromId)
    {
        Id = id;
        Name = name;
        ExamPoints = examPoints;
        LaboratoryWorks = labs;
        LectureMaterials = lectureMaterials;
        SubjectLeaders = leaders;
        CopyFromId = copyFromId;
    }

    public SuccessAndMessage TrySetExamPoint(double points, Guid changerId)
    {
        if (points <= 0)
            throw new ArgumentException("Points must to be positive");

        if (!IdChecker.HasAccess(changerId, SubjectLeaders))
            return new SuccessAndMessage(false, "Person without access");

        ExamPoints = points;
        return new SuccessAndMessage(true, $"Set exam points {ExamPoints}");
    }
}