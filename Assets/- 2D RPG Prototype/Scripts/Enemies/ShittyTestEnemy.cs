using RPGPrototype.Combat;
using UnityEngine;

namespace RPGPrototype.Enemies
{
    [System.Serializable]
    public class ShittyTestEnemy : Enemy
    {
        public Combat.DamageInfo damage;
        float tempTimer;

        protected override void Start()
        {
            base.Start();
            tempTimer = attackTime;
        }

        protected override void Update()
        {
            base.Update();
            if (target == null) return;
            
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            
            // too far to hit
            if (distanceToTarget > attackDistance) return;
            
            tempTimer -= Time.deltaTime;
            // Should really come after an animation
            if (tempTimer <= 0)
            {
                OnAttack();
                tempTimer = attackTime;
            }
        }

        
        protected virtual void OnAttack()
        {
            var hittableObject = (Combat.IHittable)target.GetComponent(typeof(Combat.IHittable));
            if (hittableObject != null)
                hittableObject.Hit(new HitInfo(damage, transform.position, true, HitInfo.HitSources.Enemy));
        }
    }
}
