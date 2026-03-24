using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtilities
{
    public static class DataProcess
    {
        public static bool CheckContains(this IEnumerable<ICheckable> collection, object element)
        => collection.Any(x => x.Check(element));
    }
}
