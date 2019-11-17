using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public int increaseDifficultyScoreStep;
    public float difficultyMultiplier;
    public float ySpawn1 = 7.685f;
    public float ySpawn2 = 0.429f;
    public float ySpawn3 = -6.946f;

    private float scoreSinceLastIncrease;
    private float initialSpeed;
    private float moveSpeed;

    private void Start()
    {
        scoreSinceLastIncrease = 0;
        moveSpeed = 15f;
        initialSpeed = moveSpeed;
    }

    public void ScoreUpdated(int addedScore)
    {
        scoreSinceLastIncrease += addedScore;
        if (scoreSinceLastIncrease >= increaseDifficultyScoreStep) // Every 'increaseDifficultyScoreStep', moveSpeed will increase
        {
            ChangeSpeed(moveSpeed + difficultyMultiplier);
            scoreSinceLastIncrease = 0;
        }
    }

    #region AnimatorSpeed
    public float GetAnimatorSpeed()
    {
        return moveSpeed / initialSpeed;
    }
    #endregion AnimatorSpeed

    #region MoveSpeed
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    #endregion MoveSpeed

    #region SpeedModifiers
    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed(float waitTime)
    {
        StartCoroutine(ResetSpeedAfter(waitTime));
    }

    private IEnumerator ResetSpeedAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moveSpeed = initialSpeed;
    }
    #endregion SpeedModifiers
}
