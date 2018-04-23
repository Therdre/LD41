using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUI;

public class Timer : MonoBehaviour
{
    public float time = 60.0f;
    bool timerRunning = false;

    Slider timeDisplay = null;
    Text timeText = null;
    private void Start()
    {
        timeDisplay = UIManager.Instance.timeDisplay;
        timeDisplay.maxValue = time;
        timeText = timeDisplay.gameObject.GetComponentInChildren<Text>();
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        timerRunning = true;
        float currentTime = time;
        while(currentTime >= 0.0f)
        {
            UpdateDisplay(currentTime);
            currentTime -= Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
        UpdateDisplay(currentTime);
        currentTime = 0.0f;
        timerRunning = false;
    }

    void UpdateDisplay(float timeLeft)
    {
        timeDisplay.value = timeLeft;

        if (timeText != null)
        {
            int totalSeconds = (int)timeLeft;
            int seconds = totalSeconds % 60;
            int minutes = totalSeconds / 60;
            timeText.text = minutes + ":" + seconds;
        }
    }

    public bool IsTimerRunning()
    {
        return timerRunning;
    }
}
