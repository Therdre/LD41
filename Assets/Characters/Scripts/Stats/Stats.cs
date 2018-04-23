using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNameSpace.Stats

{
    public class Stat : MonoBehaviour
    {
        public string statName = "";
        public int value = 10;
        public int maxValue = 100;
        public int minValue = 0;
        public string tooltip = "";
        
        public virtual float MissOutCome()
        {
            return 0.0f;
        }

        public virtual int StressLoss(int initialLoss)
        {
            return initialLoss;
        }
    }
}
