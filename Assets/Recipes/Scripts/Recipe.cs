using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class RecipeIngredient: ExistingFood
    {
        List<Tag> optionalTags = new List<Tag>();
        public bool ingredientCompleted = false;
        public List<Tag> partialTags = new List<Tag>();

        public RecipeIngredient(Food food) : base(food)
        {

        }

        public void AddOptionalTag(Tag tag)
        {
            optionalTags.Add(tag);
        }

        public void ClearPartialTags()
        {
            partialTags.Clear();
        }
        public void CheckPartialIngredients(ExistingFood food)
        {
            //food needs to have less items, and those items need to be a perfect math
            List<Tag> otherTags = food.GetTags();
            if (otherTags.Count >= tagsAdded.Count || otherTags.Count<= partialTags.Count || food.GetFoodType().foodName!=this.foodType.foodName)
                return;

            bool allTagsExist = true;
            for(int i=0;i< otherTags.Count;++i)
            {
                if(!HasTag(otherTags[i]))
                {
                    allTagsExist = false;
                    break;
                }
            }

            if(allTagsExist)
            {
                partialTags = otherTags;
            }
        }

    }

    public class Recipe
    {
        public string name = "";
        public List<RecipeIngredient> ingredientesList = new List<RecipeIngredient>();
        bool completed = false;

        public void AddIngredient(RecipeIngredient ingredient)
        {
            if(name.Length!=0)
            {
                name += " and ";
            }
            name += ingredient.GenerateName();
            ingredientesList.Add(ingredient);
        }

        public void UpdateCompleteStatus(List<ExistingFood> inventoryFood)
        {
            completed = true;
            for (int i = 0; i < ingredientesList.Count; ++i)
            {
                ingredientesList[i].ClearPartialTags();
                ingredientesList[i].ingredientCompleted = inventoryFood.Exists(x=>x.EqualAs(ingredientesList[i]));
                if(!ingredientesList[i].ingredientCompleted)
                {
                    completed = false;
                    //check for partial tags
                    for(int j=0;j<inventoryFood.Count;++j)
                    {
                        if (inventoryFood[j].GetTags().Count > 0)
                        {
                            ingredientesList[i].CheckPartialIngredients(inventoryFood[j]);
                        }
                    }
                }
            }
        }

        public bool IsCompleted()
        {
            //UpdateCompleteStatus(inventoryFood);
            return completed;
        }
    }
}
