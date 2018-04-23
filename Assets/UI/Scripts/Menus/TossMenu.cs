using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;

namespace GameUI
{
    public class TossMenu : Menu
    {

        public int instancesPerButton = 8;
        // Use this for initialization
        public override void CreateButtons()
        {
            FoodInventory foodInventory = FoodInventory.Instance;
            for (int i = 0; i < foodInventory.rawFood.Count; ++i)
            {
                for (int j = 0; j < instancesPerButton; ++j)
                {
                    AddButton(null, foodInventory.rawFood[i].foodName, true, foodInventory.rawFood[i]);
                }
            }

        }

        public void OnButtonClick(Tag tag, ExistingFood food)
        {
            UIManager.Instance.TossFood(food);
        }

        public void AddButton(Tag tag, string name, bool active, Food food)
        {
            ContextMenuButton newButton = Instantiate(buttonInstance, buttonsRoot.transform);
            newButton.associatedTag = tag;
            newButton.associatedFood = null;
            newButton.gameObject.SetActive(active);
            newButton.onClick.AddListener(OnButtonClick);
            newButton.buttonText.text = name;
            buttons.Add(newButton);
        }

        public override void ActivateButtons(Tag tagToActivate)
        {
            //deactivate all buttons first
            for (int i = 0; i < buttons.Count; ++i)
            {
                buttons[i].gameObject.SetActive(false);
            }

            List<ExistingFood> availableFood = FoodInventory.Instance.GetAllInvetoryFood();
            availableFood = availableFood.FindAll(x=>x.GetTags().Count>0);
            for (int i = 0; i < availableFood.Count; ++i)
            {
                buttons[i].associatedTag = tagToActivate;
                buttons[i].associatedFood = availableFood[i];
                buttons[i].buttonText.text = availableFood[i].GenerateName();
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
