using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text leaderboardText;

    private float currentTime = 0f;
    private bool isTiming = false;

    private List<float> times = new List<float>();

    void Update()
    {
        if (isTiming)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartTimer()
    {
        if (isTiming)
        {
            Debug.Log("Tried to start, but already running");
            return;
        }

        Debug.Log("Timer Started");

        currentTime = 0f;
        isTiming = true;
    }

    public void StopTimer()
    {
        if (!isTiming)
        {
            Debug.Log("Tried to stop, but timer not running");
            return;
        }

        Debug.Log("Timer Stopped");

        isTiming = false;

        times.Add(currentTime);
        times.Sort();

        UpdateLeaderboardUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = FormatTime(currentTime);
    }

    void UpdateLeaderboardUI()
    {
        leaderboardText.text = "<b>Leaderboard:</b>\n";

        for (int i = 0; i < times.Count; i++)
        {
            if (i == 0)
            {
                leaderboardText.text += $"<color=yellow><b>{i + 1}. {FormatTime(times[i])}</b></color>\n";
            }
            else
            {
                leaderboardText.text += $"{i + 1}. {FormatTime(times[i])}\n";
            }
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);

        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
