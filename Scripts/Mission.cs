using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Making it Systme.Serializable kind of makes it unnullable as long as it is exposed to the Unity inspector

[System.Serializable]
//Maybe should be struct
public class Mission
{
    //Bad naming!!!
    public enum Type
    {
        Combat,
        Gather,
        Scout,
        Escort
    }

    public int MissionTime;
    public Type MissionType;
    public TypeData MissionDetails;

    public int GoldReward;
    public int EXPReward;

    public List<Item> AdventurerRewards;
    public List<Item> GuildRewards;

    public int Difficulty;

    public bool inProgres = false;

    public delegate void OnCompleteDel(bool result);

    public OnCompleteDel OnComplete;

    public void NotifySuccess(bool result)
    {
        OnComplete?.Invoke(result);
    }
}

[System.Serializable]
public class TypeData
{
    public string Target;
    public virtual int GetMissionType()
    {
        return 0;
    }
}

[System.Serializable]
public class CombatMission : TypeData
{

    public int Count;

    public override int GetMissionType()
    {
        return 1;
    }
}
[System.Serializable]
public class GatherMission : TypeData
{
    public int Count;

    public override int GetMissionType()
    {
        return 2;
    }
}
[System.Serializable]
public class ScoutMission : TypeData
{
    public override int GetMissionType()
    {
        return 3;
    }
}
[System.Serializable]
public class EscortMission : TypeData
{
    public override int GetMissionType()
    {
        return 4;
    }
}