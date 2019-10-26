using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRobot : ARobot
{
    private bool hasBeenOnScreen;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Overrides "Start" MonoBehaviour method from ARobot
    private void Start()
    {
        isDying = false;
        hasBeenOnScreen = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        gameParameters = FindObjectOfType<GameParameters>();
    }

    // Overrides
    private void FixedUpdate()
    {
        if (!isDying)
            moveSpeed = gameParameters.GetMoveSpeed();
        if (ai != RobotAI.AIRobot.STILL)
        {
            CheckInSight();
            animator.SetBool("IsMoving", true);
            Move();
        }
    }

    private void CheckInSight()
    {
        if (!hasBeenOnScreen && spriteRenderer.isVisible)
            hasBeenOnScreen = true;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (hasBeenOnScreen && (screenPosition.x < (-Screen.width + 300) || screenPosition.x > (Screen.width + 300)))
            Destroy(gameObject);
    }

    public override void DieAbility()
    {
        ManageScore.actualScore += 1;
        DeathAnimation();
    }

    protected override void DeathAnimation()
    {
        isDying = true;
        moveSpeed /= 3; // Looks like the robot falls along. Not stopping right on touch
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        UnshowParts();
        animator.SetTrigger("DeathTrigger");
    }
}
