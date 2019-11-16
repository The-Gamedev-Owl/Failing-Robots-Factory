using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameOverScore : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highscoreText;

    private void Start()
    {
        int actualScore = ManageScore.GetActualScore();

        UpdateHighScore(actualScore);
        DisplayScores(actualScore);
    }

    private void UpdateHighScore(int actualScore)
    {
        if (actualScore > GetHighscore())
            PlayerPrefs.SetInt("HighScore", actualScore);
    }

    private void DisplayScores(int actualScore)
    {
        highscoreText.text = GetHighscore().ToString();
        scoreText.text = actualScore.ToString();
    }

    private int GetHighscore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
