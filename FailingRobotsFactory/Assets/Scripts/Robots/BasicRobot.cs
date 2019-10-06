﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRobot : MonoBehaviour
{
    public RobotAI.AIRobot ai;

    private float moveSpeed;

    private void Start()
    {
        moveSpeed = FindObjectOfType<GameParameters>().GetMoveSpeed();
    }

    private void Update()
    {
        if (ai != RobotAI.AIRobot.STILL)
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
}
