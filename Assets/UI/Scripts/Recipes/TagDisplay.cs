using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FoodNameSpace.Tags;

namespace GameUI
{
    public class TagDisplay : MonoBehaviour
    {
        public Text text = null;
        public Image color = null;
        public Color completedTag = new Color(1f, 1f, 1f, 1f);
        public Color incompletedTag = new Color(1f, 1f, 1f, 1f);

        public void DisplayTag(Tag tag)
        {
            text.text = tag.tagName;    
        }

        public void SetCompleted(bool completed)
        {
            if(completed)
            {
                if(color!=null)
                {
                    color.color = completedTag;
                }
            }
            else
            {
                if (color != null)
                {
                    color.color = incompletedTag;
                }
            }
        }
    }
}
