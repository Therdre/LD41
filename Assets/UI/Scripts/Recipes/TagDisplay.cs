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

        public void DisplayTag(Tag tag)
        {
            text.text = tag.tagName;
        }

    }
}
