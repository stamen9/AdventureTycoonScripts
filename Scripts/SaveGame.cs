using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DataAccessLayers;
using UnityEngine.GameFoundation.DataPersistence;

public class SaveGame : MonoBehaviour
{
    PersistenceDataLayer dataLayer;

    void Awake()
    {
        // choose what format you want to use
        JsonDataSerializer dataSerializer = new JsonDataSerializer();

        string path = Application.persistentDataPath + "/Game.ats";
        // choose where to store the data
        IDataPersistence localPersistence = new LocalPersistence("GFTutorial", dataSerializer);

        // create a data access layer for Game Foundation and keep a reference to it
        // to save your progression whenever you want
        dataLayer = new PersistenceDataLayer(localPersistence);

        // tell Game Foundation to initialize using the created data access layer
        GameFoundation.Initialize(dataLayer);
    }

    public void Save()
    {
        // save Game Foundation's data
        dataLayer.Save();
    }

    public void Load()
    {
        // to load a fresh set of data for Game Foundation you need to: unitialize it ...
        GameFoundation.Uninitialize();

        // ... and re-initialize it with the desired data access layer
        GameFoundation.Initialize(dataLayer);
    }
}