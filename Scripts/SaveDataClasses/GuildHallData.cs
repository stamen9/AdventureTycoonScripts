using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildHallData 
{
    public Adventurer[] adventurers;
    public Mission[] missions;

    public GuildHallData(GuildHall guildhall)
    {
        //foreach(Adventurer a in guildhall.)
        missions = new Mission[guildhall.MissionsList.Count];
        for(int i = 0; i < guildhall.MissionsList.Count; i++)
        {
            //missions[i] = guildhall.MissionsList[i];
        }
        List<Adventurer> advCol = guildhall.GetAdventurers();
        adventurers = new Adventurer[advCol.Count];
        for (int i = 0; i < advCol.Count; i++)
        {
            adventurers[i] = advCol[i];
        }
    }
}


