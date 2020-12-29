using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "Scriptable Objects/New Encounter", order = 1)]
public class Encounter : ScriptableObject
{
    public string EncounterName;
    public DropTable Table;

    public string DrawFromTable()
    {
        return Table.DrawFromTable();
    }

    public int difficulty;
}
