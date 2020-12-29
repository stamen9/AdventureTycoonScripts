using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUIHelper : MonoBehaviour
{
    private static bool isUIOpen = false;

    public static GeneralUIHelper instance;

    public static bool IsUIOpen()
    {
        return isUIOpen;
    }

    public GameObject SvaeLoadButtonPrefab;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject BlacksmithPanel;

    [SerializeField]private GameObject EscPanel;

    public void OpnePanel(GameObject PanelToOpen)
    {
        DayNightCycle.Instance.PauseTime();
        isUIOpen = true;
        PanelToOpen.SetActive(true);
    }

    public void ClosePanel(GameObject PanelToClose)
    {
        PanelToClose.SetActive(false);
        isUIOpen = false;
        DayNightCycle.Instance.ResumeTime();
    }

    public static void OpnePanelStatic(GameObject PanelToOpen)
    {
        DayNightCycle.Instance.PauseTime();
        isUIOpen = true;
        PanelToOpen.SetActive(true);
    }

    public static void ClosePanelStatic(GameObject PanelToClose)
    {
        PanelToClose.SetActive(false);
        isUIOpen = false;
        DayNightCycle.Instance.ResumeTime();
    }

    public void SetUpLoadPanel(GameObject Panel)
    {
        foreach (Transform child in Panel.transform.GetChild(2))
        {
            Destroy(child.gameObject);
        }
        Panel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Load";
        FileInfo[] fileNames = SaveSystem.GetExistingSaves();
        for(int i = 0; i<5; i++)
        {
            GameObject newButton = Instantiate(SvaeLoadButtonPrefab, Panel.transform.GetChild(2));
            if(i < fileNames.Length)
            {
                newButton.GetComponentInChildren<TMPro.TMP_Text>().text = fileNames[i].Name;
                newButton.GetComponent<Button>().onClick.AddListener(delegate { SaveSystem.LoadAllData(fileNames[i].Name); });
            }
            else
            {
                newButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Empty";
            }
        }
        
    }
    public void SetUpSavePanel(GameObject Panel)
    {
        foreach (Transform child in Panel.transform.GetChild(2))
        {
            Destroy(child.gameObject);
        }
        Panel.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Save";
        FileInfo[] fileNames = SaveSystem.GetExistingSaves();
        for (int i = 0; i < 5; i++)
        {
            GameObject newButton = Instantiate(SvaeLoadButtonPrefab, Panel.transform.GetChild(2));
            if (i < fileNames.Length)
            {
                newButton.GetComponentInChildren<TMPro.TMP_Text>().text = fileNames[i].Name;
                string temp = "Save" + i + ".ats";
                newButton.GetComponent<Button>().onClick.AddListener(delegate { SaveSystem.SaveAllData(temp); });
            }
            else
            {
                newButton.GetComponentInChildren<TMPro.TMP_Text>().text = "New Save";
                string temp = "Save" + i + ".ats";
                newButton.GetComponent<Button>().onClick.AddListener(delegate { SaveSystem.SaveAllData(temp); });
            }
        }
    }
}
