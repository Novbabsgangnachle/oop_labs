using System;
using System.Collections.ObjectModel;
using System.Linq;
using lab_2.dto;
using lab_2.interfaces;
using lab_2.interfaces.entities;

namespace lab_2.entities;

public class User(string name) : IEntity
{
    public string Name { get; } = name;

    public Guid Id { get; } = Guid.NewGuid();

    private Collection<ILaboratoryWork> LaboratoryWorks { get; set; } = [];

    private Collection<ILectureMaterial> LectureMaterials { get; set; } = [];

    private Collection<EducationalProgram> EducationalPrograms { get; set; } = [];

    private Collection<ISubject> Subjects { get; set; } = [];

    public SuccessAndMessage TryAddLaboratoryWork(ILaboratoryWork laboratoryWork)
    {
        ArgumentNullException.ThrowIfNull(laboratoryWork);

        if (!laboratoryWork.LaboratoryLeaders.Contains(Id))
            return new SuccessAndMessage(false, "This person isn't a leader of the Laboratory Work.");
        LaboratoryWorks.Add(laboratoryWork);
        return new SuccessAndMessage(true, $"Added laboratory work {laboratoryWork.Id}");

    }

    public SuccessAndMessage TryAddLectureMaterial(ILectureMaterial lectureMaterial)
    {
        ArgumentNullException.ThrowIfNull(lectureMaterial);

        if (!lectureMaterial.LectureMaterialLeaders.Contains(Id))
            return new SuccessAndMessage(false, "This person isn't a leader of the lecture material.");
        LectureMaterials.Add(lectureMaterial);
        return new SuccessAndMessage(true, $"Added lecture material {lectureMaterial.Id}");

    }

    public SuccessAndMessage TryAddEducationalProgram(EducationalProgram educationalProgram)
    {
        ArgumentNullException.ThrowIfNull(educationalProgram);

        if (!educationalProgram.ProgramLeaders.Contains(Id))
            return new SuccessAndMessage(false, "This person isn't a leader of the Educational Program.");
        EducationalPrograms.Add(educationalProgram);
        return new SuccessAndMessage(true, $"Added educational program {educationalProgram.Id}");

    }

    public SuccessAndMessage TryAddSubject(ISubject subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        if (!subject.SubjectLeaders.Contains(Id))
            return new SuccessAndMessage(false, "This person isn't a leader of the Subject.");
        Subjects.Add(subject);
        return new SuccessAndMessage(true, $"Added subject {subject.Id}");

    }

    public SuccessAndMessage TryRemoveLaboratoryWork(Guid laboratoryWorkId)
    {
        var laboratoryWork = LaboratoryWorks.FirstOrDefault(lw => lw.Id == laboratoryWorkId);
        if (laboratoryWork == null)
        {
            return new SuccessAndMessage(false, "Laboratory Work not found.");
        }

        if (!laboratoryWork.LaboratoryLeaders.Contains(Id))
        {
            return new SuccessAndMessage(false, "This person isn't a leader of the Laboratory Work.");
        }

        LaboratoryWorks.Remove(laboratoryWork);
        return new SuccessAndMessage(true, $"Removed laboratory work {laboratoryWorkId}");
    }

    public SuccessAndMessage TryRemoveLectureMaterial(Guid lectureMaterialId)
    {
        var lectureMaterial = LectureMaterials.FirstOrDefault(lm => lm.Id == lectureMaterialId);
        if (lectureMaterial == null)
        {
            return new SuccessAndMessage(false, "Lecture Material not found.");
        }

        if (!lectureMaterial.LectureMaterialLeaders.Contains(Id))
        {
            return new SuccessAndMessage(false, "This person isn't a leader of the Lecture Material.");
        }

        LectureMaterials.Remove(lectureMaterial);
        return new SuccessAndMessage(true, $"Removed lecture material {lectureMaterialId}");
    }

    public SuccessAndMessage TryRemoveEducationalProgram(Guid educationalProgramId)
    {
        var educationalProgram = EducationalPrograms.FirstOrDefault(ep => ep.Id == educationalProgramId);
        if (educationalProgram == null)
        {
            return new SuccessAndMessage(false, "Educational Program not found.");
        }

        if (!educationalProgram.ProgramLeaders.Contains(Id))
        {
            return new SuccessAndMessage(false, "This person isn't a leader of the Educational Program.");
        }

        EducationalPrograms.Remove(educationalProgram);
        return new SuccessAndMessage(true, $"Removed educational program {educationalProgramId}");
    }

    public SuccessAndMessage TryRemoveSubject(Guid subjectId)
    {
        var subject = Subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject == null)
        {
            return new SuccessAndMessage(false, "Subject not found.");
        }

        if (!subject.SubjectLeaders.Contains(Id))
        {
            return new SuccessAndMessage(false, "This person isn't a leader of the Subject.");
        }

        Subjects.Remove(subject);
        return new SuccessAndMessage(true, $"Removed subject {subjectId}");
    }
}