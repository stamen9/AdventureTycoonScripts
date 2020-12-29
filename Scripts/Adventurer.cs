using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Adventurer
{
    public string Name = "Bob";

    public bool isOnMission = false;
    public bool isResting = false;
    public int RestTime = 0;
    public float Desperation = 0.1f;

    public int[] Skills = new int[4];

    public Mission MissionInProgress;

    public int EXP;
    public int Level;

    public void StartMission()
    {
        Desperation = 0;
        isOnMission = true;
        DayNightCycle.Instance.OnDayPassed += Instance_OnDayPassed;
    }

    private void Instance_OnDayPassed(object sender, System.EventArgs e)
    {
        MissionInProgress.MissionTime -= 1;
        //Debug.Log(MissionInProgress.MissionTime + " lowered by ->" + Name);
        if(MissionInProgress.MissionTime == 0)
        {
            Debug.Log(Name + " is progressing on " + MissionInProgress.MissionDetails.Target);
            NotificationData NewNotificationData = new NotificationData();
            if (RollForMissionSuccess())
            {
                Debug.Log("Mission succeded!");
                EXP += MissionInProgress.EXPReward;
                Player.Instance.Gold += MissionInProgress.GoldReward;
                CheckIfLevelUp();
                RestTime = 2;
                NewNotificationData.Text = MissionInProgress.MissionType.ToString() + " Mission has been compleated!";
                MissionInProgress.NotifySuccess(true);
                int numberOfRewards = UnityEngine.Random.Range(3, 8);
                GetLoot(numberOfRewards);
                InventoryHelper.Instance.InventoryNeedsUpdate();

            }
            else
            {
                RestTime = 4;
                NewNotificationData.Text = MissionInProgress.MissionType.ToString() + " Mission has been failed!";
            }

            if(Player.Instance.hasBlacksmith)
            {
                if(UnityEngine.Random.Range(0f,1.0f) < GameParameters.Instance.AdventurerRepairTreshold)
                {
                    RequestRepair();
                }
            }

            isResting = true;
            DayNightCycle.Instance.OnDayPassed -= Instance_OnDayPassed;
            DayNightCycle.Instance.OnDayPassed += Rest;
            
            NotificationManager.Instance.PushNotification(NewNotificationData);
        }
    }

    private void GetLoot(int rewardNum)
    {
        float mod = WorldEventManager.CheckForEvent(MissionInProgress.MissionDetails.Target);
        rewardNum = (int)Math.Round(rewardNum * mod);
        GuildHallUIHelper.Instance.PassItemDrawRequestToDatabase(rewardNum , MissionInProgress.MissionType, MissionInProgress.MissionDetails.Target);
    }

    public void RequestRepair()
    {
        BlacksmithUIHelper.Instance.PushRequest();
    }

    private bool RollForMissionSuccess()
    {
        float diff = MissionInProgress.Difficulty - Skills[(int)MissionInProgress.MissionType];
        int result = UnityEngine.Random.Range(0, 101);
        float threshhold = 100 - diff * 5;
        if(result < threshhold)
        {
            return true;
        }
        return false;
    }

    private void Rest(object sender, EventArgs e)
    {
        RestTime -= 1;
        if (RestTime <= 0)
        {
            isResting = false;
            DayNightCycle.Instance.OnDayPassed -= Rest;
        }
    }

    private void CheckIfLevelUp()
    {
        if(10 * Level <= EXP)
        {
            Level += 1;
            EXP -= 10 * Level;
            LevelUP();
        }
    }

    private void LevelUP()
    {
        //Do some stuff;
    }

    //Creates a random adventurer;
    public Adventurer()
    {
        DayNightCycle.Instance.OnDayPassed += GrowingDespair;
        //
        for(int i=0; i < 4; i++)
        {
            Skills[i] = UnityEngine.Random.Range(2, 5);
        }
    }

    public Adventurer(string name): this()
    {
        Name = name;
        Debug.Log(Name + " has been born!");
    }

    private void GrowingDespair(object sender, EventArgs e)
    {
        if(!isResting && !isOnMission)
        {
            Desperation += 0.1f;
        }
    }
}
