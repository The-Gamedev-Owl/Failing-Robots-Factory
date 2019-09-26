using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRobot : MonoBehaviour
{
    public enum AIBasicRobot
    {
        MOVE_LEFT,
        MOVE_RIGHT,
        STILL
    }

    public AIBasicRobot AIRobot;

    private float moveSpeed;

    private void Start()
    {
        moveSpeed = FindObjectOfType<GameParameters>().GetMoveSpeed();
    }

    private void Update()
    {
        if (AIRobot != AIBasicRobot.STILL)
            Move();
    }

    private void Move()
    {
        int multiplicator = AIRobot == AIBasicRobot.MOVE_LEFT ? -1 : 1; // Assign 1 if going to right, -1 if going to left
        Vector3 position = transform.position;

        position += ((Vector3.right * Time.deltaTime) * moveSpeed) * multiplicator;
        transform.position = position;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
