using UnityEngine;

namespace RPGPrototype.Combat
{
    [System.Serializable]
    public class HitInfo
    {
        public DamageInfo Damage;
        public Vector3 Point;
        public bool ShouldShowEffect = true;
        public HitSources Source;

        public enum HitSources
        {
            None = 0,
            Player = 1,
            Enemy = 2,
            Enviroment = 3
        }

        public HitInfo() { }

        public HitInfo(DamageInfo damage, Vector3 point, bool shouldShowEffect, HitSources source)
        {
            this.Damage = damage;
            this.Point = point;
            this.ShouldShowEffect = shouldShowEffect;
            this.Source = source;
        }
    }
}
