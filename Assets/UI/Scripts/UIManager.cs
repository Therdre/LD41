using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;
using System.Linq;
using UnityEngine.UI;

namespace GameUI
{
    public class UIManager : Singleton<UIManager>
    {
        public ActionMenu mainMenu = null;
        public List<RecipeDisplay> recipeDisplays = new List<RecipeDisplay>();
        public Slider timeDisplay = null;
        public List<CharacterInfo> characterInfo = new List<CharacterInfo>();

        bool actionSelected = false;
        public bool tossSelected = false;
        Tag currentTag = null;
        ExistingFood currentFood = null;
        public void OpenMenu()
        {
            actionSelected = false;
            mainMenu.Open();
        }

        public void CloseMenu()
        {
            mainMenu.Close();
        }

        public void FoodButtonClicked(Tag tag, ExistingFood food)
        {
            currentTag = tag;
            currentFood = food;
            actionSelected = true;
            tossSelected = false;
            CloseMenu();
        }

        public void TossFood(ExistingFood food)
        {
            currentTag = null;
            tossSelected = true;
            currentFood = food;
            actionSelected = true;
            CloseMenu();
        }

        public bool WasActionSelected()
        {
            return actionSelected;
        }

        public Tag GetCurrentTag()
        {
            return currentTag;
        }

        public ExistingFood GetCurrentFood()
        {
            return currentFood;
        }

        public RecipeDisplay GetAvailableRecipeDisplay()
        {
            return recipeDisplays.FirstOrDefault(x => !x.gameObject.activeSelf);
        }

        public CharacterInfo GetCharacterInfo(int index)
        {
            if (index >= characterInfo.Count)
                return null;

            return characterInfo[index];
        }
    }
}
