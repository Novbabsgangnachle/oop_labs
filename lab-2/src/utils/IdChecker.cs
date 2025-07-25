using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_2.utils;

public class IdChecker
{
    public static bool HasAccess(Guid id, IEnumerable<Guid> authors)
    {
        return authors.Contains(id);
    }
}