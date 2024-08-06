using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameData : MonoBehaviour
{
    public static SaveGameData instance;
    
    public GameData data = new GameData();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Load();
    }

    public void Save()
    {
        string gameData = JsonUtility.ToJson(instance.data);
        string filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, gameData);
        print("Game has Been Saved");
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        if (System.IO.File.Exists(filePath))
        {
            string gameData = System.IO.File.ReadAllText(filePath);
            instance.data = JsonUtility.FromJson<GameData>(gameData);
            print("Data has been loaded");
        }
        else
        {
            Save();
            Load();
        }
    }
}

[System.Serializable]

public class GameData
{
    public List<string> MapsCompleted = new List<string>();

    public bool TimerOn = false;
}


