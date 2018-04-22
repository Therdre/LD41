using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

                //2)Perform character action
                yield return StartCoroutine(characters[i].MoveTo(fridge.gameObject.transform.position,0.18f));
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(characters[i].MoveTo(oven.gameObject.transform.position,0.18f));
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(characters[i].MoveToOriginalPosition());
            }
            turnPlaying = false;
        }
    }
}
