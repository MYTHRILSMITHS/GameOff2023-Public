using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static int HighestScore = 0;
 

    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        LoadScore();
    }

    public void LoadScore()
    {
        var highscore = PlayerPrefs.GetInt("HighestScore", 0);
        if (highscore > HighestScore) HighestScore = highscore;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighestScore", HighestScore);
    }

   
    private void OnGUI()
    {
        GUILayout.Label("Score: " + score.ToString());
    }
}
