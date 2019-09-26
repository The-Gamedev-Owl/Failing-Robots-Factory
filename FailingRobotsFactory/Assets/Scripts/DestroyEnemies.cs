using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    private RaycastHit2D hit;

    private void Update()
    {
        for (int input = 0; input < Input.touchCount; input++)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(input).position), Vector2.zero);

            if (hit.transform != null)
                Destroy(hit.transform.gameObject);
        }
    }
}
