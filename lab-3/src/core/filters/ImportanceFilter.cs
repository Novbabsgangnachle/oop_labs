using lab_3.core.interfaces;
using lab_3.core.utils;

namespace lab_3.core.filters;

public class ImportanceFilter
{
    private ImportanceLevel MinimumImportance { get; }

    public ImportanceFilter(ImportanceLevel minimumImportance)
    {
        MinimumImportance = minimumImportance;
    }

    public bool ShouldFilter(IMessage message)
    {
        return message.Importance < MinimumImportance;
    }
}