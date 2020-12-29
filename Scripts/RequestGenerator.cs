using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestGenerator : MonoBehaviour
{
    
    public Request RanodmRequest(int diff)
    {
        Request NewRequest = new Request();

        NewRequest.TypeInfo = (Request.RequestType)UnityEngine.Random.Range(0, 4);

        return NewRequest;
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
