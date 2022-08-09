using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private string userName;
    private List<Score> bestScoreList;
    private int maxSize = 5;
    private string filePath;

    public static ScoreManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            Debug.Log("instance");
            instance = this;
            DontDestroyOnLoad(gameObject);

            bestScoreList = new List<Score>();
            filePath = Application.persistentDataPath + "/savefile.json";
            LoadBestScore();
            Debug.Log("File Path: " + filePath);
        }
    }

    public void SetUserName(string value)
    {
        userName = value;
    }

    public string GetHighestScore()
    {
        if(bestScoreList.Count > 0)
        {
            return bestScoreList[0].ToString();
        }
        return "";
    }

    public List<Score> GetBestScoreList()
    {
        return bestScoreList;
    }

    public void SaveBestScore(int value)
    {
        //Debug.Log("SaveBestScore begin: " + printBestScoreList());
        bool hasChanged = false;
        Score s = bestScoreList.Find(x => x.userName.Equals(userName));
        if (s != null)
        {
            if(value > s.score)
            {
                s.score = value;
                hasChanged = true;
            }
            
        }
        else if(bestScoreList.Count < maxSize)
        {
            bestScoreList.Add(new Score(userName, value));
            hasChanged = true;
        }
        else if(value > bestScoreList[^1].score)
        {
            bestScoreList.RemoveAt(bestScoreList.Count - 1);
            bestScoreList.Add(new Score(userName, value));
            hasChanged = true;
        }

        if (hasChanged)
        {
            bestScoreList.Sort();

            SaveData saveData = new();
            saveData.scores = bestScoreList.ToArray();

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(filePath, json);

            //Debug.Log("SaveBestScore: " + printBestScoreList());
        }

    }
    public void LoadBestScore()
    {
        // Only load once by checking if there is best score list
        if (bestScoreList.Count == 0 && File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            bestScoreList.AddRange(saveData.scores);

            //Debug.Log("LoadBestScore: " + printBestScoreList());
        }
    }

    public string printBestScoreList()
    {
        string result = "";
        foreach(Score s in bestScoreList)
        {
            result += ("[" + s.ToString() + "]");
        }
        return result;
    }


    [System.Serializable]
    private class SaveData
    {
        public Score[] scores;
    }
}

[System.Serializable]
public class Score : IComparable<Score>
{
    public string userName;
    public int score;

    public Score(string userName, int score)
    {
        this.userName = userName;
        this.score = score;
    }

    // Sort score from high to low
    public int CompareTo(Score other)
    {
        if(score < other.score)
        {
            return 1;
        }else if(score > other.score)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public override string ToString()
    {
        return userName + " : " + score;
    }
}
