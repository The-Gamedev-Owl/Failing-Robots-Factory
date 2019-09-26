using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public float ySpawn1 = 7.685f;
    public float ySpawn2 = 0.429f;
    public float ySpawn3 = -6.946f;

    private float moveSpeed = 15f;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
