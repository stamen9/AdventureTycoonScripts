using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventStringCollection", menuName = "Scriptable Objects/World Event Strings", order = 1)]
public class PossibleWorldEventTargets : ScriptableObject
{
    public List<string> TargetStrings;
}
/*
public enum WorldEventEnums
{
    Ore,
    Goblins,
    RagingWolfs,
    Food,
    Medecine,
    Wood,
    Herb,
    Nobles
}
*/