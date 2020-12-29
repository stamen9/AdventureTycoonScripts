using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//make it scriptable object
//move the quantety

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/New Item", order = 1)]
public class Item: ScriptableObject
{
    public string Name;
    public int Value;
    //public int Quantety;
    public string Description;
    public Sprite Icon;

    public string key;
}

[System.Serializable]
public class DictionaryItem
{
    public int id;
    public string Name;
    public int Value;
    //public int Quantety;
    public string Description;
    public Sprite Icon;
}