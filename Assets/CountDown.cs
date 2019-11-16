using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Text countdownText;

    private void Start()
    {
        countdownText = GetComponent<Text>();
        Time.timeScale = 0;
        player.SetActive(false);
    }

    public void ChangeText(string newText)
    {
        countdownText.text = newText;
    }

    public void StartCountdown()
    {
        countdownText.text = "3";
    }

    public void EndCountdown()
    {
        Time.timeScale = 1;
        player.SetActive(true);
        StartCoroutine(DestroyAfterTime(0.1f)); // Destroy gameobject but not too soon otherwise timescale will not be set properly
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
