using System.Collections;
using System.Collections.Generic;
using FoodNameSpace.Tags;
using UnityEngine;

namespace GameUI
{
    public class StationMenu : Menu
    {
        public FoodMenu foodMenu = null;
        public override void CreateButtons()
        {
            if (tagsAvailable == null)
                return;

            for (int i = 0; i < tagsAvailable.existingTags.Count; ++i)
            {
                AddButton(tagsAvailable.existingTags[i], tagsAvailable.existingTags[i].actionName, false);
            }

        }

        public override void ActivateButtons(Tag tagToActivate)
        {
            for(int i=0;i<buttons.Count;++i)
            {
                if(buttons[i].associatedTag.tagType==tagToActivate.tagType)
                {
                    buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

        public override void OpenNextMenu(Tag selectedTag)
        {
            foodMenu.Open();
            foodMenu.ActivateButtons(selectedTag);
        }

        public override void Close()
        {
            base.Close();
            foodMenu.Close();
        }
    }
}
