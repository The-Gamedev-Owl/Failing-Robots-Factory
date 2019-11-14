using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyEnemies : MonoBehaviour
{
    public GameObject errorPrefab;

    private bool hasLost;
    private Sight playerSight;

    private void Start()
    {
        hasLost = false;
        playerSight = GetComponent<Sight>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            KillRobotOnTouch();
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            Vector3 touchedPosition;

            if (!hasLost)
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform != null)
                {
                    playerSight.ResetFade();
                    hit.transform.gameObject.GetComponent<ARobot>().DieAbility();
                }
                else
                {
                    hasLost = true;
                    playerSight.StopFade();
                    touchedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchedPosition.z = 0f;
                    Instantiate(errorPrefab, touchedPosition, new Quaternion(0f, 0f, 0f, 0f));
                    StartCoroutine(CameraZoomInLoose(touchedPosition));
                }
            }
        }
#endif 
    }

    private void KillRobotOnTouch()
    {
        Touch actualTouch = Input.GetTouch(0);
        RaycastHit2D hit;
        Vector3 touchedPosition;

        if (!hasLost && actualTouch.phase == TouchPhase.Began)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(actualTouch.position), Vector2.zero);
            if (hit.transform != null)
            {
                playerSight.ResetFade();
                hit.transform.gameObject.GetComponent<ARobot>().DieAbility();
            }
            else
            {
                hasLost = true;
                playerSight.StopFade();
                touchedPosition = Camera.main.ScreenToWorldPoint(actualTouch.position);
                touchedPosition.z = 0f;
                Instantiate(errorPrefab, touchedPosition, new Quaternion(0f, 0f, 0f, 0f));
                StartCoroutine(CameraZoomInLoose(touchedPosition));
            }
        }
    }

    private IEnumerator CameraZoomInLoose(Vector3 touchedPosition)
    {
        float timeGoal = Time.time + 1f;

        touchedPosition.z = Camera.main.transform.position.z;
        while (Time.time < timeGoal)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, touchedPosition, Time.deltaTime * 5f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 7f, Time.deltaTime * 5f);
            yield return null;
        }
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }
}

