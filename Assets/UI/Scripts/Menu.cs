using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace.Tags;

namespace GameUI
{
    public class Menu : MonoBehaviour
    {

        public GameObject menuRoot = null;
        public GameObject buttonsRoot = null;
        
        public ContextMenuButton buttonInstance = null;
        public TagList tagsAvailable = null;

        protected List<ContextMenuButton> buttons = new List<ContextMenuButton>();

        protected virtual void Start()
        {
            CreateButtons();
            Close();
        }

        public virtual void AddButton(Tag tag, string name, bool active)
        {
            ContextMenuButton newButton = Instantiate(buttonInstance, buttonsRoot.transform);
            newButton.associatedTag = tag;
            newButton.gameObject.SetActive(active);
            newButton.onHover.AddListener(OpenNextMenu);
            newButton.onExitHover.AddListener(CloseNextMenu);
            newButton.buttonText.text = name;
            buttons.Add(newButton);
        }

        public void Open()
        {
            menuRoot.SetActive(true);
        }

        public virtual void Close()
        {
            menuRoot.SetActive(false);
        }

        public virtual void CreateButtons()
        {

        }
        public virtual void OpenNextMenu(Tag selectedTag)
        {
        }

        public virtual void CloseNextMenu()
        {

        }

        public virtual void ActivateButtons(Tag tagToActivate)
        {

        }
    }
}
