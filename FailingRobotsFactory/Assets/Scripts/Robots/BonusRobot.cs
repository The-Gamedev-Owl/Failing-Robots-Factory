using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRobot : MonoBehaviour
{
    public RobotAI.AIRobot ai;

    private float moveSpeed;

    private void Start()
    {
        moveSpeed = FindObjectOfType<GameParameters>().GetMoveSpeed() * 1.5f;
        RotateDependingOnAI();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        int multiplicator = ai == RobotAI.AIRobot.MOVE_LEFT ? -1 : 1; // Assign 1 if going to right, -1 if going to left
        Vector3 position = transform.position;

        position += ((Vector3.right * Time.deltaTime) * moveSpeed) * multiplicator;
        transform.position = position;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void RotateDependingOnAI()
    {
        Vector3 newRotation = transform.eulerAngles;

        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            newRotation.z = 90f;
        if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            newRotation.z = 270f;
        transform.eulerAngles = newRotation;
    }
}

