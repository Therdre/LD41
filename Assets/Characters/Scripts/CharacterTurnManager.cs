using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameUI;
using UnityEngine.SceneManagement;
using FoodNameSpace;
using FoodNameSpace.Tags;

namespace CharacterNameSpace
{
    //shoudl this be technically the turn manager?
    public class CharacterTurnManager : MonoBehaviour
    {
        public Character[] characters = null;
        public float delayBetweenTurns = 2.0f;
        public Timer recipeTimer = null;
        [Header("Temp")]
        public GameObject fridge = null;
        public GameObject oven = null;

        bool turnPlaying = false;

        private void Start()
        {
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].SetCharacterDisplay(UIManager.Instance.GetCharacterInfo(i));
            }
        }
        private void Update()
        {
            if (!turnPlaying)
                StartCoroutine(PlayTurn());
        }

        IEnumerator PlayTurn()
        {
            turnPlaying = true;
            if (characters == null)
                yield break;

            
            //should be sorting characters first per skill
            characters = characters.OrderByDescending(x => x.Speed).ToArray();

            for (int i = 0; i < characters.Length; ++i)
            {
                if(characters[i].characterStress==0)
                {
                    continue;
                }
                //do this check first in case the 
                while(!RecipeManager.Instance.AreRecipesPending())
                {
                    RecipeManager.Instance.GenerateRecipe();
                    yield return StartCoroutine(RecipeManager.Instance.UpateRecipeStatus());
                    if(RecipeManager.Instance.AreRecipesPending())
                    {
                        recipeTimer.ResetTimer();
                    }
                }

                //1)select action, for now we are just gonna move
                UIManager.Instance.OpenMenu();
                while(!UIManager.Instance.WasActionSelected() && recipeTimer.IsTimerRunning())
                {
                    yield return new WaitForFixedUpdate();
                }

                if (recipeTimer.IsTimerRunning())
                {
                    UIManager.Instance.CloseMenu();
                    //2)Perform character action

                    Tag tag = UIManager.Instance.GetCurrentTag();
                    ExistingFood food = UIManager.Instance.GetCurrentFood();
                    characters[i].SetIcon(food.GetFoodType().GetIcon(tag), food.GetFoodType().GetColor(tag));


                    yield return StartCoroutine(characters[i].MoveTo(fridge.gameObject.transform.position));
                    characters[i].ShowIcon(true);
                    yield return new WaitForSeconds(0.2f);
                    yield return StartCoroutine(characters[i].MoveTo(oven.gameObject.transform.position));
                    yield return new WaitForSeconds(0.2f);
                    yield return StartCoroutine(characters[i].MoveToOriginalPosition());
                    FoodInventory.Instance.AddFood(tag, food);
                    characters[i].ShowIcon(false);
                }

                if(!recipeTimer.IsTimerRunning())
                {
                    yield return StartCoroutine(RecipeManager.Instance.FailedRecipe());
                    yield return StartCoroutine(AOEDamage(25,0.2f));

                }
                else
                {
                    yield return StartCoroutine(RecipeManager.Instance.UpateRecipeStatus());
                }
                

            }
            //yield return new WaitForSeconds(delayBetweenTurns);
            turnPlaying = false;
        }

        IEnumerator AOEDamage(int damage, float time)
        {
            float currentTime = 0.0f;

            List<int> initialStress = new List<int>();
            List<int> targetStress = new List<int>();
            for (int i = 0; i < characters.Length; ++i)
            {
                initialStress.Add(characters[i].characterStress);
                int target = characters[i].characterStress - damage;
                if(target < 0)
                {
                    target = 0;
                }
                targetStress.Add(target);
            }

            while (currentTime < time)
            {
                for (int i = 0; i < characters.Length; ++i)
                {
                    characters[i].SetStress((int)Mathf.Lerp((float)targetStress[i], (float)targetStress[i], currentTime / time));
                }
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].SetStress(targetStress[i]);
            }
        }
    }
}
