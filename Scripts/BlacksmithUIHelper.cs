using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithUIHelper : MonoBehaviour
{
    public static BlacksmithUIHelper Instance;

    private Blacksmith BlacksmithComponent;

    public Blacksmith GetBlacksmith()
    {
        return BlacksmithComponent;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Resources.FindObjectsOfTypeAll<GameObject>();
            BlacksmithPanel = GeneralUIHelper.instance.BlacksmithPanel;
            BlacksmithComponent = this.GetComponent<Blacksmith>();
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] private GameObject BlacksmithPanel;
    [SerializeField] private GameObject RequestInfoPanel;

    public void ShowPanel()
    {
        DayNightCycle.Instance.PauseTime();
        BlacksmithPanel.SetActive(true);
    }

    public void HidePanel()
    {
        BlacksmithPanel.SetActive(false);
        DayNightCycle.Instance.ResumeTime();
    }

    public void AcceptRequest(Request RequestToAccept)
    {
        RequestToAccept.AcceptedStatus = true;
        RequestInfoPanel.SetActive(false);
        if (Player.Instance.CheckForResources(RequestToAccept.Data.Requirements))
        {
            Player.Instance.TakeResources(RequestToAccept.Data.Requirements);
            
        }
    }

    public void DeclienRequest(Request RequestToDecline)
    {
        BlacksmithComponent.PopRequest(RequestToDecline);
        RequestInfoPanel.SetActive(false);
    }

    public void PushRequest()
    {
        List<Player.InventorySlot> requestedItems = new List<Player.InventorySlot>();
        Item ore = BlacksmithComponent.GetRandomBasicOre();
        if(ore == null)
        {
            Debug.LogWarning("Failed to find ore for new request");
            return;
        }
        int randomAmoutOfOre = UnityEngine.Random.Range(2, 5);
        Player.InventorySlot randomOre = new Player.InventorySlot(ore, randomAmoutOfOre);
        requestedItems.Add(randomOre);
        RequestTypeData data = new RequestTypeData(requestedItems, 10);
        Request newRepairRequest = new Request(Request.RequestType.Repair, data);

        BlacksmithComponent.PushRequest(newRepairRequest);
        //RequestInfoPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
