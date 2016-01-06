using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.UI
{ 
    public class UIController : Helpers.Singleton<UIController>
    {
        public DialogScreen dialogScreen;
        public InventoryScreen inventoryScreen;
        public HUD hudScreen;
        public SystemMessagesScreen systemMessageScreen;
    }
}

