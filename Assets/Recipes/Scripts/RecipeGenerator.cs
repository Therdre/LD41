using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FoodNameSpace
{
    using Tags;
    public class RecipeGenerator : MonoBehaviour
    {
        public TagList availableTags=null;

        [Header("Random recipe generator stuff")]
        public float meatChance = 0.5f;
        public float vegetableChange = 0.5f;
        public int maxTags = 3;

        Recipe currentRecipe = null;

        public void GenerateRandomRecipe()
        {
            currentRecipe = new Recipe();
            //Add a meat ingredient??
            if(Chance(0.5f))
            {
                currentRecipe.AddIngredient(GenerateIngredient(FoodType.MEAT));                
            }

            if(Chance(0.5f))
            {
                currentRecipe.AddIngredient(GenerateIngredient(FoodType.VEGETABLE));
            }
        }

        public RecipeIngredient GenerateIngredient(FoodType foodType)
        {
            List<Food> foodList = FoodInventory.Instance.rawFood.FindAll(x => x.foodType == foodType);

            if (foodList.Count == 0)
                return null;

            Food food = foodList[UnityEngine.Random.Range(0, foodList.Count)];
            RecipeIngredient ingredient = new RecipeIngredient(food);
            
            int numberTags= Enum.GetNames(typeof(TagType)).Length;
            for (int i=0;i<numberTags;++i)
            {
                if(Chance(0.5f))
                {
                    Tag tagToAdd=availableTags.GetRandomTagOfType((TagType)i);

                    //add as optional
                    if(Chance(0.5f))
                    {
                        ingredient.AddOptionalTag(tagToAdd);
                    }
                    else
                    {
                        ingredient.AddTag(tagToAdd);
                    }
                }
            }

            return ingredient;
        }

        bool Chance(float percentage)
        {
            float chance = UnityEngine.Random.value;
            if (chance <= percentage)
                return true;

            return false;
        }
    }
}
