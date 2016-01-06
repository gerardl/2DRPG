using UnityEngine;
using RPGPrototype.Management;

namespace RPGPrototype.Interactive
{
    /// <summary>
    ///  
    ///     Allows objects to be pushed by the player character in a 
    ///     similar style to "Link to the Past".  
    ///     Rigidbody2D Setup: Turn off Linear Drag, Angular Drag, and Gravity Scale,
    ///     Freeze Z-Rotation.
    /// 
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PushableObject : MonoBehaviour
    {
        [Tooltip("Can we currently move this object.")]
        public bool movable = true;
        [Tooltip("Objects on these layers can move this GameObject.")]
        public LayerMask canBeMovedBy;
        [Tooltip("The time you need to be pushing against an object before it starts moving.")]
        public float buildUpTime = .5f;
        [Tooltip("The speed at which the object is pushed.")]
        [Range(0f, 5f)]
        public float moveSpeed = 2.5f;
        [Tooltip("Show lines indicating moveable directions in scene view.")]
        public bool showRaycast;
        [Tooltip("How far we cast out our ray")]
        public float raycastDistance = 1f;
        [Tooltip("Can the object be pushed in this direction.")]
        public bool canPushUp = true,
                    canPushDown = true,
                    canPushRight = true, 
                    canPushLeft = true;

        private Vector3 upPosition, 
                        downPosition, 
                        leftPosition, 
                        rightPosition;

        private bool isPushingDown = false,
                        isPushingUp = false,
                        isPushingRight = false, 
                        isPushingLeft = false;

        private float timer;
        private Rigidbody2D rigidBody2D;
        
        void Start()
        {
            timer = buildUpTime;
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!movable || !Player.PlayerController.Instance.CanMoveObjects) return;

            upPosition = new Vector3(transform.position.x, transform.position.y + raycastDistance, transform.position.z);
            downPosition = new Vector3(transform.position.x, transform.position.y - raycastDistance, transform.position.z);
            rightPosition = new Vector3(transform.position.x + raycastDistance, transform.position.y, transform.position.z);
            leftPosition = new Vector3(transform.position.x - raycastDistance, transform.position.y, transform.position.z);

            if (showRaycast) DrawRayCast();

            // Draw a line from the movable object 1 unit in all directions and see if we hit a layer we can be moved by, 
            // also check if the player is pushing in the opposite direction. (not just standing by the movable object)
            // is there a better way to check this by seeing if a player or any layer, really, is colliding with this and then still apply
            // constant force for a zelda like effect?  Can the collider tell me which direction I am pushing into? - glucas

            if (Physics2D.Linecast(transform.position, upPosition, canBeMovedBy) && InputController.ForwardBack == -1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isPushingDown = true;
            }
            else if (Physics2D.Linecast(transform.position, downPosition, canBeMovedBy) && InputController.ForwardBack == 1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isPushingUp = true;
            }
            else if (Physics2D.Linecast(transform.position, rightPosition, canBeMovedBy) && InputController.LeftRight == -1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isPushingLeft = true;
            }
            else if (Physics2D.Linecast(transform.position, leftPosition, canBeMovedBy) && InputController.LeftRight == 1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isPushingRight = true;
            }
            else
            {
                isPushingDown = false;
                isPushingUp = false;
                isPushingLeft = false;
                isPushingRight = false;
                timer = buildUpTime;
            }
        }

        void FixedUpdate()
        {
            Vector2 movement;

            if (isPushingDown && canPushDown)
                movement = new Vector2(0f, -1f);
            else if (isPushingUp && canPushUp)
                movement = new Vector2(0f, 1f);
            else if (isPushingLeft && canPushLeft)
                movement = new Vector2(-1f, 0f);
            else if (isPushingRight && canPushRight)
                movement = new Vector2(1f, 0f);
            else
                movement = new Vector2(0f, 0f);

            rigidBody2D.velocity = movement * moveSpeed;
        }
        
        void DrawRayCast()
        {
            if (canPushUp) Debug.DrawLine(transform.position, upPosition);
            if (canPushDown) Debug.DrawLine(transform.position, downPosition);
            if (canPushRight) Debug.DrawLine(transform.position, rightPosition);
            if (canPushLeft) Debug.DrawLine(transform.position, leftPosition);
        }
    }
}
