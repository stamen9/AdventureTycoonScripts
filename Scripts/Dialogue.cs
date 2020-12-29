using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/New Dialogue", order = 1)]
public class Dialogue: ScriptableObject
{
    //Do i need more stuff here?
    //Considering the scope/goal i guess no?
    public string Talker;
    [TextArea(1, 5)]
    public string[] Lines;
}