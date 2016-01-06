using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RPGPrototype.Management
{
    public class MoveSceneOnTrigger : MonoBehaviour
    {

        public string levelToLoad;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player")
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}

