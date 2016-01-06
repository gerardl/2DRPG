using UnityEngine;


namespace RPGPrototype.Combat
{
    public class AimedProjectile : Projectile
    {
        protected override void Start()
        {
            base.Start();

            // get the direction of the player -> mouse pointer
            var player = Player.PlayerController.Instance;
            Vector2 mousePosition = player.playerCamera.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - (Vector2)player.weaponController.weaponHand.transform.position;
            direction.Normalize();
        }
    }
}
