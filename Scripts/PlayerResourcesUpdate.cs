using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerResourcesUpdate : MonoBehaviour
{
    public TMPro.TMP_Text PlayerGoldValueText;
    public TMPro.TMP_Text PlayerFameValueText;
    // Start is called before the first frame update
    void Awake()
    {
        //Not sure if this is 100% safe to do on Awake
        //Should be
        //Definetly can cause issues on Start
        Player.Instance.PostValueChangeTrigger += Instance_PostValueChangeTrigger; ;
    }

    private void Instance_PostValueChangeTrigger(object sender, System.EventArgs e)
    {
        //I'm not a fan of this parsing
        //Maybe theres is a way to tell what type of EventArgs will get passed
        UpdateValues(((Player.PlayerEventArgs)e).GoldEventArg, ((Player.PlayerEventArgs)e).FameEventArg);
    }

    private void UpdateValues(int gold , int fame)
    {

        PlayerGoldValueText.text = gold.ToString();
        PlayerFameValueText.text = fame.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
