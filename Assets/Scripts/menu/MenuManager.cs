using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public Text scoreText;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowRank()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UserNameEntered(string value)
    {
        ScoreManager sm = ScoreManager.instance;
        sm.SetUserName(value);
        //sm.LoadBestScore();

        string result = sm.GetHighestScore();
        if (!string.IsNullOrEmpty(result)){
            scoreText.text = $"Best Score: {result}";
        }

    }
}
