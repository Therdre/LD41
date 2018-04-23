using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;
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
                Recipe currentRecipe=recipeGenerator.GenerateRandomRecipe();
                RecipeDisplay recipeDisplay=UIManager.Instance.GetAvailableRecipeDisplay();

                if(recipeDisplay!=null)
                {
                    recipeDisplay.gameObject.SetActive(true);
                    recipeDisplay.Display(currentRecipe);
                }
            }
        }
    }
}
