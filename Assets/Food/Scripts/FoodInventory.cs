using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodNameSpace
{
    [CreateAssetMenu(fileName = "Tag", menuName = "FoodTags/FoodList", order = 1)]
    public class FoodInventory : ScriptableObject
    {
        public List<Food> rawFood = new List<Food>();

        Dictionary<string, int> foodInventory = new Dictionary<string, int>();


    }
}
