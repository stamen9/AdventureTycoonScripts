using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildHallUIHelper : MonoBehaviour
{

    public static GuildHallUIHelper Instance;

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

    [SerializeField] private GameObject MissionPanel;
    [SerializeField] private GameObject WorkerPanel;

    [SerializeField] private GameObject GuildHallUIPanel;
    [SerializeField] private GameObject MissionCreatePanel;
    [SerializeField] private GameObject MissionViewPanel;

    public static GameObject MissionCreateButton;

    [SerializeField] private GameObject MissionCreateButtonHelper;

    public static GameObject MissionViewButton;

    [SerializeField] private GameObject MissionViewButtonHelper;

    [SerializeField] private GameObject TechTreeHolder;

    //such a brutish way to populate the UI
    public void PassValuesToCombatTarget(List<string> encCollection)
    {
        GameObject Options = MissionCreatePanel.transform.Find("BackgroundHide/MissionViewBackground/TextHolder/TargetHolder/CombatTargetDropdown").gameObject;
        Options.GetComponent<TMPro.TMP_Dropdown>().AddOptions(encCollection);
    }

    public GuildHall GetGuildHall()
    {
        return GetComponent<GuildHall>();
    }

    public List<Adventurer> GetAdventurers()
    {
        return GetGuildHall().GetAdventurers();
    }

    public void PassValuesToGatherTarget(List<string> encCollection)
    {
        GameObject Options = MissionCreatePanel.transform.Find("BackgroundHide/MissionViewBackground/TextHolder/TargetHolder/GatherTargetDropdown").gameObject;
        Options.GetComponent<TMPro.TMP_Dropdown>().AddOptions(encCollection);
    }
    public void PassValuesToScoutTarget(List<string> encCollection)
    {
        GameObject Options = MissionCreatePanel.transform.Find("BackgroundHide/MissionViewBackground/TextHolder/TargetHolder/ScoutTargetDropdown").gameObject;
        Options.GetComponent<TMPro.TMP_Dropdown>().AddOptions(encCollection);
    }
    public void PassValuesToEscortTarget(List<string> encCollection)
    {
        GameObject Options = MissionCreatePanel.transform.Find("BackgroundHide/MissionViewBackground/TextHolder/TargetHolder/EscortTargetDropdown").gameObject;
        Options.GetComponent<TMPro.TMP_Dropdown>().AddOptions(encCollection);
    }

    public void ShowPanel()
    {
        GeneralUIHelper.OpnePanelStatic(GuildHallUIPanel);
        /*DayNightCycle.Instance.PauseTime();
        GuildHallUIPanel.SetActive(true);*/
    }

    public void HidePanel()
    {
        GeneralUIHelper.ClosePanelStatic(GuildHallUIPanel);
        /*GuildHallUIPanel.SetActive(false);
        DayNightCycle.Instance.ResumeTime();*/
    }

    public void ShowMissionCreatePanel(MissionSlot Caller)
    {
        MissionCreatePanel.SetActive(true);

        MissionCreatePanel.GetComponent<MissionDataParser>().CurrentSelectedSlot = Caller;

        /*GameObject Button = GameObject.Find("CreateMissionButton");
        Button.GetComponent<Button>().onClick.RemoveAllListeners();
        Button.GetComponent<Button>().onClick.AddListener(Caller.CreateRandomMission);*/
    }
    public void HideMissionCreatePanel()
    {
        MissionCreatePanel.SetActive(false);
        MissionCreatePanel.GetComponent<MissionDataParser>().CurrentSelectedSlot = null;
    }

    public void ShowMissionViewPanel(MissionSlot Caller)
    {
        MissionViewPanel.SetActive(true);
    }
    public void HideMissionViewPanel()
    {
        MissionViewPanel.SetActive(false);
    }

    public void LockButton(Button Caller)
    {
        Caller.enabled = false;
        ChangeButtonImage(Caller);
    }

    public void OpenWorkerView()
    {
        WorkerPanel.SetActive(true);
        MissionPanel.SetActive(false);
    }

    public void OpenMissionView()
    {
        MissionPanel.SetActive(true);
        WorkerPanel.SetActive(false);
    }

    public void OpenGuildHallTeckTree()
    {
        TechTreeHolder.SetActive(true);
    }
    public void CloseGuildHallTeckTree()
    {
        TechTreeHolder.SetActive(false);
    }
    private void ChangeButtonImage(Button Caller)
    {

    }
    
    public void PushMissionToGuildHall(Mission MissionToPush)
    {
        this.GetComponent<GuildHall>().MissionsList.Add(MissionToPush);
    }

    // Start is called before the first frame update
    void Start()
    {
        MissionCreateButton = MissionCreateButtonHelper;
        MissionViewButton = MissionViewButtonHelper;
    }

    public void PassItemDrawRequestToDatabase(int rewardNum, Mission.Type type, string encounterName)
    {
        Encounter enc = this.GetComponent<GuildHall>().GetEncounter(type, encounterName);
        if(enc == null)
        {
            Debug.Log("did not find encounter with name: " + encounterName);
            return;
        }
        ItemDatabase.Instance.DrawRewards(rewardNum, enc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
