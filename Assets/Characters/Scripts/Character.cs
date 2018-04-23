using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNameSpace
{
    using Stats;
    public class Character : MonoBehaviour
    {

        public float velocity = 0.5f;
        public Vector3 originalPosition;
        public SpriteRenderer iconPlaceholder = null;
        Stat[] availableStats = null;
        Stat skillStat = null;

        private void Awake()
        {
            availableStats = GetComponents<Stat>();
            skillStat = GetComponent<SkillStat>();
            originalPosition = transform.position;
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

        public IEnumerator MoveTo(Vector3 target)
        {
            Vector3 originalPosition = transform.position;
            float distance = (target-originalPosition).sqrMagnitude;
            float time = distance/velocity;
            float currentTime = 0.0f;
            
            while(currentTime<time)
            {
                transform.position = Vector3.Lerp(originalPosition,target,currentTime/time);
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            transform.position = target;
            /*yield return new WaitForSeconds(0.3f);

            currentTime = 0.0f;
            while (currentTime < time)
            {
                transform.position = Vector3.Lerp(originalPosition, target, 1.0f-currentTime / time);
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }*/

            //transform.position = originalPosition;
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

        public void SetIcon(Sprite icon, Color color)
        {
            if (iconPlaceholder == null)
                return;

            iconPlaceholder.sprite = icon;
            
        }

        public void ShowIcon(bool show)
        {
            if (iconPlaceholder == null)
                return;

            iconPlaceholder.gameObject.SetActive(show);
        }
    }
}
