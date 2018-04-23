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

        [Header("Completed/Incomplete recipe")]
        public Image color = null;
        public Color completedTag = new Color(1f, 1f, 1f, 1f);
        public Color incompletedTag = new Color(1f, 1f, 1f, 1f);

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

        public void SetCompletedDisplay(bool completed, List<Tag> partialTags)
        {
            if (completed || partialTags.Count>0)
            {
                if (color != null)
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

            for (int i = 0; i < tags.Count; ++i)
            {
                tags[i].SetCompleted(completed || partialTags.Exists(x=>x.tagName==tags[i].text.text));
            }
        }
    }
}
