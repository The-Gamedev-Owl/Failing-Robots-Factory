using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ARobot : MonoBehaviour
{
    public RobotAI.AIRobot ai;
    public GameObject deathSprites;

    protected bool isDying;
    protected float moveSpeed;
    protected GameParameters gameParameters;

    #region MonoBehaviourMethods
    private void Start()
    {
        isDying = false;
        gameParameters = FindObjectOfType<GameParameters>();
    }

    private void FixedUpdate()
    {
        if (!isDying)
        {
            moveSpeed = gameParameters.GetMoveSpeed();
            Move();
        }
    }

    private void OnBecameInvisible()
    {
        if (!isDying)
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
    abstract protected void DeathAnimation();

    /* Will randomly deactivate some part from robots. This way, death animation will not always be exactly the same */
    protected void UnshowParts()
    {
        int minimumShown = deathSprites.transform.childCount / 3;
        int random = Random.Range(minimumShown, deathSprites.transform.childCount);

        deathSprites.SetActive(true);
        for (int i = 0; i < random; i++)
            deathSprites.transform.GetChild(Random.Range(i, random)).gameObject.SetActive(false);
        deathSprites.transform.Find("Explosion").gameObject.SetActive(true);
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
