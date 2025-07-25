using System;
using System.Collections.ObjectModel;
using lab_2.dto;
using lab_2.interfaces;
using lab_2.interfaces.entities;
using lab_2.utils;

namespace lab_2.entities;

public class LectureMaterial : ILectureMaterial, IEntity
{
    public Guid Id { get; }

    public string Name { get; private set; }

    public string Summary { get; private set; }

    public string Content { get; private set; }

    public Collection<Guid> LectureMaterialLeaders { get; }

    public Guid CopyFromId { get; }

    public LectureMaterial(Guid id, string name, string summary, string content, Guid copyFromId, Collection<Guid> lectureMaterialLeaders)
    {
        Id = id;
        Name = name;
        Summary = summary;
        Content = content;
        CopyFromId = copyFromId;
        LectureMaterialLeaders = lectureMaterialLeaders;
    }

    public LectureMaterial(ILectureMaterial lectureMaterial)
    {
        Id = Guid.NewGuid();
        Name = lectureMaterial.Name;
        Summary = lectureMaterial.Summary;
        Content = lectureMaterial.Content;
        CopyFromId = lectureMaterial.Id;
        LectureMaterialLeaders = new Collection<Guid>(lectureMaterial.LectureMaterialLeaders);
    }

    public SuccessAndMessage TrySetName(string newName, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LectureMaterialLeaders))
            return new SuccessAndMessage(false, "Person without access.");

        if (string.IsNullOrWhiteSpace(newName))
            return new SuccessAndMessage(false, "Name cannot be empty");

        Name = newName;
        return new SuccessAndMessage(true, $"Set name {Name}");
    }

    public SuccessAndMessage TrySetSummary(string newSummary, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LectureMaterialLeaders))
            return new SuccessAndMessage(false, "Person without access.");

        if (string.IsNullOrWhiteSpace(newSummary))
            return new SuccessAndMessage(false, "Summary cannot be empty");

        Summary = newSummary;
        return new SuccessAndMessage(true, $"Set summary {Summary}");
    }

    public SuccessAndMessage TrySetContent(string newContent, Guid changerId)
    {
        if (!IdChecker.HasAccess(changerId, LectureMaterialLeaders))
            return new SuccessAndMessage(false, "Person without access.");

        if (string.IsNullOrWhiteSpace(newContent))
            return new SuccessAndMessage(false, "Content cannot be empty");

        Content = newContent;
        return new SuccessAndMessage(true, $"Set content {Content}");
    }

    public SuccessAndMessage TryAddLectureMaterialLeader(User user, Guid changerId)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!IdChecker.HasAccess(changerId, LectureMaterialLeaders))
            return new SuccessAndMessage(false, "Person without access.");

        if (LectureMaterialLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "This leader is already in");

        LectureMaterialLeaders.Add(user.Id);
        return new SuccessAndMessage(true, $"Added leader {user.Id}");
    }

    public SuccessAndMessage TryDeleteLectureMaterialLeader(User user, Guid changerId)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!IdChecker.HasAccess(changerId, LectureMaterialLeaders))
            return new SuccessAndMessage(false, "Person without access.");

        if (!LectureMaterialLeaders.Contains(user.Id))
            return new SuccessAndMessage(false, "Leader not found");

        LectureMaterialLeaders.Remove(user.Id);
        return new SuccessAndMessage(true, $"Deleted leader {user.Id}");
    }

    public ILectureMaterial Clone()
    {
        return new LectureMaterial(this);
    }
}