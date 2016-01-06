using UnityEngine;
using System.Collections;

namespace RPGPrototype.Player
{
    // I will replace this soon
    public class CameraController : MonoBehaviour
    {
        public GameObject cameraTarget;
        private Vector3 targetPos;
        public float moveSpeed;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = new Vector3(cameraTarget.transform.position.x,
                                    cameraTarget.transform.position.y,
                                    transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}

