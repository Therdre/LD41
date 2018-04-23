using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class CharacterInfo : MonoBehaviour
    {
        public Text charactername = null;
        public Slider stressLevel = null;
        public Text stressText = null;
        public void SetName(string name)
        {
            charactername.text = name;
        }

        public void UpdateStress(int stress)
        {
            stressLevel.value = stress;
            stressText.text = "" + stress + "%";
        }
    }
}
