using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    public static DataManagement dataManagement;
    public int highScore;

    private void Awake()
    {
        if (dataManagement == null) 
        {
            DontDestroyOnLoad(gameObject);
            dataManagement = this;
        }else if (dataManagement != this)
        { // Si dans la nouvelle scène chargée il existe déjà un dataManagement on le supprime
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); //creating the file where data are saved
        GameData data = new GameData(); // creates container for data
        if (highScore >= data.highscore)
        {
            data.highscore = highScore;
        }
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            GameData gameData = (GameData)binaryFormatter.Deserialize(file);
            file.Close();
            highScore = gameData.highscore;
        }
    }
}


[Serializable]
class GameData
{
    public int highscore;

}
