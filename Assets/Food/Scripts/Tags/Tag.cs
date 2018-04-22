﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace.Tags
{
    public enum TagType
    {
        CUT,
        COOKED,
        FANCY
    }

    [CreateAssetMenu(fileName = "Tag", menuName = "FoodTags/Tag", order = 1)]
    public class Tag: ScriptableObject
    {
        public string tagName = "";
        public TagType tagType = TagType.CUT;

    }
}
