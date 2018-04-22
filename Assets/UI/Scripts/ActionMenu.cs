using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace.Tags;
using System.Linq;

namespace GameUI
{
    public class ActionMenu : Menu
    {
        public StationMenu stationsMenu = null;

        public override void CreateButtons()
        {
            if (tagsAvailable == null)
                return;

            List<Tag> availableTagsTypes = tagsAvailable.existingTags.GroupBy(x => x.tagType).Select(g => g.First()).ToList();

            for(int i=0;i<availableTagsTypes.Count;++i)
            {
                AddButton(availableTagsTypes[i], TagTypeToString(availableTagsTypes[i].tagType),true);
            }
            CreatePlateButton();
        }

        void CreatePlateButton()
        {
            ContextMenuButton newButton = Instantiate(buttonInstance, buttonsRoot.transform);
            newButton.gameObject.SetActive(true);
            newButton.buttonText.text = "Plate";
        }

        string TagTypeToString(TagType tagType)
        {
            if(tagType==TagType.COOKED)
            {
                return "Cook";
            }
            else if (tagType == TagType.CUT)
            {
                return "Cut";
            }
            else if (tagType == TagType.FANCY)
            {
                return "Fancy";
            }
            return "Plate";
        }

        public override void OpenNextMenu(Tag selectedTag)
        {
            stationsMenu.Open();
            stationsMenu.ActivateButtons(selectedTag);
        }

        public override void CloseNextMenu()
        {
            //stationsMenu.Close();
        }

        public override void Close()
        {
            base.Close();
            stationsMenu.Close();

        }
    }
}
