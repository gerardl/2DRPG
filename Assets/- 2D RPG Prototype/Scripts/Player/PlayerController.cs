using UnityEngine;
using System.Collections;
using RPGPrototype.Combat;
using System;
using RPGPrototype.Management;
using RPGPrototype.Interactive;
using RPGPrototype.Helpers;

namespace RPGPrototype.Player
{
    public class PlayerController : Helpers.Singleton<PlayerController>, IHittable
    {
        public Camera playerCamera;

        public Vector2 mousePosition;
        public Vector2 mouseDirection;

        public float moveSpeed;
        private Animator animator;
        private Rigidbody2D playerRigidbody;
        private bool isMoving;
        private Vector2 lastXAndY;

        public WeaponController weaponController;
        public InventoryController inventoryController;
        public StatController statController;
        public DefenseInfo defenseInfo;

        public LayerMask interactableLayers;

        public IInteractive InteractiveObject { get; private set; }

        public int startMaxHealth = 100;
        public int startMaxMana = 100;
        private int maxHealth;
        private int maxMana;
        [SerializeField]
        private int health;
        [SerializeField]
        private int mana;

        // move this to "stat/leveling" controller most likely
        public bool CanMoveObjects = false;

        public int MaxHealth
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                if (Health > MaxHealth) Health = MaxHealth;
            }
        }

        public int MaxMana
        {
            get { return maxMana; }
            set
            {
                maxMana = value;
                if (Mana > MaxMana) Mana = MaxMana;
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = Mathf.Clamp(value, 0, maxHealth);
                if (health == 0) Die();
            }
        }

        public int Mana
        {
            get { return mana; }
            set
            {
                mana = Mathf.Clamp(value, 0, maxMana);
            }
        }

        // Use this for initialization
        void Start()
        {
            InitPlayer();
            SetCallbacks();
        }

        private void InitPlayer()
        {
            maxHealth = startMaxHealth;
            maxMana = startMaxMana;
            Health = maxHealth;
            Mana = maxMana;
            animator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody2D>();

            DontDestroyOnLoad(transform.gameObject);
        }

        private void SetCallbacks()
        {
            statController.onExperienceAdded += UI.UIController.Instance.systemMessageScreen.OnExperienceAdded;
            statController.onLevelGained += UI.UIController.Instance.systemMessageScreen.OnLevelGained;
            inventoryController.onSetWeapon += weaponController.OnSetWeapon;
        }

        // Update is called once per frame
        void Update()
        {
            if (animator == null)
            {
                Debug.LogError("No Animator found on player character, exiting!");
                return;
            }

            #region Set Mouse Position & Direction

            mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection = mousePosition - (Vector2)weaponController.weaponHand.transform.position;
            mouseDirection.Normalize();

            animator.SetFloat(AnimationVariables.MOUSE_DIRECTION_X, mouseDirection.x);
            animator.SetFloat(AnimationVariables.MOUSE_DIRECTION_Y, mouseDirection.y);

            #endregion

            #region Movement

            

            // reset moving state
            isMoving = false;

            var xAxisInput = Input.GetAxisRaw("Horizontal");
            var yAxisInput = Input.GetAxisRaw("Vertical");

            // moving left or right
            if (xAxisInput > 0.5f || xAxisInput < -0.5f)
            {
                playerRigidbody.velocity = new Vector2(xAxisInput * moveSpeed, playerRigidbody.velocity.y);
                isMoving = true;
                lastXAndY = new Vector2(xAxisInput, 0);
            }
            // moving up or down
            if (yAxisInput > 0.5f || yAxisInput < -0.5f)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, yAxisInput * moveSpeed);
                isMoving = true;
                lastXAndY = new Vector2(0, yAxisInput);
            }
            // stopped moving left or right
            if (xAxisInput < 0.5f && xAxisInput > -0.5f)
            {
                playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
            }
            // stopped moving up or down
            if (yAxisInput < 0.5f && yAxisInput > -0.5f)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            }

            // relay movement state to animator
            animator.SetFloat(AnimationVariables.MOVE_X, xAxisInput);
            animator.SetFloat(AnimationVariables.MOVE_Y, yAxisInput);
            animator.SetBool(AnimationVariables.IS_MOVING, isMoving);
            animator.SetFloat(AnimationVariables.LAST_X, lastXAndY.x);
            animator.SetFloat(AnimationVariables.LAST_Y, lastXAndY.y);

            #endregion

            #region Interact With Stuff

            if (InputController.Use)
            {
                // standard ray for mouse position
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // translate this to 2d intersection
                var rayIntersect = Physics2D.GetRayIntersection(ray, interactableLayers);

                // try to find an object that we are allowed to interact with
                // or things we can hit.
                InteractiveObject = null;
                if (rayIntersect.collider != null)
                {
                    InteractiveObject = (IInteractive)rayIntersect.collider.gameObject.GetComponent(typeof(IInteractive));

                    // found interactable object and it is not inactive, use it
                    if (InteractiveObject != null && InteractiveObject.IsActive != false)
                        InteractiveObject.Use();
                }
            }

            #endregion
        }

        public void Hit(HitInfo hitInfo)
        {

            // see if they blocked
            var wasBlocked = false;
            if (weaponController.IsBlocking)
            {
                var hitDirection = hitInfo.Point - transform.position;
                hitDirection.Normalize();
                var angle = Vector2.Angle(mouseDirection, hitDirection);
                //Debug.Log("mousedir: " + mouseDirection + " projectiledir: " + hitDirection + " angle: " + angle);
                if (angle <= 90)
                {
                    wasBlocked = true;
                    Debug.Log("you blocked");
                }
            }

            int damage = defenseInfo.CalculateFinalDamage(hitInfo.Damage);
            // move to calculate
            damage = wasBlocked ? (damage / 2) : damage;
            Health -= damage;
            Debug.Log("dmg: " + damage);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}


