using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class AllObjectData
{
    public PlayerData playerData;
    public GuildHallData guildHallData;
    public BlacksmithData blacksmithData;

    public AllObjectData(Player player, GuildHall guildHall, Blacksmith blacksmith = null)
    {
        playerData = new PlayerData(player);
        guildHallData = new GuildHallData(guildHall);
        if(blacksmith)
        {
            blacksmithData = new BlacksmithData(blacksmith);
        }
    }
}

public static class SaveSystem 
{
    public static FileInfo[] GetExistingSaves()
    {
        //string[] saveFiles = new string[5] { "Empty", "Empty", "Empty", "Empty", "Empty"};
        //int i = 0;
        string path = Application.persistentDataPath;
        DirectoryInfo di = new DirectoryInfo(@path);
        FileInfo[] files = di.GetFiles("*.ats");
        return files;
    }
    public static void SaveAllData(string fileName = null)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + (fileName != null ? fileName : "Game.ats");
        Debug.Log("Game file saved -> " + path);
        FileStream writeStream = new FileStream(path, FileMode.Create);

        Blacksmith blacksmith = null;
        if(Player.Instance.hasBlacksmith)
        {
            blacksmith = BlacksmithUIHelper.Instance.GetBlacksmith();
        }

        AllObjectData data = new AllObjectData(Player.Instance,GuildHallUIHelper.Instance.GetGuildHall(), blacksmith);

        formatter.Serialize(writeStream, data);
        writeStream.Close();
    }

    public static AllObjectData LoadAllData(string fileName = null)
    {
        string path = Application.persistentDataPath + "/" + (fileName!=null ? fileName : "Game.ats");
        Debug.Log("Game file loaded <- " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream readStream = new FileStream(path, FileMode.Open);

            AllObjectData data = formatter.Deserialize(readStream) as AllObjectData;
            readStream.Close();

            return data;
        } else
        {
            Debug.LogError("Could not find save file");
            return null;
        }


    }

    #region Load methods

    #endregion
}
