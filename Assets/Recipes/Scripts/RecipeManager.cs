using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    public class RecipeManager : Singleton<RecipeManager>
    {

        public RecipeGenerator recipeGenerator = null;

        bool isRecipeBeingMade = false;

        private void Update()
        {
            if(!isRecipeBeingMade)
            {
                isRecipeBeingMade = true;
                recipeGenerator.GenerateRandomRecipe();
            }
        }
    }
}
