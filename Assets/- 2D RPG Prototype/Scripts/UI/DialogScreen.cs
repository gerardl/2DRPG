using RPGPrototype.Management;
using RPGPrototype.Dialog;
using UnityEngine.UI;

namespace RPGPrototype.UI
{
    public class DialogScreen : UIWindow
    {
        public Image iconImage;
        public Text nameText;
        public Text messageText;

        private Dialog.Dialog currentDialog;
        private int pageCount = 0;

        void Update()
        {
            if (!IsVisible) return;

            if (InputController.Use)
            {
                pageCount++;
                if (pageCount >= currentDialog.dialogPages.Count)
                    Hide();
                else
                    DrawDialog();
            }
        }

        public void SetDialog(Dialog.Dialog dialog)
        {
            if (dialog == null || dialog.dialogPages.Count == 0)
            {
                UIController.Instance.dialogScreen.Hide();
                return;
            }
            currentDialog = dialog;
            pageCount = 0;
            DrawDialog();
        }

        private void DrawDialog()
        {
            var currentPage = currentDialog.dialogPages[pageCount];
            iconImage.sprite = currentPage.icon;
            nameText.text = currentPage.name;
            messageText.text = currentPage.text;
        }
    }
}
