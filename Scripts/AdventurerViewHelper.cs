using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventurerViewHelper : MonoBehaviour
{
    [SerializeField] private GameObject ViewPanel;
    [SerializeField] private GuildHall guildHall;

    private List<GameObject> ButtonCollection = new List<GameObject>();
    [SerializeField] private GameObject ButtonHolder;
    [SerializeField] private GameObject ButtonPrefab;

    [SerializeField] private GameObject AdventurerDetailsViewPanel;
    [SerializeField] private TMPro.TMP_Text NameHoledr;
    [SerializeField] private TMPro.TMP_Text[] StatHolders = new TMPro.TMP_Text[4]; 

    public void ShowViewPanel(bool s)
    {
        ViewPanel.SetActive(s);
        if(s)
        {
            SetUpAdveturerButtons();
        }
    }

    private void SetUpAdveturerButtons()
    {
        int i = 0;
        foreach(Adventurer adv in guildHall.GetAdventurers())
        {
            if (ButtonCollection.Count == i )
            {
                GameObject NewButton = Instantiate(ButtonPrefab, ButtonHolder.transform);
                ButtonCollection.Add(NewButton);
                Button bScript = NewButton.GetComponent<Button>();
                if(bScript)
                {
                    bScript.onClick.AddListener(delegate { ShowAdveturerDetails(adv); });
                }
                else
                {
                    Debug.LogError("Button prefab is missing Button script");
                }
            }
            else
            {
                Button bScript = ButtonCollection[i].GetComponent<Button>();
                if (bScript)
                {
                    bScript.onClick.RemoveAllListeners();
                    bScript.onClick.AddListener(delegate { ShowAdveturerDetails(adv); });
                }
                else
                {
                    Debug.LogError("Button prefab is missing Button script");
                }
            }
            i++;
        }
        while(i < ButtonCollection.Count)
        {
            ButtonCollection[i++].SetActive(false);
        }
    }
    public void ShowAdveturerDetails(Adventurer adv)
    {
        AdventurerDetailsViewPanel.SetActive(true);
        NameHoledr.text = adv.Name;
        for(int i = 0 ; i < 4 ; i++)
        {
            StatHolders[i].text = adv.Skills[i].ToString();
        }
    }
    public void HideAdveturerDetails()
    {
        AdventurerDetailsViewPanel.SetActive(false);
    }
}
