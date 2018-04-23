using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;
using CharacterNameSpace;
namespace FoodNameSpace
{
    public class RecipeManager : Singleton<RecipeManager>
    {

        public RecipeGenerator recipeGenerator = null;
        public AudioSource recipeCompleted = null;
        bool isRecipeBeingMade = false;
        public int completedRecipes = 0;
        
        Recipe currentRecipe = null;
        RecipeDisplay recipeDisplay = null;
        /*private void Update()
        {
            if(!isRecipeBeingMade)
            {
                isRecipeBeingMade = true;
                currentRecipe=recipeGenerator.GenerateRandomRecipe();
                recipeDisplay=UIManager.Instance.GetAvailableRecipeDisplay();

                if (recipeDisplay!=null)
                {
                    recipeDisplay.gameObject.SetActive(true);
                    recipeDisplay.Display(currentRecipe);
                }
            }
        }*/

        public void GenerateRecipe()
        {
            currentRecipe = recipeGenerator.GenerateRandomRecipe();
            recipeDisplay = UIManager.Instance.GetAvailableRecipeDisplay();
            isRecipeBeingMade = true;
            if (recipeDisplay != null)
            {
                recipeDisplay.gameObject.SetActive(true);
                recipeDisplay.Display(currentRecipe);
            }
        }

        public bool AreRecipesPending()
        {
            return isRecipeBeingMade;
        }

        public IEnumerator UpateRecipeStatus()
        {
            if (currentRecipe != null)
            {
                currentRecipe.UpdateCompleteStatus(FoodInventory.Instance.GetAllInvetoryFood());
            }
            if(recipeDisplay!=null)
            {
                recipeDisplay.UpdateCompletedDisplay();
            }

            //for some sort of celebration
            if (currentRecipe.IsCompleted())
            {
                yield return new WaitForSeconds(0.5f);
                completedRecipes++;
                if (recipeCompleted!=null)
                {
                    recipeCompleted.Play();
                }
                if (recipeDisplay != null)
                {
                    recipeDisplay.gameObject.SetActive(false);
                }
                FoodInventory.Instance.RemoveRecipeIngredients(currentRecipe);
                CharacterTurnManager.Instance.DecreaseTimer();
                isRecipeBeingMade = false;
            }
            
        }

        public IEnumerator FailedRecipe()
        {
            yield return new WaitForSeconds(0.5f);
            if (recipeDisplay != null)
            {
                recipeDisplay.gameObject.SetActive(false);
            }
            isRecipeBeingMade = false;
        }

        public void ResetGame()
        {
            completedRecipes = 0;
            isRecipeBeingMade = false;
            //currentRecipe = null;
            //recipeDisplay = null;
        }
    }
}
