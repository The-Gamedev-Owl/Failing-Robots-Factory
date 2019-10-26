using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float minimumAlphaMap;
    public float timeBeforeFade;
    public float fadeOutDuration;
    public Material mapFadeOutMat;
    public Material robotsFadeOutMat;

    private Coroutine fadeOutCoroutine;

    private void Start()
    {
        fadeOutCoroutine = null;
        ResetFade();
    }

    public void ResetFade()
    {
        mapFadeOutMat.color = Color.white;
        robotsFadeOutMat.color = Color.white;
        if (fadeOutCoroutine != null)
            StopCoroutine(fadeOutCoroutine);
        fadeOutCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startTime;
        float timeStep;
        float newAlphaRobot;
        float newAlphaMap;

        yield return new WaitForSeconds(timeBeforeFade);
        startTime = Time.time;
        while (mapFadeOutMat.color.a > 0)
        {
            timeStep = (Time.time - startTime) / fadeOutDuration;
            newAlphaRobot = Mathf.SmoothStep(1f, 0f, timeStep);
            newAlphaMap = Mathf.SmoothStep(1f, minimumAlphaMap, timeStep);
            mapFadeOutMat.color = new Color(1f, 1f, 1f, newAlphaMap);
            robotsFadeOutMat.color = new Color(1f, 1f, 1f, newAlphaRobot);
            yield return null;
        }
    }
}
