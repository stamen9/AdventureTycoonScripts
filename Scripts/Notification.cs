using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class Notification : MonoBehaviour
{
    // Start is called before the first frame update

    public NotificationData Data;

    public TMP_Text TextHolder;
    public Image ImageHolder;

    public int LeftDuration = 0;

    private void UpdateData()
    {

        TextHolder.text = Data.Text;
        ImageHolder.sprite = Data.img;
        LeftDuration = Data.duration;
        if (LeftDuration > 0)
        {
            SubscribeToDayNightCycle();
        }
    }

    private void SubscribeToDayNightCycle()
    {
        DayNightCycle.Instance.OnDayPassed += Instance_OnDayPassed;
    }

    private void Instance_OnDayPassed(object sender, EventArgs e)
    {

        //Logic needs to be consitent with WorldEventManager.cs
        if (LeftDuration == 0)
        {
            //Will this cause a null exeption in the NotificationManager?
            //Also I should maybe pool this.
            //reserves a bit of mem
            //saves on prcessing
            NotificationManager.Instance.ResolveNotification(this.gameObject);
            Destroy(this);
            //Someting strange is happening here with the GC!
            //This is likly not the proper way to handle it;
            LeftDuration--;
        }
        else
        {
            LeftDuration--;
        }
    }

    void Start()
    {
        //Maybe too early?
        UpdateData();
    }
}

public struct NotificationData 
{
    // Start is called before the first frame update

    public string Text;
    public Sprite img;
    //overlap of data?
    public int duration;
}