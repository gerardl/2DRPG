using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.Dialog
{
    [System.Serializable]
    public class Dialog
    {
        public string id;
        public List<DialogPage> dialogPages;

        public Dialog()
        {
            id = "Dialog" + GetHashCode();
            dialogPages = new List<DialogPage>();
        }

        public override string ToString()
        {
            return id;
        }

        #region Editor

        public DialogPage AddNewItem()
        {
            DialogPage item = new DialogPage();
            dialogPages.Add(item);
            return item;
        }

        public void Remove(DialogPage item)
        {
            dialogPages.Remove(item);
        }

        #endregion
    }
}
