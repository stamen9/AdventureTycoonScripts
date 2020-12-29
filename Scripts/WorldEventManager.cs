using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    private int EventIntensity = 5;

    private int EventTreshhold = 100;
    private int EventCurrentTreshhold = 100;
    
    // Start is called before the first frame update
    public class WorldEvent
    {
        //varies from 0 to infinity I guess;
        public float EventMod = 1;
        //string to match
        public string EventTarget = "";

        public int DaysLeft = 0;

        public WorldEvent(float _mod, string _target, int _days)
        {
            EventMod = _mod;
            EventTarget = _target;
            DaysLeft = _days;
        }
    }

    public void EndEvent(WorldEvent EventToRemove)
    {
        OngoingEvents.Remove(EventToRemove);
    }

    public PossibleWorldEventTargets EventTargets;

    private static List<WorldEvent> OngoingEvents = new List<WorldEvent>();

    void Start()
    {
        //is it ok to do this on start
        //Don't think it misses 1st
        //But could it cause issues on Awake?
        DayNightCycle.Instance.OnDayPassed += Instance_OnDayPassed;

        EventCurrentTreshhold = EventTreshhold;
    }
    public static float CheckForEvent(string check)
    {
        foreach(WorldEvent evnt in OngoingEvents)
        {
            if(evnt.EventTarget == check)
            {
                return evnt.EventMod;
            }
        }
        return 1;
    }
    private void Instance_OnDayPassed(object sender, System.EventArgs e)
    {
        List<WorldEvent> EventsToRemove = new List<WorldEvent>();
        foreach(WorldEvent Event in OngoingEvents)
        {
            //Logic needs to be consitent with Notification.cs   
            if(Event.DaysLeft == 0)
            {
                //Modifing collection while it is enumerated is not ok!
                EventsToRemove.Add(Event);
            }
            else
            {
                Event.DaysLeft--;
            }
        }
        foreach(WorldEvent Event in EventsToRemove)
        {
            EndEvent(Event);
        }
        //imposible to "Hit" on 1st turn
        if(UnityEngine.Random.Range(0,100) > EventCurrentTreshhold)
        {
            if (OngoingEvents.Count <= 7)
            {
                GenerateRandomWorldEvent();
            }
            
            EventCurrentTreshhold = EventTreshhold;
        }
        else
        {
            EventCurrentTreshhold -= EventIntensity;
        }
    }

    private void GenerateRandomWorldEvent()
    {
        int duration = UnityEngine.Random.Range(3, 7);
        int rngIndex = UnityEngine.Random.Range(0, EventTargets.TargetStrings.Count);
        string target = EventTargets.TargetStrings[rngIndex];
        bool InvalidTarget = true;
        //this is such a brute force method
        //but whatever
        while(InvalidTarget)
        { 
            foreach(WorldEvent Event in OngoingEvents)
            { 
                if(target == Event.EventTarget)
                {
                    rngIndex = UnityEngine.Random.Range(0, EventTargets.TargetStrings.Count);
                    target = EventTargets.TargetStrings[rngIndex];
                    break;
                }
            }
            InvalidTarget = false;
        }
        //Need to check if there is already a event with same target ongoing.
        float magnitude = 0.25f * UnityEngine.Random.Range(1, 3);
        float severity = 1;
        if(UnityEngine.Random.Range(0, 2) == 0)
        {
            severity += -1 * magnitude;
        }
        else
        {
            severity += magnitude;
        }

        WorldEvent NewWorldEvent = new WorldEvent(severity,target,duration);

        OngoingEvents.Add(NewWorldEvent);

        NotificationData newNotification = new NotificationData();

        newNotification.Text = target + " * " + severity.ToString();
        //overlap of data?
        newNotification.duration = duration;

        NotificationManager.Instance.PushNotification(newNotification);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
