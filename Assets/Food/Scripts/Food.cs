using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public enum FoodType
    {
        MEAT,
        VEGETABLE,
        DESSERT,
    }

    [CreateAssetMenu(fileName = "Tag", menuName = "FoodTags/Food", order = 1)]
    public class Food : ScriptableObject
    {
        public string foodName = "";
        public Sprite foodSprite = null;
        public Sprite cutSprite = null;
        public Sprite mincedSprite = null;
        public FoodType foodType = FoodType.MEAT;

        public Sprite GetIcon(Tag tag)
        {
            if(tag==null)
            {
                return foodSprite;
            }
            if(tag.tagName=="Sliced")
            {
                return cutSprite;
            }
            else if(tag.tagName=="Minced")
            {
                return mincedSprite;
            }
            return foodSprite;
        }
    }
}
