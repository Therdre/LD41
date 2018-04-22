using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace.Tags
{
    [CreateAssetMenu(fileName = "TagList", menuName = "FoodTags/List", order = 1)]
    public class TagList : ScriptableObject
    {
        public List<Tag> existingTags;

        public Tag GetRandomTagOfType(TagType tagType)
        {
            List<Tag> tagsOfType = existingTags.FindAll(x=>x.tagType==tagType);
            if (tagsOfType.Count == 0)
                return null;

            return tagsOfType[UnityEngine.Random.Range(0, tagsOfType.Count)];
        }
    }
}
