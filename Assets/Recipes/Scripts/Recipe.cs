using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class RecipeIngredient: ExistingFood
    {
        List<Tag> optionalTags = new List<Tag>();
        public bool ingredientCompleted = false;

        public RecipeIngredient(Food food) : base(food)
        {

        }

        public void AddOptionalTag(Tag tag)
        {
            optionalTags.Add(tag);
        }


    }

    public class Recipe
    {
        public string name = "";
        public List<RecipeIngredient> ingredientesList = new List<RecipeIngredient>();
        bool completed = false;

        public void AddIngredient(RecipeIngredient ingredient)
        {
            if(name.Length!=0)
            {
                name += " and ";
            }
            name += ingredient.GenerateName();
            ingredientesList.Add(ingredient);
        }

        public void UpdateCompleteStatus(List<ExistingFood> inventoryFood)
        {
            completed = true;
            for (int i = 0; i < ingredientesList.Count; ++i)
            {
                ingredientesList[i].ingredientCompleted = inventoryFood.Exists(x=>x.EqualAs(ingredientesList[i]));
                if(!ingredientesList[i].ingredientCompleted)
                {
                    completed = false;
                }
            }

        }
        public bool IsCompleted()
        {
            //UpdateCompleteStatus(inventoryFood);
            return completed;
        }
    }
}
