using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameUI;
using UnityEngine.SceneManagement;
using FoodNameSpace;
using FoodNameSpace.Tags;
using StationsNamespace;
namespace CharacterNameSpace
{
    //shoudl this be technically the turn manager?
    public class CharacterTurnManager : Singleton<CharacterTurnManager>
    {
        public Character[] characters = null;
        public float delayBetweenTurns = 2.0f;
        public Timer recipeTimer = null;

        [Header("Playgame stuff")]
        public int stressLossDamage = 5;
        public float damageChance = 0.3f;

        public float initialRecipeTime = 30f;
        public float minRecipeTime = 5f;
        [Header("Temp")]
        public GameObject fridge = null;
        public List<CookingStation> cookingStations = new List<CookingStation>();
        public Animator cookingStationAnimator = null;

        bool turnPlaying = false;
        bool done = false;
        float currentTimer = 0.0f;
        private void Start()
        {
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].SetCharacterDisplay(UIManager.Instance.GetCharacterInfo(i));
            }
            currentTimer = initialRecipeTime;
        }
        private void Update()
        {
            if (!turnPlaying && !done && MainMenu.Instance.gameStarted)
                StartCoroutine(PlayTurn());
        }

        public CookingStation SelectCookingStation(Tag actionTag)
        {
            return cookingStations.Find(x => x.HasTag(actionTag));
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
                    characters[i].CheckIfKO();
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

                UIManager.Instance.CloseMenu();
                if (recipeTimer.IsTimerRunning())
                {
                    
                    //2)Perform character action
                    Tag tag = UIManager.Instance.GetCurrentTag();
                    ExistingFood food = UIManager.Instance.GetCurrentFood();

                    if (!UIManager.Instance.tossSelected)
                    {
                        yield return StartCoroutine(CharacterCooking(characters[i], tag, food));
                    }
                    else
                    {
                        FoodInventory.Instance.RemoveFood(food,true);
                    }
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
                characters[i].CheckIfKO();

            }
            //yield return new WaitForSeconds(delayBetweenTurns);
            //check if we are done with the game
            done = true;
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].CheckIfKO();
                if (characters[i].characterStress > 0)
                {
                    
                    done =false;
                }
            }
            if(done)
            {
                MainMenu.Instance.ShowScoreScreen(RecipeManager.Instance.completedRecipes);
            }
            turnPlaying = false;
        }


        IEnumerator CharacterCooking(Character character,Tag tag, ExistingFood food)
        {           

            //walk to fridge
            if (food.GetTags().Count == 0)
            {
                yield return StartCoroutine(character.MoveTo(fridge.gameObject.transform.position));
                if (cookingStationAnimator != null)
                {
                    cookingStationAnimator.SetTrigger("OpenFridge");
                }
                yield return new WaitForSeconds(1f);
                character.SetIcon(food.GetFoodType().GetIcon(null));
            }
            //remove from the display and just walk directly to station
            else
            {
                FoodInventory.Instance.RemoveFoodFromDisplay(food);
                character.SetIcon(food.GetFoodType().GetIcon(food.GetTagOfType(TagType.CUT)));

                Tag cookTag = food.GetTagOfType(TagType.COOKED);
                if (cookTag != null)
                {
                    character.SetPlateIcon( cookTag.plateIcon);
                }
                else
                {
                    character.SetPlateIcon( null);
                }

            }

            character.ShowIcon(true);
            character.ShowPlateIcon(false);
            
            //select station
            CookingStation station = SelectCookingStation(tag);
            yield return StartCoroutine(character.MoveTo(station.gameObject.transform.position));
            if (cookingStationAnimator != null)
            {
                if (station.animatorTrigger.Length > 0)
                {
                    cookingStationAnimator.SetTrigger(station.animatorTrigger);
                }
            }

            yield return new WaitForSeconds(0.8f);

            //check for hit/miss chance
            float chance = UnityEngine.Random.value;
            bool miss = false;
            bool damage = false;
            //miss
            float characterMissChance = character.GetMissChance();
            if (chance<= characterMissChance)
            {
                miss = true;
                
            }
            else
            {
                chance = UnityEngine.Random.value;
                damage = chance <= damageChance;
            }

            if (!damage)
            {
                character.PlayOutComeEffect(miss);
            }
            else
            {
                character.Damaged(stressLossDamage);
            }
            yield return new WaitForSeconds(0.2f);

            if (!miss)
            {
                if (tag.tagType == TagType.CUT)
                {
                    character.SetIcon(food.GetFoodType().GetIcon(tag));
                }

                character.SetPlateIcon(tag.plateIcon);
                character.ShowPlateIcon(true);
            }
            yield return StartCoroutine(character.MoveToOriginalPosition());

            if (!miss)
            {
                FoodInventory.Instance.AddFood(tag, food);
            }
            else if(food.GetTags().Count>0)
            {
                FoodInventory.Instance.DisplayFood(food);
            }
            character.ShowIcon(false);
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


            //while (currentTime < time)
            int damageApplied = 0;
            
            while (damageApplied<damage)
            {
                for (int i = 0; i < characters.Length; ++i)
                {
                    //characters[i].SetStress((int)Mathf.Lerp((float)targetStress[i], (float)targetStress[i], currentTime / time));
                    if (damageApplied == 0)
                    {
                        characters[i].Damaged(1);
                    }
                    else
                    {
                        characters[i].Damaged(1,false);
                    }

                }
                //currentTime += Time.deltaTime;

                damageApplied++;
                yield return new WaitForEndOfFrame();
            }

            /*for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].SetStress(targetStress[i]);
            }*/
        }

        public void ResetGame()
        {
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].ResetInfo();
            }
            done = false;
            turnPlaying = false;
            currentTimer = initialRecipeTime;
            recipeTimer.SetTime(currentTimer);
        }

        public void DecreaseTimer()
        {
            
            currentTimer -= 5.0f;
            if (currentTimer < minRecipeTime)
            {
                currentTimer = minRecipeTime;
            }
            recipeTimer.SetTime(currentTimer);
        }
    }
}
