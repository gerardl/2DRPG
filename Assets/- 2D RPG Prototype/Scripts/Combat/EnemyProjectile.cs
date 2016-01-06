using UnityEngine;


namespace RPGPrototype.Combat
{
    public class EnemyProjectile : Projectile
    {
        protected override void Start()
        {
            base.Start();

            // Move towards target player
            var player = Player.PlayerController.Instance;
            direction = player.transform.position - transform.position;
            direction.Normalize();
        }
    }
}