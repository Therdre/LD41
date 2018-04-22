using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    [CreateAssetMenu(fileName = "Tag", menuName = "FoodTags/Food", order = 1)]
    public class Food : ScriptableObject
    {
        public string foodName = "";
        public Sprite foodSprite = null;
        public Sprite cutSprite = null;
        
        List<Tag> tagsAdded = new List<Tag>();

        public void AddTag(Tag newTag)
        {
            //for now, we can only have one type of tag
            if(!TagTypeExists(newTag.tagType))
            {
                tagsAdded.Add(newTag);
            }
        }

        public bool TagTypeExists(Tags.TagType type)
        {
            return tagsAdded.Exists(x => x.tagType == type);
        }
    }
}
