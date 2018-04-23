using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class ExistingFood
     {

        protected List<Tag> tagsAdded = new List<Tag>();
        protected Food foodType = null;
        protected int quantity = 1;

        public ExistingFood(Food food)
        {
            foodType = food;
        }

        public ExistingFood(ExistingFood other)
        {
            foodType = other.foodType;
            for(int i=0;i<other.tagsAdded.Count;++i)
            {
                tagsAdded.Add(other.tagsAdded[i]);
            }
        }
        public void AddTag(Tag newTag)
        {
            //for now, we can only have one type of tag
            if (!TagTypeExists(newTag.tagType))
            {
                tagsAdded.Add(newTag);
            }
        }

        public bool TagTypeExists(Tags.TagType type)
        {
            return tagsAdded.Exists(x => x.tagType == type);
        }

        public Food GetFoodType()
        {
            return foodType;
        }
        public bool HasTag(Tag tag)
        {
            return tagsAdded.Exists(x => x.tagName == tag.tagName);
        }

        public bool HasTags(ExistingFood other, Tag tag)
        {
            for (int i = 0; i < other.tagsAdded.Count; ++i)
            {
                if (!HasTag(other.tagsAdded[i]))
                    return false;
            }
            return tagsAdded.Exists(x => x.tagName == tag.tagName);
        }

        public bool IsFoodType(Food food)
        {
            return foodType.foodName == food.foodName;
        }

        public bool IsFoodType(ExistingFood food)
        {
            return foodType.foodName == food.foodType.foodName;
        }

        public void DecreaseQuantity()
        {
            if (tagsAdded.Count > 0)
            {
                quantity--;
            }
            else
            {
                quantity = 1;
            }
        }

        public void IncreaseQuantity()
        {
            if (tagsAdded.Count > 0)
            {
                quantity++;
            }
            else
            {
                quantity = 1;
            }
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public string GenerateName()
        {
            string text = "";
            for(int i=0;i<tagsAdded.Count;++i)
            {
                text += tagsAdded[i].tagName;
                text += " ";
            }
            text += foodType.foodName;
            return text;
        }

        public List<Tag> GetTags()
        {
            return tagsAdded;
        }
    }
}
