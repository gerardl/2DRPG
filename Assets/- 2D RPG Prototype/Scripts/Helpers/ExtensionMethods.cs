using System.Collections.Generic;

namespace RPGPrototype.Helpers
{
    public static class ExtensionMethods
    {
        public static T GetRandom <T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
