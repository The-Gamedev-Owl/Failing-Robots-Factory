using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour
{
    static public int actualScore;

    static private GameParameters gameParameters;
    static private SpawnRobots spawner;

    private void Start()
    {
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed", 0) + 1);
        actualScore = 0;
        spawner = GetComponent<SpawnRobots>();
        gameParameters = FindObjectOfType<GameParameters>();
    }

    static public int GetActualScore()
    {
        return actualScore;
    }

    static public void AddScore(int toAdd)
    {
        actualScore += toAdd;
        spawner.ScoreUpdated(toAdd);
        gameParameters.ScoreUpdated(toAdd);
    }
}
