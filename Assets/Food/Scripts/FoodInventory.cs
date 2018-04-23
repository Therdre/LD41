using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    using Tags;

    public class FoodInventory : Singleton<FoodInventory>
    {
        public List<Food> rawFood = new List<Food>();
        public List<FoodIcons> inventoryDisplay = new List<FoodIcons>();

        List<ExistingFood> foodInventory = new List<ExistingFood>();
        int maxInventoryNumber = 0;
        int inventorySize = 0;
        void Start()
        {
            foodInventory.Clear();
            PopulateRawFood();
            maxInventoryNumber = inventoryDisplay.Count;

            for(int i=0;i<inventoryDisplay.Count;++i)
            {
                inventoryDisplay[i].gameObject.SetActive(false);
            }
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
            if(inventorySize==maxInventoryNumber)
            {
                return;
            }
            //check if the food with the tag already exists, if it does
            //increment it
            List<ExistingFood> foodType = foodInventory.FindAll(x => x.IsFoodType(food));

            //check if the tag exists
            ExistingFood foodWithTag = foodType.Find(x => x.HasTags(food,tag));
            if(foodWithTag!=null)
            {
                foodWithTag.IncreaseQuantity();
                DisplayFood(foodWithTag);
            }
            else
            {
                ExistingFood newFood = new ExistingFood(food);
                newFood.AddTag(tag);
                foodInventory.Add(newFood);
                DisplayFood(newFood);
            }
            inventorySize++;
            RemoveFood(food);
        }

        public void RemoveRecipeIngredients(Recipe recipe)
        {
            List<RecipeIngredient> ingredients = recipe.ingredientesList;

            for(int i=0;i<ingredients.Count;++i)
            {
                ExistingFood food = foodInventory.Find(x => x.EqualAs(ingredients[i]));
                RemoveFood(food);
                RemoveFoodFromDisplay(food);
            }
        }

        public void RemoveFood(ExistingFood food, bool removeFromDisplay=false)
        {
            if (food == null)
                return;

            if (removeFromDisplay)
            {
                RemoveFoodFromDisplay(food);
            }
            if (food.GetTags().Count > 0)
            {
                inventorySize--;
            }
            food.DecreaseQuantity();

            if (food.GetQuantity() <= 0)
            {
                foodInventory.Remove(food);
            }
            
        }

        public List<ExistingFood> GetAvailableFood(Tag tag)
        {
            if (inventorySize == maxInventoryNumber)
            {
                return new List<ExistingFood>();
            }
            List<ExistingFood> availableFood = foodInventory.FindAll(x => !x.TagTypeExists(tag.tagType));
            
            return availableFood;
        }

        public List<ExistingFood> GetAllInvetoryFood()
        {
            return foodInventory;
        }

        public void RemoveFoodFromDisplay(ExistingFood food)
        {
            FoodIcons availableSpace= inventoryDisplay.Find(x => x.gameObject.activeSelf && x.GetExistingFood()==food);
            if(availableSpace)
            {
                availableSpace.gameObject.SetActive(false);
            }
        }

        public void DisplayFood(ExistingFood food)
        {
            //get available inventory space
            FoodIcons availableSpace = inventoryDisplay.Find(x=>!x.gameObject.activeSelf);
            if (availableSpace == null)
                return;

            availableSpace.SetGraphics(food);
            availableSpace.gameObject.SetActive(true);

        }
    }
}
