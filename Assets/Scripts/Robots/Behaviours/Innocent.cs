using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Innocent : ARobot
{
    public float disappearTime;

    private Animator animator;
    private BoxCollider2D boxCollider;

    // Overrides "Start" MonoBehaviour method from ARobot
    private void Start()
    {
        isDying = false;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gameParameters = FindObjectOfType<GameParameters>();
        StartCoroutine(DisappearAfterTime());
    }

    // Override
    private void FixedUpdate()
    {
        if (!isDying)
            animator.speed = gameParameters.GetAnimatorSpeed();
        else
            animator.speed = 1;
    }

    public override void DieAbility()
    {
        PlayerPrefs.SetInt("InnocentsKilled", PlayerPrefs.GetInt("InnocentsKilled", 0) + 1);
        DeathAnimation();
    }

    protected override void DeathAnimation()
    {
        isDying = true;
        boxCollider.enabled = false;
        animator.SetTrigger("DeathTrigger");
    }

    private IEnumerator DisappearAfterTime()
    {
        yield return new WaitForSeconds(disappearTime);
        if (!isDying)
            Destroy(gameObject);
    }
}
