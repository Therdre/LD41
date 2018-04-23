using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;
using UnityEngine.UI;
using System.Linq;

namespace GameUI
{
    public class FoodMenu : Menu
    {

        public int instancesPerButton = 8;
        public AudioSource buttonClick = null;

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
            UIManager.Instance.FoodButtonClicked(tag, food);
            if(buttonClick!=null)
            {
                buttonClick.Play();
            }
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

            List<ExistingFood> availableFood = FoodInventory.Instance.GetAvailableFood(tagToActivate);
            availableFood = availableFood.OrderByDescending(x => x.GetTags().Count).ToList();
            for (int i=0;i< availableFood.Count;++i)
            {
                buttons[i].associatedTag = tagToActivate;
                buttons[i].associatedFood = availableFood[i];
                buttons[i].buttonText.text = availableFood[i].GenerateName();
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
