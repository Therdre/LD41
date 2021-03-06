﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodNameSpace.Tags;
using System.Linq;

namespace StationsNamespace
{

    public class CookingStation : MonoBehaviour
    {
        public Tag[] tagOutput = null;
        public string animatorTrigger = "";
        public ParticleSystem damageEffect = null;

        public bool HasTag(Tag tag)
        {
            return tagOutput.Contains(tag);
        }

        public void PlayDamageEffect()
        {
            if(damageEffect!=null)
            {
                damageEffect.Play();
            }
        }
    }
}
