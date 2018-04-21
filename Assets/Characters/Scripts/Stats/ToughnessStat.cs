using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNameSpace.Stats
{
    public class ToughnessStat : Stat
    {
        public int multiplier = 2;
        // Use this for initialization

        public override int StressLoss(int initialLoss)
        {
            if (multiplier > 0)
            {
                return initialLoss / multiplier;
            }

            return initialLoss;

        }
    }
}
