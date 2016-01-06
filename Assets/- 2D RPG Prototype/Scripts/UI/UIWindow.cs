using UnityEngine;

namespace RPGPrototype.UI
{
    public class UIWindow : MonoBehaviour
    {
        public GameObject mainPanel;

        public bool IsVisible { get; private set; }

        public virtual void Show()
        {
            mainPanel.SetActive(true);
            IsVisible = true;
        }

        public virtual void Hide()
        {
            mainPanel.SetActive(false);
            IsVisible = false;
        }

        public void ToggleState()
        {
            if (IsVisible) Hide();
            else Show();
        }
    }
}
