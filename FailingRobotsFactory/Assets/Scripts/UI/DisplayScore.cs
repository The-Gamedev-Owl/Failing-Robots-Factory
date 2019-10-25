using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    private void OnGUI()
    {
        scoreText.text = ManageScore.actualScore.ToString();
    }
}
