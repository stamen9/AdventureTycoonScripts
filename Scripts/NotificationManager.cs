using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public List<GameObject> NotificationList;

    public GameObject NotificationBoard;

    public GameObject NotificationPrefab;

    public static NotificationManager Instance;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PushNotification(NotificationData NewNotificationData)
    {
        GameObject NewNotificationInstance = Instantiate(NotificationPrefab, NotificationBoard.transform);
        //uhhh isn't it possible that this happens after the Start call of Notification?
        //hmmm this should break, but it doesnt?
        NewNotificationInstance.GetComponent<Notification>().Data = NewNotificationData;
        NotificationList.Add(NewNotificationInstance);
    }

    //Should be changed to a pooling system? e.g. -> SetActive(False);
    //Destuction/creation of GO can be expensive.
    //But since it happens at a rate of 1GO per 30 secs it should be fine right?
    public void ResolveNotification(GameObject NotificationToResolve)
    {
        NotificationList.Remove(NotificationToResolve);
        Destroy(NotificationToResolve);
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
