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
        [Header("Temp")]
        public GameObject fridge = null;
        public GameObject oven = null;

        bool turnPlaying = false;

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

            yield return new WaitForSeconds(delayBetweenTurns);
            //should be sorting characters first per skill
            characters = characters.OrderByDescending(x => x.Speed).ToArray();

            for (int i = 0; i < characters.Length; ++i)
            {
                //1)select action, for now we are just gonna move
                UIManager.Instance.OpenMenu();
                while(!UIManager.Instance.WasActionSelected())
                {
                    yield return new WaitForFixedUpdate();
                }
                UIManager.Instance.CloseMenu();
                //2)Perform character action

                Tag tag = UIManager.Instance.GetCurrentTag();
                ExistingFood food = UIManager.Instance.GetCurrentFood();
                characters[i].SetIcon(food.GetFoodType().GetIcon(tag));
                

                yield return StartCoroutine(characters[i].MoveTo(fridge.gameObject.transform.position));
                characters[i].ShowIcon(true);
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(characters[i].MoveTo(oven.gameObject.transform.position));
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(characters[i].MoveToOriginalPosition());
                FoodInventory.Instance.AddFood(tag, food);
                characters[i].ShowIcon(false);

            }
            turnPlaying = false;
        }
    }
}
