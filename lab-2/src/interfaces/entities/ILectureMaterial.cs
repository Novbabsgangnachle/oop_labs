using System;
using System.Collections.ObjectModel;
using lab_2.dto;

namespace lab_2.interfaces.entities;

public interface ILectureMaterial
{
    Guid Id { get; }

    string Name { get; }

    string Summary { get; }

    string Content { get; }

    Collection<Guid> LectureMaterialLeaders { get; }

    Guid CopyFromId { get; }

    SuccessAndMessage TrySetName(string newName, Guid changerId);

    SuccessAndMessage TrySetSummary(string newSummary, Guid changerId);

    SuccessAndMessage TrySetContent(string newContent, Guid changerId);

    SuccessAndMessage TryAddLectureMaterialLeader(User user, Guid changerId);

    SuccessAndMessage TryDeleteLectureMaterialLeader(User user, Guid changerId);

    ILectureMaterial Clone();
}