using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRobot : ARobot
{
    // Overrides
    private void Start()
    {
        gameParameters = FindObjectOfType<GameParameters>();
        RotateDependingOnDirection();
    }

    // Overrides
    private void FixedUpdate()
    {
        moveSpeed = gameParameters.GetMoveSpeed() * 1.5f;
        Move();
    }

    private void RotateDependingOnDirection()
    {
        Vector3 newRotation = transform.eulerAngles;

        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            newRotation.z = 90f;
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            newRotation.z = 270f;
        transform.eulerAngles = newRotation;
    }

    public override void DieAbility()
    {
        ManageScore.actualScore += 20;
        SelfDestruct();
    }
}

