using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.Items
{
    [System.Serializable]
    public class ItemWeapon : Item
    {
        public ItemWeapon()
        {
        }

        public override void Activate()
        {
            base.Activate();
            Player.PlayerController.Instance.inventoryController.SetActiveWeapon(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Player.PlayerController.Instance.inventoryController.Additem(this);
        }
    }
}
