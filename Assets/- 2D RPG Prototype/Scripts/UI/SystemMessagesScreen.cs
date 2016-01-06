using RPGPrototype.Management;
using UnityEngine;
using UnityEngine.UI;

namespace RPGPrototype.UI
{
    public class SystemMessagesScreen : UIWindow
    {
        public Text systemMessageText;
        public Image systemMessageImage;

        public float systemMessageTimer = 2f;
        public const float maxMessageTimer = 5f;
        private float currentTimer = 0f;
        void Update()
        {
            if (!IsVisible) return;

            if (currentTimer > 0f)
                currentTimer -= Mathf.Clamp((currentTimer - Time.deltaTime), 0, maxMessageTimer);
            else
            {
                Hide();
                currentTimer = 0f;
            }
        }
        public void ShowSystemMessage(string messageText, Sprite messageImage)
        {
            Show();
            systemMessageText.text = messageText;
            systemMessageImage.sprite = messageImage;
            currentTimer += systemMessageTimer;
        }

        public void OnExperienceAdded(int amount)
        {
            ShowSystemMessage("You got" + amount + "Exp!", null);
        }

        public void OnLevelGained(Player.Level level)
        {
            ShowSystemMessage("You are now level " + level.Number, null);
            Debug.Log("level: " + level.Number + " points avail: " + Player.PlayerController.Instance.statController.AvailablePoints + "next level exp: " + level.NeededExperience);
        }
    }
}
