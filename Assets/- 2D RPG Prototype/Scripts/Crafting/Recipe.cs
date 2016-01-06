using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGPrototype.Crafting
{
    [System.Serializable]
    public abstract class Recipe
    {
        public enum RecipeTypes
        {
            Potion = 1,
            Armor = 2,
            Weapon = 3,
            Shield = 4
        }

        public string Id;
        public string Name;
        public string Description;
        public Sprite Icon;

        public Recipe() { }

    }
}
