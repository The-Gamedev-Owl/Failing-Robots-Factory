using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ARobot : MonoBehaviour
{
    public RobotAI.AIRobot ai;

    protected GameParameters gameParameters;
    protected float moveSpeed;

    #region MonoBehaviourMethods
    private void Start()
    {
        gameParameters = FindObjectOfType<GameParameters>();
    }

    private void FixedUpdate()
    {
        moveSpeed = gameParameters.GetMoveSpeed();
        Move();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    #endregion MonoBehaviourMethods

    protected void Move()
    {
        int multiplicator = ai == RobotAI.AIRobot.MOVE_LEFT ? -1 : 1; // Assign 1 if going to right, -1 if going to left
        Vector3 position = transform.position;

        position += ((Vector3.right * Time.deltaTime) * moveSpeed) * multiplicator;
        transform.position = position;
    }

    abstract public void DieAbility();

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
