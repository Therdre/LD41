using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;

namespace GameUI
{
    public class UIManager : Singleton<UIManager>
    {
        public ActionMenu mainMenu = null;

        bool actionSelected = false;
        Tag currentTag = null;
        Food currentFood = null;
        public void OpenMenu()
        {
            actionSelected = false;
            mainMenu.Open();
        }

        public void CloseMenu()
        {
            mainMenu.Close();
        }

        public void FoodButtonClicked(Tag tag, Food food)
        {
            currentTag = tag;
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

        public Food GetCurrentFood()
        {
            return currentFood;
        }
    }
}
