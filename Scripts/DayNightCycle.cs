using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update

    public static DayNightCycle Instance;

    [SerializeField] private int DaysPassed = 0;
    [SerializeField] private float HoursPassed = 0;

    [SerializeField] private TMPro.TMP_Text CurrentDate;
    [SerializeField] private TMPro.TMP_Text CurrentTime;

    [SerializeField] private TMPro.TMP_Text TimeSpeed;

    [SerializeField] private float TimeMultiplier = 1;
 

    public event EventHandler OnDayPassed;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void SetTimeMultiplier(float _newTimeMulti)
    {
        TimeMultiplier = _newTimeMulti;
    }
    public void SlowDownTime()
    {
        if(TimeMultiplier < 0.26)
        {
            return;
        }
        TimeMultiplier /= 2;
        TimeSpeed.text = "x" + TimeMultiplier.ToString();
    }
    public void SpeedUpTime()
    {
        if (TimeMultiplier > 3.99)
        {
            return;
        }
        TimeMultiplier *= 2;
        TimeSpeed.text = "x" + TimeMultiplier.ToString();
    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        HoursPassed += Time.deltaTime * 0.5f * TimeMultiplier;
        if(HoursPassed >= 24)
        {
            DaysPassed++;
            CurrentDate.text = "Day: " + DaysPassed.ToString();
            HoursPassed = 0;
            OnDayPassed?.Invoke(this, EventArgs.Empty);
        }
        string TimeInText;
        int TimeInRoundedHours = (int)HoursPassed;
        TimeInText = TimeInRoundedHours.ToString() + ":00";
        /*float TimeInRoundedMins = HoursPassed - TimeInRoundedHours;
        TimeInRoundedMins *= 60;
        if (TimeInRoundedMins >= 10)
        {
            TimeInText += ((int)TimeInRoundedMins).ToString();
        }
        else
        {
            TimeInText += "0" + ((int)TimeInRoundedMins).ToString();
        }*/
        CurrentTime.text = TimeInText;
    }
}
