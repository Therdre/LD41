using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FoodNameSpace.Tags;
using UnityEngine.EventSystems;
using FoodNameSpace;

namespace GameUI
{
    [System.Serializable]
    public class ContextMenuEvent : UnityEvent<Tag>
    {

    }

    [System.Serializable]
    public class FoodMenuEvent : UnityEvent<Tag, ExistingFood>
    {

    }


    public class ContextMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Text buttonText = null;
        public Tag associatedTag = null;
        public ExistingFood associatedFood = null;
        public ContextMenuEvent onHover = null;
        public UnityEvent onExitHover=null;
        public FoodMenuEvent onClick = null;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onHover != null)
                onHover.Invoke(associatedTag);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (onExitHover!=null)
                onExitHover.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
                onClick.Invoke(associatedTag, associatedFood);
        }


    }
}
