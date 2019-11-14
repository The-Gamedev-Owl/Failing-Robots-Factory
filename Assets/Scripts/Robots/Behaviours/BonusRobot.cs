using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRobot : ARobot
{
    private bool hasBeenOnScreen;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Overrides
    private void Start()
    {
        isDying = false;
        hasBeenOnScreen = false;
        animator = GetComponent<Animator>();
        gameParameters = FindObjectOfType<GameParameters>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartMovingAnimation();
    }

    // Overrides
    private void FixedUpdate()
    {
        if (!isDying)
        {
            CheckInSight();
            moveSpeed = gameParameters.GetMoveSpeed() * 1.5f;
            animator.speed = gameParameters.GetAnimatorSpeed();
        }
        else
            animator.speed = 1;
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

    private void StartMovingAnimation()
    {
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetBool("MoveLeft", true);
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetBool("MoveRight", true);
    }

    public override void DieAbility()
    {
        ManageScore.AddScore(20);
        DeathAnimation();
    }

    protected override void DeathAnimation()
    {
        isDying = true;
        moveSpeed /= 3; // Looks like the robot falls along. Not stopping right on touch
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        UnshowParts();
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetTrigger("DeathLeft");
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetTrigger("DeathRight");
    }
}

