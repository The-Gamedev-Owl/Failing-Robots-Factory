﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public float ySpawn1 = 7.685f;
    public float ySpawn2 = 0.429f;
    public float ySpawn3 = -6.946f;

    private float initialSpeed;
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = 15f;
        initialSpeed = moveSpeed;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed(float waitTime)
    {
        StartCoroutine(ResetSpeedAfter(waitTime));
    }

    private IEnumerator ResetSpeedAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moveSpeed = initialSpeed;
    }
}