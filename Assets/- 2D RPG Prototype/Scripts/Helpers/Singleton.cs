using UnityEngine;
using System.Collections;

namespace RPGPrototype.Helpers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null || instance.gameObject == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but one was not found!");
                    }
                }
                return instance;
            }
        }
    }

}
