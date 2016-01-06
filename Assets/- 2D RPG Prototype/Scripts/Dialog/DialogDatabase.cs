using System.Collections.Generic;

namespace RPGPrototype.Dialog
{
    [System.Serializable]
    public class DialogDatabase : Helpers.Singleton<DialogDatabase>
    {
        public List<Dialog> dialogs = new List<Dialog>();

        //void Awake()
        //{
        //    if (dialogs == null)
        //        dialogs = new List<Dialog>();
        //}

        public Dialog GetDialogById(string id)
        {
            Dialog dialog = dialogs.Find(d => d.id == id);
            return dialog;
        }

        #region Editor

        public Dialog AddDialog()
        {
            Dialog dialog = new Dialog();
            dialogs.Add(dialog);
            return dialog;
        }

        public void RemoveDialog(Dialog dialog)
        {
            dialogs.Remove(dialog);
        }

        public string[] GetDialogsId()
        {
            string[] strings = new string[dialogs.Count];
            for (int i = 0; i < dialogs.Count; i++)
            {
                strings[i] = dialogs[i].id;
            }
            return strings;
        }

        #endregion

    }
}
