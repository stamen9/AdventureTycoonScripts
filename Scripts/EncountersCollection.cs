using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter Collection", menuName = "Scriptable Objects/New Encounter Collection", order = 1)]
public class EncountersCollection : ScriptableObject
{
    public List<Encounter> Collection;

    public Encounter FindEncounterByName(string name)
    {
        //Debug.Log("Searching for: " + name);
        //Debug.Log("Collection size: " + Collection.Count);
        foreach(Encounter enc in Collection)
        {
            
            if(enc.EncounterName == name)
            {
                return enc;
            }
            //Debug.Log(name + " != " + enc.name);
        }
        return null;
    }
    //I may need some function to return all the target names in each Encounter object
}
