﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;
using UnityEngine.UI;

namespace GameUI
{
    public class FoodMenu : Menu
    {
        public FoodInventory foodInventory = null;

        // Use this for initialization
        public override void CreateButtons()
        {
            if (foodInventory == null)
                return;


            for (int i = 0; i < foodInventory.rawFood.Count; ++i)
            {
                AddButton(null, foodInventory.rawFood[i].foodName, true);
            }

        }

        public void OnButtonClick(Tag tag,Food food)
        {
            UIManager.Instance.FoodButtonClicked(tag, food);
        }

        public override void AddButton(Tag tag, string name, bool active)
        {
            ContextMenuButton newButton = Instantiate(buttonInstance, buttonsRoot.transform);
            newButton.associatedTag = tag;
            newButton.gameObject.SetActive(active);
            newButton.onClick.AddListener(OnButtonClick);
            newButton.buttonText.text = name;
            buttons.Add(newButton);
        }
    }
}
