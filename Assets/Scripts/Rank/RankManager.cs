using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [SerializeField]
    private Text[] user;
    [SerializeField]
    private Text[] score;

    // Start is called before the first frame update
    void Start()
    {
        if(ScoreManager.instance == null)
        {
            return;
        }

        List<Score> bestScoreList = ScoreManager.instance.GetBestScoreList();
        for(int i = 0; i < bestScoreList.Count; i++)
        {
            user[i].text = bestScoreList[i].userName;
            score[i].text = bestScoreList[i].score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
