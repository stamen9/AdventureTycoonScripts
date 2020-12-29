using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue> :  ISerializationCallbackReceiver
{
    public List<string> _keys = new List<string> {};
    public List<Item> _values = new List<Item> {};


    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<string, Item> _myDictionary = new Dictionary<string, Item>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in _myDictionary)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _myDictionary = new Dictionary<string, Item>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            _myDictionary.Add(_keys[i], _values[i]);
    }

    void OnGUI()
    {
        foreach (var kvp in _myDictionary)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }

    public void Add(string key, Item item)
    {
        _myDictionary.Add(key, item);
    }
}

[Serializable] 
public class ItemDictionary : SerializableDictionary<string, Item> 
{
    
}
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this);
        }
    }

    [SerializeField] public ItemDictionary Database = new ItemDictionary();
    // Start is called before the first frame update
    
    public void AddItemFromUI()
    {
        Database.Add("testIndex", new Item());
       // Debug.Log(Database.Count());

        Debug.Log("adding item");
    }
    public void RemoveItemFromUI(string indexToRemove)
    {
        Debug.Log(indexToRemove);
        if(Database._myDictionary.ContainsKey(indexToRemove))
        {
            Debug.Log(Database._myDictionary.ContainsKey(indexToRemove));
            Database._myDictionary.Remove(indexToRemove);
        }
        Debug.Log("removing item");
    }

    public void DrawRewards(int rewardNum, Encounter enc)
    {
        /*Encounter enc = AllEncounters[(int)type].FindEncounterByName(encounterName);*/
        if (enc != null)
        {
            //int RandomNumberOfDraws = UnityEngine.Random.Range(3, 8);
            for (int i = 0; i < rewardNum; i++)
            {
                string draw = enc.DrawFromTable();
                Debug.Log("Random key drawen: " + draw);
                Item itemToAdd = Database._myDictionary[draw];
                if (itemToAdd != null)
                    Player.Instance.AddResources(itemToAdd, 1);
                else
                    Debug.Log("failed to find item with key : " + draw);
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
