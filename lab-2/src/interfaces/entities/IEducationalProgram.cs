using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using lab_2.dto;
using lab_2.entities;

namespace lab_2.interfaces.entities;

public interface IEducationalProgram
{
    Guid Id { get; }

    string Name { get; }

    Dictionary<int, List<ISubject>> SubjectsBySemester { get; }

    Collection<Guid> ProgramLeaders { get; }

    SuccessAndMessage TrySetName(string newName, Guid changerId);

    SuccessAndMessage TryAddSubjectBySemester(int semester, ISubject subject, Guid changerId);

    SuccessAndMessage TryDeleteSubjectBySemester(int semester, Guid subjectId, Guid changerId);

    SuccessAndMessage TryAddProgramLeader(User user, Guid changerId);

    SuccessAndMessage TryDeleteProgramLeader(User user, Guid changerId);
}