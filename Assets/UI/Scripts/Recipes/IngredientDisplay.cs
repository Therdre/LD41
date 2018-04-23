using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FoodNameSpace.Tags;
using FoodNameSpace;
using System;

namespace GameUI
{
    public class IngredientDisplay : MonoBehaviour
    {
        public Text foodType = null;
        public GameObject tagsParent = null;
        public TagDisplay tagsInstance = null;

        List<TagDisplay> tags = new List<TagDisplay>();
        private void Start()
        {
            int numberTags = Enum.GetNames(typeof(TagType)).Length;
            CreateTagsPlaceHolders(numberTags);
        }

        void CreateTagsPlaceHolders(int tagsType)
        {
            if(tags.Count>0)
            {
                return;
            }
            for (int i = 0; i < tagsType; ++i)
            {
                TagDisplay newTag = Instantiate(tagsInstance, tagsParent.transform);
                newTag.gameObject.SetActive(false);
                tags.Add(newTag);
            }
        }

        public void DisplayIngredient(RecipeIngredient ingredient)
        {
            if(tags.Count==0)
            {
                int numberTags = Enum.GetNames(typeof(TagType)).Length;
                CreateTagsPlaceHolders(numberTags);
            }
            foodType.text = ingredient.GetFoodType().foodName;
            List<Tag> ingredientTags = ingredient.GetTags();
            for (int i = 0; i < tags.Count; ++i)
            {
                tags[i].gameObject.SetActive(false);
            }

            for(int i=0;i<ingredientTags.Count;++i)
            {
                tags[i].DisplayTag(ingredientTags[i]);
                tags[i].gameObject.SetActive(true);
            }

        }
    }
}
