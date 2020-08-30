using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(PlayerController playerController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveFile.sms";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSaveData data = new PlayerSaveData(playerController);
        Debug.Log("Save File");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerSaveData LoadData()
    {
        Debug.Log("Load");
        string path = Application.persistentDataPath + "/SaveFile.sms";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
            Debug.Log("Load: " + data.position[0] + " " + data.position[1] + " " + data.position[2]);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

}
