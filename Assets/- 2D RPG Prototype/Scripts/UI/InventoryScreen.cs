using RPGPrototype.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.UI
{
    public class InventoryScreen : UIWindow
    {
        void Update()
        {
            if (InputController.Inventory)
                ToggleState();
        }
    }
}
