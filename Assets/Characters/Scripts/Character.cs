using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;

namespace CharacterNameSpace
{
    using Stats;
    public class Character : MonoBehaviour
    {

        public float velocity = 0.5f;
        
        public Vector3 originalPosition;
        public SpriteRenderer iconPlaceholder = null;
        public SpriteRenderer plateIconPlaceholder = null;
        public Animator animator = null;
        [Header("Stress")]
        public int characterStress = 100;

        GameUI.CharacterInfo characterDisplay = null;
        Stat[] availableStats = null;
        Stat skillStat = null;

        private void Awake()
        {
            availableStats = GetComponents<Stat>();
            skillStat = GetComponent<SkillStat>();
            originalPosition = transform.position;
        }

        public void SetCharacterDisplay(GameUI.CharacterInfo display)
        {
            characterDisplay = display;
            if (characterDisplay != null)
            {
                characterDisplay.UpdateStress(characterStress);
            }
        }

        public int Speed
        {
            get
            {
                if (skillStat != null)
                    return skillStat.value;
                return 1;
            }
        }

        public void SetStress(int stress)
        {
            characterStress = stress;
            if(characterDisplay!=null)
            {
                characterDisplay.UpdateStress(stress);
            }
        }
        public IEnumerator MoveTo(Vector3 target)
        {
            animator.SetTrigger("Walk");
            Vector3 startPosition = transform.position;
            float distance = (target- startPosition).sqrMagnitude;
            Vector3 direction = (target - startPosition).normalized;
            float time = distance / velocity;
            if (time < 0.3f)
            {
                time = 0.3f;
            }
            float currentTime = 0.0f;
            
            while(currentTime<time)
            {
                
                transform.position = Vector3.Lerp(startPosition,target,currentTime/time);
                currentTime += Time.fixedDeltaTime;
                //transform.position += direction*velocity * Time.fixedDeltaTime;                
                yield return new WaitForFixedUpdate();
            }
            transform.position = target;
            animator.SetTrigger("Idle");
        }

        //change this to rigid body and AI stuff?
        public IEnumerator MoveTo( Vector3 target, float minDistance)
        {
            Vector3 distance = target - transform.position;
            target.x = target.x - (Mathf.Sign(distance.x) * minDistance);
            yield return StartCoroutine(MoveTo(target));
        }

        public IEnumerator MoveToOriginalPosition()
        {
            yield return StartCoroutine(MoveTo(originalPosition));
        }

        public void SetIcon(Sprite icon)
        {
            if (iconPlaceholder == null)
                return;

            iconPlaceholder.sprite = icon;
        }

        public void SetPlateIcon(Sprite icon)
        {
            if (plateIconPlaceholder == null)
                return;

            plateIconPlaceholder.sprite = icon;
        }

        public void ShowIcon(bool show)
        {
            if (iconPlaceholder == null)
                return;

            iconPlaceholder.gameObject.SetActive(show);
        }

        public void ShowPlateIcon(bool show)
        {
            if (plateIconPlaceholder == null)
                return;

            plateIconPlaceholder.gameObject.SetActive(show);
        }
    }
}
