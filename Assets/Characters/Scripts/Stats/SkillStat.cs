using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNameSpace.Stats
{
    //determines speed and hit/miss chances
    public class SkillStat : Stat
    {
        [Tooltip("max is the miss chance at max skill")]
        public Vector3 missChanceRanges = new Vector2(1.0f,50.0f);
        // Use this for initialization

        public override int MissOutCome()
        {
            float t = Mathf.InverseLerp((float)minValue,(float)maxValue,(float)value);
            return (int)(Mathf.Lerp(missChanceRanges.x, missChanceRanges.y, t) * 100.0f);
        }
    }
}
