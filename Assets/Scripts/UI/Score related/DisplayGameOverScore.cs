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

    private AudioSource highscoreSound;

    private void Start()
    {
        int actualScore = ManageScore.GetActualScore();

        highscoreSound = GetComponent<AudioSource>();
        UpdateHighScore(actualScore);
        DisplayScores(actualScore);
    }

    private void UpdateHighScore(int actualScore)
    {
        if (actualScore > GetHighscore())
        {
            highscoreSound.Play();
            PlayerPrefs.SetInt("HighScore", actualScore);
        }
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
