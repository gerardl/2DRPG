using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RPGPrototype.Player
{
    public class Level
    {
        public int Number { get; set; }
        public int NeededExperience { get; set; }
        public string RequiredItemId { get; set; }

        public Level(int number, int neededExp)
        {
            Number = number;
            NeededExperience = neededExp;
        }

        public Level(int number, int neededExp, string requiredItemId)
        {
            Number = number;
            NeededExperience = neededExp;
            RequiredItemId = requiredItemId;
        }
    }

    public class StatController : MonoBehaviour
    {
        public int pointsPerLevel = 5;
        public bool canLevel = true;

        public System.Action<int> onExperienceAdded;
        public System.Action<Level> onLevelGained;

        public List<Level> Levels { get; set; }

        public Level CurrentLevel { get; private set; }
        public int AvailablePoints { get; private set; }
        public int TotalPoints { get; private set; }
        public int CurrentExperience { get; private set; }
        public int TotalExperience { get; private set; }

        public int Strength { get; private set; }
        public int Stamina { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }

        public int ItemStrength { get; private set; }
        public int ItemStamina { get; private set; }
        public int ItemIntelligence { get; private set; }
        public int ItemWisdom { get; private set; }

        public int TotalStrength { get { return Strength + ItemStrength; } }
        public int TotalStamina { get { return Stamina + ItemStamina; } }
        public int TotalIntelligence { get { return Intelligence + ItemIntelligence; } }
        public int TotalWisdom { get { return Wisdom + ItemWisdom; } }

        void Awake()
        {
            Levels = new List<Level>();
            Levels.Add(new Level(1, 100));
            Levels.Add(new Level(2, 200));
            Levels.Add(new Level(3, 300));
            Levels.Add(new Level(4, 400));
            Levels.Add(new Level(5, 500));

            CurrentLevel = Levels.OrderBy(o => o.Number).First();
        }

        public void AddStrength(int amount, bool fromItem)
        {
            if (fromItem) ItemStrength += amount;
            else
            {
                Strength += amount;
                AddPoints(-amount);
            }
            
        }

        public void AddStamina(int amount, bool fromItem)
        {
            if (fromItem) ItemStamina += amount;
            else
            {
                Stamina += amount;
                AddPoints(-amount);
            }

        }

        public void AddIntelligence(int amount, bool fromItem)
        {
            if (fromItem) ItemIntelligence += amount;
            else
            {
                Intelligence += amount;
                AddPoints(-amount);
            }

        }

        public void AddWisdom(int amount, bool fromItem)
        {
            if (fromItem) ItemWisdom += amount;
            else
            {
                Wisdom += amount;
                AddPoints(-amount);
            }

        }

        public void AddExperience(int amount)
        {
            if (!canLevel) return;
            
            if (onExperienceAdded != null) onExperienceAdded(amount);

            CurrentExperience += amount;
            TotalExperience += amount;

            if (CurrentExperience >= CurrentLevel.NeededExperience)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            var nextLevel = Levels.Find(l => l.Number == (CurrentLevel.Number + 1));
            if (nextLevel == null)
            {
                canLevel = false;
                return;
            }

            AddPoints(pointsPerLevel);
            
            CurrentExperience -= CurrentLevel.NeededExperience;
            if (CurrentExperience < 0)
            {
                CurrentExperience = 0;
                return;
            } 
            
            CurrentLevel = nextLevel;
            if (onLevelGained != null) onLevelGained(CurrentLevel);

            if (CurrentExperience >= CurrentLevel.NeededExperience) LevelUp();
        }

        public void AddPoints(int points)
        {
            AvailablePoints += points;
            if (points > 0) TotalPoints += points;
        }
    }
}
