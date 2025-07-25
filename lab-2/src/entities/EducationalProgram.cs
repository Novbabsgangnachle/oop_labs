using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using lab_2.dto;
using lab_2.interfaces;
using lab_2.interfaces.entities;
using lab_2.utils;

namespace lab_2.entities;

public class EducationalProgram : IEducationalProgram, IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Dictionary<int, List<ISubject>> SubjectsBySemester { get; private set; }

    public Collection<Guid> ProgramLeaders { get; private set; }

    public EducationalProgram(EducationalProgramBuilder builder)
    {
        Id = builder.Id;
        Name = builder.Name;
        SubjectsBySemester = new Dictionary<int, List<ISubject>>(builder.SubjectsBySemester);
        ProgramLeaders = new Collection<Guid>(builder.ProgramLeaders);
    }

    public SuccessAndMessage TrySetName(string newName, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, ProgramLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (string.IsNullOrWhiteSpace(newName))
            return new SuccessAndMessage(false, "Name cannot be empty");

        Name = newName;
        return new SuccessAndMessage(true, $"Set name {Name}");
    }

    public SuccessAndMessage TryAddSubjectBySemester(int semester, ISubject subject, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, ProgramLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (semester <= 0)
            return new SuccessAndMessage(false, "Semester must be a positive integer");

        if (!SubjectsBySemester.ContainsKey(semester))
            SubjectsBySemester[semester] = new List<ISubject>();

        if (SubjectsBySemester[semester].Any(s => s.Id == subject.Id))
            return new SuccessAndMessage(false, "Subject already exists in this semester");

        SubjectsBySemester[semester].Add(subject);
        return new SuccessAndMessage(true, $"Added subject {subject.Id} into {semester} semester");
    }

    public SuccessAndMessage TryDeleteSubjectBySemester(int semester, Guid subjectId, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, ProgramLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (!SubjectsBySemester.ContainsKey(semester))
            return new SuccessAndMessage(false, "Non-existent semester");

        var subject = SubjectsBySemester[semester].FirstOrDefault(s => s.Id == subjectId);
        if (subject == null)
            return new SuccessAndMessage(false, "Subject not found in the specified semester");

        SubjectsBySemester[semester].Remove(subject);
        return new SuccessAndMessage(true, $"Deleted subject {subjectId} from {semester} semester");
    }

    public SuccessAndMessage TryAddProgramLeader(User user, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, ProgramLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (ProgramLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "Leader is already part of the program");

        ProgramLeaders.Add(user.Id);
        return new SuccessAndMessage(true, $"Added leader {user.Id}");
    }

    public SuccessAndMessage TryDeleteProgramLeader(User user, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, ProgramLeaders))
            return new SuccessAndMessage(false, "Person without access");

        if (!ProgramLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "Leader not found");
        ProgramLeaders.Remove(user.Id);
        return new SuccessAndMessage(true, $"Deleted leader {user.Id}");
    }
}