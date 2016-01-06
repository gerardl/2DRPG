using RPGPrototype.Items;
using RPGPrototype.Management;
using RPGPrototype.Helpers;
using UnityEngine;

namespace RPGPrototype.Player
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject weaponHand;
        public GameObject shieldHand;
        public GameObject tempWeapon;
        public GameObject weaponArcPoint;
        public bool ShowRaycast;
        private ItemWeapon weapon;
        private Animator animator;
        private PlayerController player;
        public LayerMask hittableLayers;

        public bool IsAttacking { get; private set; }
        public bool IsLastAttackComplete { get; private set; }
        public bool IsBlocking { get; private set; }

        void Start()
        {
            animator = GetComponent<Animator>();
            player = PlayerController.Instance;
            IsLastAttackComplete = true;
        }

        void Update()
        {
            // move this
            if (ShowRaycast) DrawRaycast();
            
            //if (GameController.Instance.GameState != GameStates.Game) return;
            
            if (weapon == null)
                return;

            IsAttacking = InputController.WeaponClick && IsLastAttackComplete;
            IsBlocking = !InputController.WeaponClick && InputController.Block && IsLastAttackComplete;

            
            //animator.SetBool("IsBlocking", IsBlocking);

            if (IsAttacking)
            {
                IsLastAttackComplete = false;
                animator.SetBool(AnimationVariables.IS_ATTACKING, IsAttacking);
            }

            if (IsBlocking)
            {
                shieldHand.SetActive(true);
                return;
            }
            else if (shieldHand.activeInHierarchy)
                shieldHand.SetActive(false);
        }

        // Fired From Player Animation Event
        public void OnAttack()
        {
            if (weapon == null)
                return;

            if (weapon.itemType == Item.ItemTypes.MeleeWeapon)
            {
                // show a temp melee weapon spot
                if (tempWeapon != null)
                {
                    var ang = Vector2.Angle(player.mouseDirection, weaponArcPoint.transform.position);
                    Debug.Log(ang);
                    tempWeapon.SetActive(true);
                    tempWeapon.transform.position = (Vector2)weaponArcPoint.transform.position + player.mouseDirection;
                    //tempWeapon.transform.LookAt(player.mouseDirection);
                }

                var meleeWeapon = (ItemWeaponMelee)weapon;

                Combat.IHittable HittableObject;
                var rays = Physics2D.CircleCastAll((Vector2)weaponArcPoint.transform.position + player.mouseDirection, meleeWeapon.attackArc, player.mouseDirection, .01f, hittableLayers);
                if (rays != null)
                {
                    for (var i = 0; i < rays.Length; i++)
                    {
                        //Debug.Log("hit " + rays[i].collider.name);
                        HittableObject = (Combat.IHittable)rays[i].collider.gameObject.GetComponent(typeof(Combat.IHittable));

                        if (HittableObject != null && weapon != null)
                        {
                            Combat.DamageInfo damageInfo = meleeWeapon.damage.Copy();
                            // add damage bonuses from player stats here
                            HittableObject.Hit(new Combat.HitInfo(damageInfo, rays[i].collider.transform.position, false, Combat.HitInfo.HitSources.Player));
                        }
                    }
                }
            }
            else if (weapon.itemType == Item.ItemTypes.RangedWeapon)
            {
                var rangedWeapon = (ItemWeaponRanged)weapon;
                Instantiate(rangedWeapon.projectile, (Vector2)weaponHand.transform.position, Quaternion.identity);
            }
        }

        // Fired From Player Animation Event
        public void OnAttackComplete()
        {
            // getting in here twice, i think because of the blend tree
            //Debug.Log("complete");
            //if (IsLastAttackComplete) return;
            IsLastAttackComplete = true;
            animator.SetBool(AnimationVariables.IS_ATTACKING, false);
            if (tempWeapon != null) tempWeapon.SetActive(false);
        }

        public void OnSetWeapon(ItemWeapon weapon)
        {
            this.weapon = weapon;
            animator.SetBool(AnimationVariables.IS_ATTACKING, false);
            //if (weaponModel != null) Destroy(weaponModel);
            //if (weapon == null)
            //{
            //    animator.SetInteger("WeaponType", -1);
            //}
            //else
            //{
            //    weaponModel = (GameObject)Instantiate(weapon.model);
            //    weaponModel.transform.parent = weaponHand.transform;
            //    weaponModel.transform.localPosition = Vector3.zero;
            //    weaponModel.transform.localRotation = Quaternion.identity;
            //    weaponModel.transform.localScale = Vector3.one;
            //    animator.SetInteger("WeaponType", ItemWeapon.GetAnimationId(weapon));
            //}
        }

        private void DrawRaycast()
        {
            //Vector3 mousePosition = player.playerCamera.ScreenToWorldPoint(Input.mousePosition);
            //mousePosition.z = 0;
            Debug.DrawRay(transform.position, (Vector3)player.mousePosition - transform.position, Color.red);
        }
    }
}
