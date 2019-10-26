using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRobot : ARobot
{
    public GameObject deathSprites;

    private int framesAlive;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Overrides
    private void Start()
    {
        isDying = false;
        framesAlive = 0;
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
            moveSpeed = gameParameters.GetMoveSpeed() * 1.5f;
        Move();
        framesAlive += 1;
    }

    private void OnBecameInvisible()
    {
        if (!isDying && framesAlive > 15) // Prevent from being destroyed when spawned
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
        ManageScore.actualScore += 20;
        DeathAnimation();
    }

    protected override void DeathAnimation()
    {
        isDying = true;
        moveSpeed /= 3; // Looks like the robot falls along. Not stopping right on touch
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        deathSprites.SetActive(true);
        if (ai == RobotAI.AIRobot.MOVE_LEFT)
            animator.SetTrigger("DeathLeft");
        else if (ai == RobotAI.AIRobot.MOVE_RIGHT)
            animator.SetTrigger("DeathRight");
    }
}

