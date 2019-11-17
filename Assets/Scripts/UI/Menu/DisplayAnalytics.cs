using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAnalytics : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnimator;
    /* Robots */
    [SerializeField]
    private Text innocentKilledText;
    [SerializeField]
    private Text basicKilledText;
    [SerializeField]
    private Text bonusKilledText;
    [SerializeField]
    private Text timeslowKilledText;
    /* General */
    [SerializeField]
    private Text gamesPlayedText;
    [SerializeField]
    private Text highscoreText;

    void Start()
    {
        UpdateAnalyticsTexts();
    }

    private void UpdateAnalyticsTexts()
    {
        /* Robots */
        innocentKilledText.text = PlayerPrefs.GetInt("InnocentsKilled", 0).ToString();
        basicKilledText.text = PlayerPrefs.GetInt("BasicsKilled", 0).ToString();
        bonusKilledText.text = PlayerPrefs.GetInt("BonusesKilled", 0).ToString();
        timeslowKilledText.text = PlayerPrefs.GetInt("TimeslowsKilled", 0).ToString();
        /* General */
        gamesPlayedText.text = PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
        highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void ResetAnalytics()
    {
        PlayerPrefs.DeleteAll();
        UpdateAnalyticsTexts();
    }

    public void BackToMenu()
    {
        menuAnimator.SetTrigger("BackToMenu");
    }
}
