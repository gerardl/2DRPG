using UnityEngine;

namespace RPGPrototype.Enemies
{
    [System.Serializable]
    public class Maneater : Enemy
    {
        public GameObject projectile;
        public GameObject bulletSpawnPoint;

        protected bool sawPlayer;

        float tempTimer = 2f;
        

        protected override void Update()
        {
            base.Update();

            tempTimer -= Time.deltaTime;

            if (tempTimer <= 0)
            {
                OnShoot();
                tempTimer = 2f;
            }

            //if (!sawPlayer && !seePlayer)
            //{
            //    attackTime = Time.time;
            //}
            //else if (seePlayer)
        }

        // call after animation
        protected virtual void OnShoot()
        {
            //Instantiate(projectile, (Vector2)bulletSpawnPoint.transform.position, Quaternion.identity);
            //Instantiate(projectile, bulletSpawnPoint.transform.position, directionToPlayer);
            Instantiate(projectile, (Vector2)bulletSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
