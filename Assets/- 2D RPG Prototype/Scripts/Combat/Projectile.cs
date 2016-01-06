using UnityEngine;

namespace RPGPrototype.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        public DamageInfo damage;
        public float speed = 10;
        public float bulletLifetime = 5f;
        public HitInfo.HitSources hitSource;
        public LayerMask hittableLayers;
        public GameObject hitEffect;
        public Vector2 direction;

        protected virtual void Start()
        {
            // only allow it to live for so long
            Destroy(gameObject, bulletLifetime);
        }

        protected virtual void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
            CheckHit();
        }
        
        //void FixedUpdate()
        //{
        //    transform.Translate(direction * speed * Time.deltaTime);
        //    CheckHit();
        //}

        protected virtual void CheckHit()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Helpers.GameConstants.RAYCAST_DIST, hittableLayers);
            IHittable hittableObject;
            if (hit.collider != null)
            {
                //Debug.Log("bullet hit: " + hit.collider.gameObject.name);
                hittableObject = (IHittable)hit.collider.gameObject.GetComponent(typeof(IHittable));
                if (hittableObject != null)
                {
                    hittableObject.Hit(new HitInfo(damage, transform.position, true, hitSource));
                    //Debug.Log("hit hittableObject: " + hit.collider.gameObject.name);
                }
                DestroyOnImpact(hit.collider.transform.position);
            }
        }

        protected virtual void DestroyOnImpact(Vector3 hitLocation)
        {
            // play destroy animation
            GameObject go = (GameObject)Instantiate(hitEffect, hitLocation, Quaternion.identity);
            Destroy(gameObject);
            Destroy(go, 2f);
        }
    }
}
