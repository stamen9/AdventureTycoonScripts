using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlacksmithData 
{
    public Request[] request;

    public BlacksmithData(Blacksmith blacksmith)
    {
        request = new Request[blacksmith.RequestList.Count];
        for(int i = 0; i < blacksmith.RequestList.Count; i++)
        {
            request[i] = blacksmith.RequestList[i];
        }
    }

}
