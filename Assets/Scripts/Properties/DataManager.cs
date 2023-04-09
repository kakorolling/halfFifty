using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static void Save(SavedData saveData, string saveFileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/" + saveFileName + ".sav";
        FileStream fileStream = new FileStream(savePath, FileMode.Create);

        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public static SavedData Load(string saveFileName)
    {
        string savePath = Application.persistentDataPath + "/" + saveFileName + ".sav";
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(savePath, FileMode.Open);

            SavedData saveData = formatter.Deserialize(fileStream) as SavedData;
            fileStream.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("Save file not found in " + savePath);
            return null;
        }
    }

    public static bool Delete(string saveFileName)
    {
        string savePath = Application.persistentDataPath + "/" + saveFileName + ".sav";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            return true;
        }
        else
        {
            Debug.LogError("Save file not found in " + savePath);
            return false;
        }
    }
}
