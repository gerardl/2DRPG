using UnityEngine;
using RPGPrototype.Combat;

namespace RPGPrototype.Interactive
{
    class DestructableObject : MonoBehaviour, IHittable
    {
        public int health = 100;

        public void Hit(HitInfo hitInfo)
        {
            if (health <= 0)
                return;

            int damage = hitInfo.Damage.maxPhysical;
            health -= damage;

            Debug.Log("You hit " + name + " for " + damage + ".");

            if (health <= 0)
            {
                Debug.Log("You destroyed " + name + ".");
                Die();
            }

        }

        private void Die()
        {
            // show destruction animation
            Destroy(gameObject);
        }
    }
}
