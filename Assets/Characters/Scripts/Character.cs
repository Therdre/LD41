using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNameSpace
{
    using Stats;
    public class Character : MonoBehaviour
    {

        public float speed = 0.5f;
        Stat[] availableStats = null;
        Stat skillStat = null;

        private void Awake()
        {
            availableStats = GetComponents<Stat>();
            skillStat = GetComponent<SkillStat>();
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
            //for now we are moving just -1 in x
            target = originalPosition + new Vector3(-1.0f, 0.0f, 0.0f);
            float time = 0.5f;
            float currentTime = 0.0f;
            
            while(currentTime<time)
            {
                transform.position = Vector3.Lerp(originalPosition,target,currentTime/time);
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.3f);

            currentTime = 0.0f;
            while (currentTime < time)
            {
                transform.position = Vector3.Lerp(originalPosition, target, 1.0f-currentTime / time);
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            transform.position = originalPosition;
        }

    }
}
