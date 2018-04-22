using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FoodNameSpace.Tags;
using UnityEngine.EventSystems;

namespace GameUI
{
    [System.Serializable]
    public class ContextMenuEvent : UnityEvent<Tag>
    {

    }

    public class ContextMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Text buttonText = null;
        public Tag associatedTag = null;
        public ContextMenuEvent onHover = null;
        public UnityEvent onExitHover=null;

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
    }
}
