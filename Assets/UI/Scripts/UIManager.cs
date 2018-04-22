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
            actionSelected = true;
            CloseMenu();
        }

        public bool WasActionSelected()
        {
            return actionSelected;
        }
    }
}
