using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace.Tags
{
    [CreateAssetMenu(fileName = "TagList", menuName = "FoodTags/List", order = 1)]
    public class TagList : ScriptableObject
    {
        public List<Tag> existingTags;

    }
}
