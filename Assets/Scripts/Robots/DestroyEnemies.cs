using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    private Sight playerSight;

    private void Start()
    {
        playerSight = GetComponent<Sight>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            KillRobotOnTouch();
    }

    private void KillRobotOnTouch()
    {
        Touch actualTouch = Input.GetTouch(0);
        RaycastHit2D hit;

        if (actualTouch.phase == TouchPhase.Began)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(actualTouch.position), Vector2.zero);
            playerSight.ResetFade();
            if (hit.transform != null)
                hit.transform.gameObject.GetComponent<ARobot>().DieAbility();
        }
    }
}

