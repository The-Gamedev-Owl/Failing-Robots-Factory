using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslowRobot : ARobot
{
    public float slowTimeDuration;

    private Animator animator;

    // Overrides
    private void Start()
    {
        gameParameters = FindObjectOfType<GameParameters>();
        animator = GetComponent<Animator>();
        AnimationSpeedDependingOnDirection();
    }

    // Overrides
    private void FixedUpdate()
    {
        moveSpeed = gameParameters.GetMoveSpeed() * 2f;
        Move();
    }

    private void AnimationSpeedDependingOnDirection()
    {
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetBool("MoveLeft", true);
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetBool("MoveRight", true);
    }

    public override void DieAbility()
    {
        ManageScore.actualScore += 1;
        gameParameters.ChangeSpeed(gameParameters.GetMoveSpeed() / 3);
        gameParameters.ResetSpeed(slowTimeDuration); // Changes speed back to normal afer 'slowTimeDuration'
        SelfDestruct();
    }
}

