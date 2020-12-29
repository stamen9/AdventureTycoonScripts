using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildHall : Building
{
    [SerializeField] private Worker GuildMaster;
    [SerializeField] private Worker Trader;
    [SerializeField] private Worker FrontDesk;

    [SerializeField] private List<Adventurer> AdventurersCollection;

    [SerializeField] public List<Mission> MissionsList;

    [SerializeField] private EncountersCollection CombatEncounters;
    [SerializeField] private EncountersCollection GatherEncounters;
    [SerializeField] private EncountersCollection ScoutEncounters;
    [SerializeField] private EncountersCollection EscortEncounters;

    [SerializeField] private List<EncountersCollection> AllEncounters = new List<EncountersCollection>();

    public List<Adventurer> GetAdventurers()
    {
        return AdventurersCollection;
    }

    void OnMouseDown()
    {
        if(GeneralUIHelper.IsUIOpen())
        {
            return;
        }
        SoundManager.Instance.PlaySound("DoorSound");
        this.GetComponent<GuildHallUIHelper>().ShowPanel();
    }

    //again this is a bit of a bruteish aproach;
    private void ReadAndUpdateEncounters()
    {
        List<string> tempList = new List<string>();
        foreach(Encounter enc in CombatEncounters.Collection)
        {
            tempList.Add(enc.EncounterName);
        }
        GuildHallUIHelper.Instance.PassValuesToCombatTarget(tempList);
        tempList.Clear();

        foreach (Encounter enc in GatherEncounters.Collection)
        {
            tempList.Add(enc.EncounterName);
        }
        GuildHallUIHelper.Instance.PassValuesToGatherTarget(tempList);
        tempList.Clear();

        foreach (Encounter enc in ScoutEncounters.Collection)
        {
            tempList.Add(enc.EncounterName);
        }
        GuildHallUIHelper.Instance.PassValuesToScoutTarget(tempList);
        tempList.Clear();

        foreach (Encounter enc in EscortEncounters.Collection)
        {
            tempList.Add(enc.EncounterName);
        }
        GuildHallUIHelper.Instance.PassValuesToEscortTarget(tempList);
        tempList.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        MissionsList = new List<Mission>();
        AdventurersCollection = new List<Adventurer>();
        AllEncounters.Add(CombatEncounters);
        AllEncounters.Add(GatherEncounters);
        AllEncounters.Add(ScoutEncounters);
        AllEncounters.Add(EscortEncounters);
        
        DayNightCycle.Instance.OnDayPassed += SearchForMissionsToTake;
        ReadAndUpdateEncounters();
        for(int i = 0 ; i <3 ; i++ )
        {
            Adventurer NewAdventurer = new Adventurer("Bob" + i);
            AdventurersCollection.Add(NewAdventurer);
        }
    }

    public Encounter GetEncounter(Mission.Type type, string encounterName)
    {
        //Debug.Log(AllEncounters[(int)type]);
        return AllEncounters[(int)type].FindEncounterByName(encounterName);
    }

    private void SearchForMissionsToTake(object sender, System.EventArgs e)
    {
        if (MissionsList.Count == 0)
        {
            //Debug.Log("MissionList is empty");
            return;

        }
            
        foreach (Adventurer SearchingAdventurer in AdventurersCollection)
        {
            if (SearchingAdventurer.isOnMission || SearchingAdventurer.isResting)
            {
                //Debug.Log(SearchingAdventurer.Name + " is on mission");
                continue;
            }
            
            foreach(Mission SearchingMission in MissionsList)
            {
                if (SearchingMission.inProgres)
                {
                    continue;
                }
                float upperTreshhold = SearchingAdventurer.Skills[(int)SearchingMission.MissionType] + SearchingAdventurer.Desperation;
                float lowerTreshhold = SearchingAdventurer.Skills[(int)SearchingMission.MissionType] - SearchingAdventurer.Desperation;
                //Debug.Log(SearchingAdventurer.Name + " -> " + " lowerTreshhold: " + lowerTreshhold + "| upperTreshhold: " + upperTreshhold + "| Difficulty:" + SearchingMission.Difficulty);
                if (lowerTreshhold <= SearchingMission.Difficulty && SearchingMission.Difficulty <= upperTreshhold)
                {
                    //Debug.Log(SearchingAdventurer.Name + " has gone on " + SearchingMission.MissionDetails.Target);
                    SearchingMission.inProgres = true;
                    SearchingAdventurer.MissionInProgress = SearchingMission;
                    SearchingAdventurer.StartMission();
                    //Debug.Log(SearchingAdventurer.Name + " is on mission? " + SearchingAdventurer.isOnMission);
                    break;
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
