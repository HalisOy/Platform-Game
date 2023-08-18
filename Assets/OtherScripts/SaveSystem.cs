using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SaveSystem
{
    public static void SaveGame(GameData Data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = $"{Application.persistentDataPath}/GameData.HTOStudio";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, Data);
        stream.Close();
    }
    public static GameData LoadGame()
    {
        string path = $"{Application.persistentDataPath}/GameData.HTOStudio";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }
    public static bool SaveExists()
    {
        string path = $"{Application.persistentDataPath}/GameData.HTOStudio";
        if (File.Exists(path))
            return true;
        else
            return false;
    }
}
