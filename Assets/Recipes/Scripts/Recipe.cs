using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class RecipeIngredient: ExistingFood
    {
        List<Tag> optionalTags = new List<Tag>();
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
        
        public void AddIngredient(RecipeIngredient ingredient)
        {
            ingredientesList.Add(ingredient);
        }
    }
}
