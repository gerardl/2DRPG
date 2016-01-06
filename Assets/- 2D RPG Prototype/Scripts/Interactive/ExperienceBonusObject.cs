using UnityEngine;

namespace RPGPrototype.Interactive
{
    class ExperienceBonusObject : MonoBehaviour, IInteractive
    {
        public string objectName = "Experience Cache";
        public bool destroyOnUse = true;
        public int experienceToGive = 100;

        public string Name { get { return objectName; } }
        public bool IsActive { get { return true; } }
        public InteractiveAction Action { get { return InteractiveAction.Use; } }

        void Start()
        {
            
        }

        public void Use()
        {
            Player.PlayerController.Instance.statController.AddExperience(experienceToGive);
            if (destroyOnUse) Destroy(gameObject);
        }
    }
}
