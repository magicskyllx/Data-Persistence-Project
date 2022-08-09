using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private string userName;
    private int score;

    private string bestUserName;
    private int bestScore;

    private string filePath;

    public static ScoreManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        filePath = Application.persistentDataPath + "/savefile.json";
        Debug.Log("File Path: " + filePath);
    }

    public void SetUserName(string value)
    {
        Debug.Log("Set userName: " + value);
        userName = value;
    }

    public string GetBestUserName()
    {
        return bestUserName;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SaveBestScore(int value)
    {
        if(value > bestScore)
        {
            bestScore = value;
            bestUserName = userName;

            SaveData saveData = new SaveData();
            saveData.userName = bestUserName;
            saveData.score = bestScore;

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(filePath, json);
        }
        
    }
    public void LoadBestScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            bestUserName = saveData.userName;
            bestScore = saveData.score;
        }
    }


    [System.Serializable]
    private class SaveData
    {
        public string userName;
        public int score;
    }
}
