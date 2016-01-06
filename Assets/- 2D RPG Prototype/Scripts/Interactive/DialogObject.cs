using System;
using UnityEngine;
using RPGPrototype.Dialog;

namespace RPGPrototype.Interactive
{
    public class DialogObject : MonoBehaviour, IInteractive
    {
        public string objectName;
        public string dialogId;

        public string Name { get { return objectName; } }
        public bool IsActive { get { return true; } }
        public InteractiveAction Action { get { return InteractiveAction.Read; } }

        public void Use()
        {
            Dialog.Dialog dialog = DialogDatabase.Instance.GetDialogById(dialogId);
            if (dialog != null)
            {
                //GameController.Instance.GameState = GameStates.Dialog;
                UI.UIController.Instance.dialogScreen.Show();
                UI.UIController.Instance.dialogScreen.SetDialog(dialog);
            }
        }
    }
}
