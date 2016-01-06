using UnityEngine;

namespace RPGPrototype.Combat
{
    [System.Serializable]
    public class DefenseInfo
    {
        [Range(0f, 1f)]
        public float Physical = 0;
        [Range(0f, 1f)]
        public float Fire = 0;
        [Range(0f, 1f)]
        public float Ice = 0;
        [Range(0f, 1f)]
        public float Lightning = 0;
        [Range(0f, 1f)]
        public float Water = 0;

        public int CalculateFinalDamage(DamageInfo damage, bool fromPlayer = false)
        {
            int final = 0;

            final = (int)(Random.Range(damage.minPhysical, damage.maxPhysical) * (1f - Mathf.Clamp(Physical, 0f, 1f)));
            final += (int)(damage.fire * (1f - Mathf.Clamp(Fire, 0f, 1f)));
            final += (int)(damage.ice * (1f - Mathf.Clamp(Ice, 0f, 1f)));
            final += (int)(damage.lightning * (1f - Mathf.Clamp(Lightning, 0f, 1f)));
            final += (int)(damage.water * (1f - Mathf.Clamp(Water, 0f, 1f)));

            return final;
        }
    }
}
