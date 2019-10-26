using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslowRobot : ARobot
{
    public float slowTimeDuration;
    public GameObject deathSprites;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    // Overrides
    private void Start()
    {
        isDying = false;
        gameParameters = FindObjectOfType<GameParameters>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        AnimationSpeedDependingOnDirection();
    }

    // Overrides
    private void FixedUpdate()
    {
        if (!isDying)
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
        DeathAnimation();
    }

    protected override void DeathAnimation()
    {
        isDying = true;
        moveSpeed /= 5; // Looks like the robot falls along. Not stopping right on touch
        circleCollider.enabled = false;
        spriteRenderer.enabled = false;
        deathSprites.SetActive(true);
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetTrigger("DeathLeft");
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetTrigger("DeathRight");
    }
}

