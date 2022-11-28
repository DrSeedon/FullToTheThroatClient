using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : StaticInstance<DataManager>
{
    
    [Serializable]
    public class SaveData
    {
        public string name;
        public string card;
        public string ip;
        public ushort port;
    }

    public SaveData saveData = new SaveData();

    private void Start()
    {
        saveData.ip = "127.0.0.1";
        saveData.port = 7777;
        LoadGame();
        ProfileManager.Instance.SetData();
        UIManager.Instance.ShowData();
        NetworkManager.Instance.Starting();
    }
    
    private void OnDestroy()
    {
        SaveGame();
    }
    private void OnDisable()
    {
        SaveGame();
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }


    public void SaveGame()
    {
        ResetData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.streamingAssetsPath
                                      + "/data.dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.streamingAssetsPath
                        + "/data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.streamingAssetsPath
                          + "/data.dat", FileMode.Open);
            saveData = (SaveData) bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    void ResetData()
    {
        if (File.Exists(Application.streamingAssetsPath
                        + "/data.dat"))
        {
            File.Delete(Application.streamingAssetsPath
                        + "/data.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    void EraseData()
    {
        ResetData();
    }
}
