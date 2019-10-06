using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRobots : MonoBehaviour
{
    public int maxRobots;
    /* Basic Robots */
    public GameObject basicRobotPrefab;
    /* Bonus Robots */
    public GameObject bonusRobotPrefab;
    public float bonusRobotSpawnRate;

    private void Start()
    {
        InvokeRepeating("SpawnBonusRobot", 1, bonusRobotSpawnRate);
    }

    void Update()
    {
       if (gameObject.transform.childCount < maxRobots)
       {
            SpawnBasicRobot(Random.Range(0, 100));
       }
    }

    private void SpawnBonusRobot()
    {
        float xSpawn;
        float ySpawn;
        RobotAI.AIRobot ai;

        if (Time.time >= bonusRobotSpawnRate)
        {
            ySpawn = GenerateYSpawn();
            if (Random.Range(0, 2) == 0)
                ai = RobotAI.AIRobot.MOVE_LEFT;
            else
                ai = RobotAI.AIRobot.MOVE_RIGHT;
            xSpawn = GenerateXSpawnMoving(ai, ySpawn, bonusRobotPrefab, false);
            CreateBonusRobot(xSpawn, ySpawn, ai);
        }
    }
    
    private void SpawnBasicRobot(int rand)
    {
        float ySpawn = GenerateYSpawn();
        float xSpawn;
        RobotAI.AIRobot movementType;

        if (rand < 90) // 90% chance to create a moving robot
        {
            if (Random.Range(0, 2) == 0)
                movementType = RobotAI.AIRobot.MOVE_LEFT;
            else
                movementType = RobotAI.AIRobot.MOVE_RIGHT;
            xSpawn = GenerateXSpawnMoving(movementType, ySpawn, basicRobotPrefab);
            if (!float.IsNaN(xSpawn))
                CreateBasicRobot(movementType, xSpawn, ySpawn);
        }
        else
        {
            xSpawn = GenerateXSpawnStill(ySpawn);
            if (!float.IsNaN(xSpawn))
                CreateBasicRobot(RobotAI.AIRobot.STILL, xSpawn, ySpawn);
        }
    }

    public void CreateBonusRobot(float xSpawn, float ySpawn, RobotAI.AIRobot ai)
    {
        GameObject newGo = Instantiate(bonusRobotPrefab) as GameObject;
        BonusRobot newRobot = newGo.GetComponent<BonusRobot>();

        newRobot.transform.position = new Vector3(xSpawn, ySpawn, newRobot.transform.position.z);
        newRobot.transform.parent = transform;
        newRobot.ai = ai;
    }

    public BasicRobot CreateBasicRobot(RobotAI.AIRobot ai, float xSpawn, float ySpawn)
    {
        GameObject newGo = Instantiate(basicRobotPrefab) as GameObject;
        BasicRobot newRobot = newGo.GetComponent<BasicRobot>();

        newRobot.transform.position = new Vector3(xSpawn, ySpawn, newRobot.transform.position.z);
        newRobot.transform.parent = transform;
        newRobot.ai = ai;
        return newRobot;
    }

    /* Generates a random X position on borders of the screen*/
    private float GenerateXSpawnMoving(RobotAI.AIRobot movementType, float ySpawn, GameObject prefab, bool checkOnOther = true)
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        float leftSide = -halfWidth - (prefab.transform.localScale.x * 10); // Multplying by 10 allows the robot not to be too close to screen borders
        float rightSide = halfWidth + (prefab.transform.localScale.x * 10);

        if (movementType == RobotAI.AIRobot.MOVE_LEFT)
        {
            if (checkOnOther && CheckSpawnOnOther(rightSide, ySpawn) == true)
                return float.NaN;
            return rightSide;
        }
        else
        {
            if (checkOnOther && CheckSpawnOnOther(leftSide, ySpawn) == true)
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
