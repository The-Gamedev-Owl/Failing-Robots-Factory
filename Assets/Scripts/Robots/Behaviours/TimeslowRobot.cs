using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslowRobot : ARobot
{
    public float slowTimeDuration;

    private bool hasBeenOnScreen;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    // Overrides
    private void Start()
    {
        isDying = false;
        hasBeenOnScreen = false;
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
        {
            CheckInSight();
            moveSpeed = gameParameters.GetMoveSpeed() * 2f;
        }
        Move();
    }

    private void CheckInSight()
    {
        if (!hasBeenOnScreen && spriteRenderer.isVisible)
            hasBeenOnScreen = true;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (hasBeenOnScreen && (screenPosition.x < (-Screen.width + 300) || screenPosition.x > (Screen.width + 300)))
            Destroy(gameObject);
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
        ManageScore.AddScore(1);
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
        UnshowParts();
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetTrigger("DeathLeft");
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetTrigger("DeathRight");
    }
}

