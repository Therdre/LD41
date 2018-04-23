using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class FoodInventory : Singleton<FoodInventory>
    {
        public List<Food> rawFood = new List<Food>();

        List<ExistingFood> foodInventory = new List<ExistingFood>();

        void Start()
        {
            foodInventory.Clear();
            PopulateRawFood();
        }
        void PopulateRawFood()
        {
            for(int i=0; i<rawFood.Count;++i)
            {
                ExistingFood newFood = new ExistingFood(rawFood[i]);
                foodInventory.Add(newFood);
            }
        }

        public void AddFood(Tag tag, ExistingFood food)
        {
            //check if the food with the tag already exists, if it does
            //increment it
            List<ExistingFood> foodType = foodInventory.FindAll(x => x.IsFoodType(food));

            //check if the tag exists
            ExistingFood foodWithTag = foodType.Find(x => x.HasTags(food,tag));
            if(foodWithTag!=null)
            {
                foodWithTag.IncreaseQuantity();
            }
            else
            {
                ExistingFood newFood = new ExistingFood(food);
                newFood.AddTag(tag);
                foodInventory.Add(newFood);
            }
            food.DecreaseQuantity();

            if(food.GetQuantity()<=0)
            {
                foodInventory.Remove(food);
            }
        }

        public void RemoveRecipeIngredients(Recipe recipe)
        {
            List<RecipeIngredient> ingredients = recipe.ingredientesList;

            for(int i=0;i<ingredients.Count;++i)
            {
                ExistingFood food = foodInventory.Find(x => x.EqualAs(ingredients[i]));
                RemoveFood(food);
            }
        }

        public void RemoveFood(ExistingFood food)
        {
            if (food == null)
                return;

            food.DecreaseQuantity();

            if (food.GetQuantity() <= 0)
            {
                foodInventory.Remove(food);
            }
        }

        public List<ExistingFood> GetAvailableFood(Tag tag)
        {
            List<ExistingFood> availableFood = foodInventory.FindAll(x => !x.TagTypeExists(tag.tagType));
            
            return availableFood;
        }

        public List<ExistingFood> GetAllInvetoryFood()
        {
            return foodInventory;
        }
    }
}
