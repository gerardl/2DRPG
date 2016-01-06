

namespace RPGPrototype.Combat
{
    [System.Serializable]
    public class DamageInfo
    {
        public int minPhysical = 0;
        public int maxPhysical = 1;
        public int fire = 0;
        public int ice = 0;
        public int lightning = 0;
        public int water = 0;

        public DamageInfo Copy()
        {
            DamageInfo newInfo = new DamageInfo();
            newInfo.minPhysical = this.minPhysical;
            newInfo.maxPhysical = this.maxPhysical;
            newInfo.fire = this.fire;
            newInfo.lightning = this.lightning;
            newInfo.water = this.water;

            return newInfo;
        }
    }
}
