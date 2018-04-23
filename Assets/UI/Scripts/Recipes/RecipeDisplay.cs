using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using System;
using UnityEngine.UI;

namespace GameUI
{
    public class RecipeDisplay : MonoBehaviour
    {
        public GameObject ingredientsParent = null;
        public Text title = null;
        public IngredientDisplay ingredientsInstance = null;

        Recipe associatedRecipe = null;
        List<IngredientDisplay> ingredientsDisplay = new List<IngredientDisplay>();
        void Start()
        {
            int numberTags = Enum.GetNames(typeof(FoodType)).Length;
            CreateIngredientsPlaceHolders(numberTags);
            gameObject.SetActive(false);
        }

        void CreateIngredientsPlaceHolders(int foodsType)
        {
            if(ingredientsDisplay.Count>0)
            {
                return;
            }
            for (int i = 0; i < foodsType; ++i)
            {
                IngredientDisplay newIngredient = Instantiate(ingredientsInstance, ingredientsParent.transform);
                newIngredient.gameObject.SetActive(false);
                ingredientsDisplay.Add(newIngredient);
            }
        }

        public void Display(Recipe recipe)
        {
            associatedRecipe = recipe;
            if (ingredientsDisplay.Count == 0)
            {
                int numberTags = Enum.GetNames(typeof(FoodType)).Length;
                CreateIngredientsPlaceHolders(numberTags);
            }

            List<RecipeIngredient> ingredients = recipe.ingredientesList;
            title.text = recipe.name;

            for (int i = 0; i < ingredientsDisplay.Count; ++i)
            {
                ingredientsDisplay[i].gameObject.SetActive(false);
            }

            for (int i=0;i<ingredients.Count;++i)
            {
                ingredientsDisplay[i].DisplayIngredient(ingredients[i]);
                ingredientsDisplay[i].gameObject.SetActive(true);
            }
        }

        public void UpdateCompletedDisplay()
        {
            if (associatedRecipe == null)
                return;

            List<RecipeIngredient> ingredients = associatedRecipe.ingredientesList;
            for (int i = 0; i < ingredients.Count; ++i)
            {
                ingredientsDisplay[i].SetCompletedDisplay(ingredients[i].ingredientCompleted, ingredients[i].partialTags);
            }
        }
    }
}
