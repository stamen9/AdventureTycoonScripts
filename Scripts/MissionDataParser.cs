using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDataParser : MonoBehaviour
{
    [SerializeField]private TMPro.TMP_Dropdown MissionType;

    private TMPro.TMP_Dropdown TargetDropdown;
    [SerializeField]private TMPro.TMP_Dropdown PotentialCombatTarget;
    [SerializeField]private TMPro.TMP_Dropdown PotentialGatherTarget;
    [SerializeField]private TMPro.TMP_Dropdown PotentialScoutTarget;
    [SerializeField]private TMPro.TMP_Dropdown PotentialEscortTarget;
    [SerializeField] private TMPro.TMP_InputField GoldReward;

    public MissionSlot CurrentSelectedSlot;


    public void SetPotentialTarget()
    {
        TargetDropdown?.gameObject.SetActive(false);
        switch (MissionType.value)
        {
            //Combat
            case 0:
                {
                    TargetDropdown = PotentialCombatTarget;
                    break;
                }
            //Gather
            case 1:
                {
                    TargetDropdown = PotentialGatherTarget;
                    break;
                }
            //Scout
            case 2:
                {
                    TargetDropdown = PotentialScoutTarget;
                    break;
                }
            //Escort
            case 3:
                {
                    TargetDropdown = PotentialEscortTarget;
                    break;
                }
            default:
                {
                    break;
                }
        }
        TargetDropdown.gameObject.SetActive(true);
    }
    public void Start()
    {
        SetPotentialTarget();
    }
    private bool ValidateMissionData()
    {
        if(GoldReward.text == "")
        {
            return false;
        }
        if(Player.Instance.Gold < int.Parse(GoldReward.text) || int.Parse(GoldReward.text) < 0 )
        {
            return false;
        }
        return true;
    }

    public void PassMissionData()
    {
        if(ValidateMissionData() == false)
        {
            return;
        }
        Mission newMission = new Mission();
        
        switch(MissionType.value)
        {
            //Combat
            case 0:
                {
                    CombatMission newCombatMissionDetails = new CombatMission();
                    newCombatMissionDetails.Target = TargetDropdown.options[TargetDropdown.value].text;

                    newMission.MissionType = Mission.Type.Combat;
                    newMission.MissionDetails = newCombatMissionDetails;

                    break;
                }
            //Gather
            case 1:
                {
                    GatherMission newGatherMissionDetails = new GatherMission();
                    newGatherMissionDetails.Target = TargetDropdown.options[TargetDropdown.value].text;

                    newMission.MissionType = Mission.Type.Gather;
                    newMission.MissionDetails = newGatherMissionDetails;

                    break;
                }
            //Scout
            case 2:
                {
                    ScoutMission newScoutMissionDetails = new ScoutMission();
                    newScoutMissionDetails.Target = TargetDropdown.options[TargetDropdown.value].text;

                    newMission.MissionType = Mission.Type.Scout;
                    newMission.MissionDetails = newScoutMissionDetails;
                    break;
                }
            //Escort
            case 3:
                {
                    EscortMission newEscortMissionDetails = new EscortMission();
                    newEscortMissionDetails.Target = TargetDropdown.options[TargetDropdown.value].text;

                    newMission.MissionType = Mission.Type.Escort;
                    newMission.MissionDetails = newEscortMissionDetails;
                    break;
                }
            default:
                {
                    Debug.LogError("Unexpeced/Invalid switch case value!");
                    break;
                }
        }

        newMission.GoldReward = -1 * int.Parse(GoldReward.text);

        //!!!!! THIS NEEDS TO CHANGE;
        newMission.Difficulty = 2;
        //!!!!! THIS NEEDS TO CHANGE;
        newMission.MissionTime = UnityEngine.Random.Range(2, 5);
        if (CurrentSelectedSlot)
        {
            CurrentSelectedSlot.AddMission(newMission);
            GuildHallUIHelper.Instance.HideMissionCreatePanel();
        }
        else
        {
            Debug.Log("No mission slot to add mission to!");
        }
    }
}
