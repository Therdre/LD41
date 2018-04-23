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
        public Color grilledColor=new Color(1f,1f,1f,1f);
        public Color friedColor = new Color(1f, 1f, 1f, 1f);
        public Color boiledColor = new Color(1f, 1f, 1f, 1f);
        public Color bakedColor = new Color(1f, 1f, 1f, 1f);

        public Sprite GetIcon(Tag tag)
        {
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

        public Color GetColor(Tag tag)
        {
            if (tag.tagName == "Baked")
            {
                return bakedColor;
            }
            else if (tag.tagName == "Boiled")
            {
                return boiledColor;
            }
            if (tag.tagName == "Fried")
            {
                return friedColor;
            }
            else if (tag.tagName == "Grilled")
            {
                return grilledColor;
            }
            return new Color(1f, 1f, 1f, 1f);
        }
    }
}
