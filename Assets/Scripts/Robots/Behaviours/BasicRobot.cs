using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRobot : ARobot
{
    private Animator animator;

    // Overrides "Start" MonoBehaviour method from ARobot
    private void Start()
    {
        animator = GetComponent<Animator>();
        gameParameters = FindObjectOfType<GameParameters>();
    }

    // Overrides
    private void FixedUpdate()
    {
        moveSpeed = gameParameters.GetMoveSpeed();
        if (ai != RobotAI.AIRobot.STILL)
            Move();
        else
            animator.enabled = false;
    }

    public override void DieAbility()
    {
        ManageScore.actualScore += 1;
    }
}
