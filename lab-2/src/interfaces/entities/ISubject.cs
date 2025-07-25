using System;
using System.Collections.ObjectModel;
using lab_2.dto;

namespace lab_2.interfaces.entities;

public interface ISubject
{
    Guid Id { get; }

    string Name { get; }

    double TotalPoints { get; }

    Collection<ILaboratoryWork> LaboratoryWorks { get; }

    Collection<ILectureMaterial> LectureMaterials { get; }

    Collection<Guid> SubjectLeaders { get; }

    Guid CopyFromId { get; }

    SuccessAndMessage TrySetName(string newName, Guid changerId);

    SuccessAndMessage TryAddLaboratoryWork(ILaboratoryWork lab, Guid changerId);

    SuccessAndMessage TryDeleteLaboratoryWork(ILaboratoryWork lab, Guid changerId);

    SuccessAndMessage TryAddLectureMaterial(ILectureMaterial material, Guid changerId);

    SuccessAndMessage TryDeleteLectureMaterial(ILectureMaterial material, Guid changerId);

    SuccessAndMessage TryAddSubjectLeader(User user, Guid changerId);

    SuccessAndMessage TryDeleteSubjectLeader(User user, Guid changerId);
}