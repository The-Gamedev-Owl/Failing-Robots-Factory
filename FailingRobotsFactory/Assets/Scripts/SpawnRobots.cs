using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRobots : MonoBehaviour
{
    public int maxRobots;
    public GameObject basicRobotPrefab;

    void Update()
    {
       if (gameObject.transform.childCount < maxRobots)
       {
           SpawnRobot(); //// Argument with random between 1 -> 3
       }
    }

    private void SpawnRobot()
    {
        //// Later check on how many random got
        SpawnBasicRobot(Random.Range(0, 100));
    }
    
    private void SpawnBasicRobot(int rand)
    {
        float ySpawn = GenerateYSpawn();
        float xSpawn;
        BasicRobot.AIBasicRobot movementType;

        if (rand < 90) // 90% chance to create a moving robot
        {
            if (Random.Range(0, 2) == 0)
                movementType = BasicRobot.AIBasicRobot.MOVE_LEFT;
            else
                movementType = BasicRobot.AIBasicRobot.MOVE_RIGHT;
            xSpawn = GenerateXSpawnMoving(movementType, ySpawn);
            if (!float.IsNaN(xSpawn))
                CreateBasicRobot(movementType, xSpawn, ySpawn);
        }
        else
        {
            xSpawn = GenerateXSpawnStill(ySpawn);
            if (!float.IsNaN(xSpawn))
                CreateBasicRobot(BasicRobot.AIBasicRobot.STILL, xSpawn, ySpawn);
        }
    }

    public BasicRobot CreateBasicRobot(BasicRobot.AIBasicRobot ai, float xSpawn, float ySpawn)
    {
        GameObject newGo = Instantiate(basicRobotPrefab) as GameObject;
        BasicRobot newRobot = newGo.GetComponent<BasicRobot>();

        newRobot.transform.position = new Vector3(xSpawn, ySpawn, newRobot.transform.position.z);
        newRobot.transform.parent = transform;
        newRobot.AIRobot = ai;
        return newRobot;
    }

    /* Generates a random X position on borders of the screen*/
    private float GenerateXSpawnMoving(BasicRobot.AIBasicRobot movementType, float ySpawn)
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        float leftSide = -halfWidth - (basicRobotPrefab.transform.localScale.x * 10); // Multplying by 10 allows the robot not to be too close to screen borders
        float rightSide = halfWidth + (basicRobotPrefab.transform.localScale.x * 10);

        if (movementType == BasicRobot.AIBasicRobot.MOVE_LEFT)
        {
            if (CheckSpawnOnOther(rightSide, ySpawn) == true)
                return float.NaN;
            return rightSide;
        }
        else
        {
            if (CheckSpawnOnOther(leftSide, ySpawn) == true)
                return float.NaN;
            return leftSide;
        }
    }

    /* Generates a random X position within screen size */
    float GenerateXSpawnStill(float ySpawn)
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        float minWidth = -halfWidth + (basicRobotPrefab.transform.localScale.x * 10); // Multplying by 10 allows the robot not to be too close to screen borders
        float maxWidth = halfWidth - (basicRobotPrefab.transform.localScale.x * 10);
        float xSpawn = Random.Range(minWidth, maxWidth);

        if (CheckSpawnOnOther(xSpawn, ySpawn) == true)
            return float.NaN;
        return xSpawn;
    }

    private float GenerateYSpawn()
    {
        GameParameters gameParameters = FindObjectOfType<GameParameters>();
        float randomYPos = Random.Range(0, 100);
        float yPos;

        if (randomYPos < 33)
            yPos = gameParameters.ySpawn1;
        else if (randomYPos < 66)
            yPos = gameParameters.ySpawn2;
        else
            yPos = gameParameters.ySpawn3;
        return yPos;
    }

    private bool CheckSpawnOnOther(float xSpawn, float ySpawn)
    {
        float childScale;

        foreach (Transform child in transform)
        {
            childScale = child.localScale.x * 15; // Multplying by 15 allows robots not to be too close to each others
            if (ySpawn == child.position.y && (child.position.x - childScale <= xSpawn && child.position.x + childScale >= xSpawn))
                return true;
        }
        return false;
    }
}
