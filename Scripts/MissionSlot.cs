using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;
using UnityEngine.UI;

public class MissionSlot : MonoBehaviour
    , IPointerEnterHandler
{
    //needs to be private if i wish to have it as a serailizable(for the sake of saving/loading) as well as having it = null at start;
    //alternative is NonSerialized but OOP states it should be private anyway(rest should be private as well tbh)
    private Mission SlottedMission = null;
    public TMP_Text MissionTypeText;
    public TMP_Text MissionDifficultyText;
    public TMP_Text MissionTarget;
    public TMP_Text AdventurerRewards;
    public TMP_Text GuildRewards;

    bool HideHelper = false;
    public void ShowMissionData()
    {

        MissionTypeText.text = SlottedMission.MissionType.ToString();
        MissionDifficultyText.text = SlottedMission.Difficulty.ToString();
        AdventurerRewards.text = "";
        foreach (Item item in SlottedMission.AdventurerRewards)
        {
            //AdventurerRewards.text += item.Quantety.ToString() + "x " + item.Name + "\n";
            AdventurerRewards.text = "FIX ITEMS YOU LAZY ASS";
        }
        GuildRewards.text = "";
        foreach (Item item in SlottedMission.GuildRewards)
        {
            //GuildRewards.text += item.Quantety.ToString() + "x " + item.Name + "\n";
            GuildRewards.text = "FIX ITEMS YOU LAZY ASS";
        }
        switch (SlottedMission.MissionDetails.GetMissionType())
        {
            case 1:
                {
                    MissionTarget.text = "Kill " + ((CombatMission)SlottedMission.MissionDetails).Count + " " + ((CombatMission)SlottedMission.MissionDetails).Target;
                    break;
                }
            case 2:
                {
                    MissionTarget.text = "Gather " + ((GatherMission)SlottedMission.MissionDetails).Count + " " + ((GatherMission)SlottedMission.MissionDetails).Target;
                    break;
                }
            case 3:
                {
                    MissionTarget.text = "Scout " + ((ScoutMission)SlottedMission.MissionDetails).Target;
                    break;
                }
            case 4:
                {
                    MissionTarget.text = "Escort " + ((EscortMission)SlottedMission.MissionDetails).Target;
                    break;
                }
            default:
                {
                    Debug.Log("Defaoult mission!(should not hit this code path)");
                    break;
                }
        }
        
    }


    public void ShowMissionIcon()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideMissionIcon()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void RemoveMission()
    {

    }

    public void ResolveMisson(bool result)
    {
        Debug.Log("Resolving Mission");
        if (result)
        {
            HideMissionIcon();
            SlottedMission = null;
        }
    }

    public void AddMission(Mission MissionToAdd)
    {
        Debug.Log("added mission!");
        SlottedMission = MissionToAdd;
        MissionToAdd.OnComplete = ResolveMisson;
        GuildHallUIHelper.Instance.PushMissionToGuildHall(MissionToAdd);
        ShowMissionIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SlottedMission == null)
        { 
            GuildHallUIHelper.MissionCreateButton.transform.localPosition = transform.localPosition;
            GuildHallUIHelper.MissionCreateButton.SetActive(true);
            GuildHallUIHelper.MissionCreateButton.GetComponent<Button>().onClick.RemoveAllListeners();
            GuildHallUIHelper.MissionCreateButton.GetComponent<Button>().onClick.AddListener(ShowMissionCreatePanelHelper);
            //Debug.Log("OnPointerEnter SlottedMission set;");
        }
        else
        {
            GuildHallUIHelper.MissionViewButton.transform.localPosition = transform.localPosition;
            GuildHallUIHelper.MissionViewButton.SetActive(true);
            GuildHallUIHelper.MissionViewButton.GetComponent<Button>().onClick.RemoveAllListeners();
            GuildHallUIHelper.MissionViewButton.GetComponent<Button>().onClick.AddListener(ShowMissionViewPanelHelper);
        }
        
    }

    public void ShowMissionCreatePanelHelper()
    {
        GuildHallUIHelper.Instance.ShowMissionCreatePanel(this);
    }

    public void ShowMissionViewPanelHelper()
    {
        GuildHallUIHelper.Instance.ShowMissionViewPanel(this);
    }

    //Should not be used in it's current state
    /*public void CreateRandomMission()
    {

        Mission NewMission = new Mission();
        NewMission.MissionTime = UnityEngine.Random.Range(0, 11);
        NewMission.Difficulty = 1;
        NewMission.MissionType = (Mission.Type)UnityEngine.Random.Range(0, 4);

        SlottedMission = NewMission;
        switch (NewMission.MissionType)
        {
            case Mission.Type.Combat:
                {
                    CombatMission Detail = new CombatMission();
                    Detail.Count = UnityEngine.Random.Range(5, 16);
                    Detail.Target = "Goblins";
                    SlottedMission.MissionDetails = Detail;
                    break;
                }
            case Mission.Type.Escort:
                {
                    EscortMission Detail = new EscortMission();
                    Detail.Target = "Royal Carpenter";
                    SlottedMission.MissionDetails = Detail;
                    break;
                }
            case Mission.Type.Gather:
                {
                    GatherMission Detail = new GatherMission();
                    Detail.Count = UnityEngine.Random.Range(3, 7);
                    Detail.Target = "Exotic Dandelion";
                    SlottedMission.MissionDetails = Detail;
                    break;
                }
            case Mission.Type.Scout:
                {
                    ScoutMission Detail = new ScoutMission();
                    Detail.Target = "Goblins";
                    SlottedMission.MissionDetails = Detail;
                    break;
                }

        }

        ShowMissionIcon();
        GuildHallUIHelper.Instance.HideMissionCreatePanel();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        if(SlottedMission != null)
        {
            //this is an advance for when saving/loading
            AddMission(SlottedMission);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
