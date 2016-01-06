using System;
using RPGPrototype.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace RPGPrototype.Enemies
{
    [System.Serializable]
    public abstract class Enemy : MonoBehaviour, IHittable
    {
        public SpriteRenderer spriteRenderer;
        public DefenseInfo defenseInfo = new DefenseInfo();
        public int health = 100;
        public int experience = 100;
        [SerializeField]
        protected float attackTime = 2f;
        [SerializeField]
        protected float attackDistance = 1.09f;
        [SerializeField]
        protected float movementSpeed = 1.5f;
        [SerializeField]
        protected GameObject target;

        protected bool seeTarget = false;
        protected float distanceToTarget;
        protected Quaternion directionToTarget;

        protected virtual void Start()
        {
            // for now
            target = Player.PlayerController.Instance.gameObject;
        }

        protected virtual void Update()
        {
            if (target == null) return;
            distanceToTarget = Vector2.Distance(transform.position, target.transform.position);   
        }

        public void Hit(HitInfo hitInfo)
        {
            if (health <= 0)
                return;

            int damage = defenseInfo.CalculateFinalDamage(hitInfo.Damage);
            health -= damage;

            Debug.Log("You hit " + name + " for " + damage + "!");

            if (health <= 0)
            {
                if (hitInfo.Source == HitInfo.HitSources.Player)
                {
                    Player.PlayerController.Instance.statController.AddExperience(experience);
                }  

                Debug.Log("You killed " + name + "!");
                Die();
            }

        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
