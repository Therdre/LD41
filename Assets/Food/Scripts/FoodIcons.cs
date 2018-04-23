using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace;
using FoodNameSpace.Tags;

public class FoodIcons : MonoBehaviour
{
    public SpriteRenderer foodGraphic = null;
    public SpriteRenderer plateGraphic = null;

    ExistingFood associatedFood = null;

    public void SetGraphics(ExistingFood food)
    {
        associatedFood = food;
        foodGraphic.sprite = food.GetFoodType().GetIcon(food.GetTagOfType(TagType.CUT));

        Tag cookTag = food.GetTagOfType(TagType.COOKED);
        if (cookTag != null)
        {
            plateGraphic.sprite = cookTag.plateIcon;
        }
        else
        {
            plateGraphic.sprite = null;
        }
    }

    public ExistingFood GetExistingFood()
    {
        return associatedFood;
    }

}
