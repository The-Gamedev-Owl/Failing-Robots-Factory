﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRobots : MonoBehaviour
{
    public int maxRobots;
    /* Innocent Robots */
    public GameObject innocentPrefab;
    public int innocentSpawnRate;
    /* Basic Robots */
    public GameObject basicRobotPrefab;
    /* Bonus Robots */
    public GameObject bonusRobotPrefab;
    public int bonusRobotSpawnRate;
    /* Time Slow Robots */
    public GameObject timeslowRobotPrefab;
    public int timeslowRobotSpawnRate;

    private int stackedScoreInnocentRobots;
    private int stackedScoreSpecialRobots;

    private void Start()
    {
        stackedScoreInnocentRobots = 0;
        stackedScoreSpecialRobots = 0;
    }

    void Update()
    {
       if (gameObject.transform.childCount < maxRobots)
            SpawnBasicRobot(Random.Range(0, 100));
    }

    #region ScoreStacking
    public void ScoreUpdated(int toAdd)
    {
        StackInnocentScore(toAdd);
        StackBonusRobotsScore(toAdd);
    }

    private void StackInnocentScore(int toAdd)
    {
        stackedScoreInnocentRobots += toAdd;
        if (stackedScoreInnocentRobots >= innocentSpawnRate)
        {
            stackedScoreInnocentRobots -= innocentSpawnRate;
            SpawnInnocentRobot();
        }
    }

    private void StackBonusRobotsScore(int toAdd)
    {
        stackedScoreSpecialRobots += toAdd;
        if (stackedScoreSpecialRobots == bonusRobotSpawnRate) // If actual score is equal to 'bonusSpawnRate'
            SpawnSpecialRobots(bonusRobotPrefab);
        if (stackedScoreSpecialRobots >= timeslowRobotSpawnRate) // If actual score is equal or greater than 'timeslowSpawnRate'
        {
            SpawnSpecialRobots(timeslowRobotPrefab);
            stackedScoreSpecialRobots = 0;
        }
    }
    #endregion ScoreStacking

    #region RobotCreation
    private void SpawnSpecialRobots(GameObject robotPrefab)
    {
        float xSpawn;
        float ySpawn;
        RobotAI.AIRobot ai;

        ySpawn = GenerateYSpawn();
        if (Random.Range(0, 2) == 0)
            ai = RobotAI.AIRobot.MOVE_LEFT;
        else
            ai = RobotAI.AIRobot.MOVE_RIGHT;
        xSpawn = GenerateXSpawnMoving(ai, ySpawn, robotPrefab, false);
        CreateRobot(xSpawn, ySpawn, ai, robotPrefab, true);
    }

    private void SpawnInnocentRobot()
    {
        float ySpawn = GenerateYSpawn();
        float xSpawn = GenerateXSpawnStill(ySpawn);

        if (!float.IsNaN(xSpawn))
            CreateRobot(xSpawn, ySpawn, RobotAI.AIRobot.STILL, innocentPrefab, false);
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
                CreateRobot(xSpawn, ySpawn, movementType, basicRobotPrefab, true);
        }
        else
        {
            xSpawn = GenerateXSpawnStill(ySpawn);
            if (!float.IsNaN(xSpawn))
                CreateRobot(xSpawn, ySpawn, RobotAI.AIRobot.STILL, basicRobotPrefab, true);
        }
    }

    public void CreateRobot(float xSpawn, float ySpawn, RobotAI.AIRobot ai, GameObject robotPrefab, bool setParent)
    {
        GameObject newGo = Instantiate(robotPrefab) as GameObject;
        ARobot newRobot = newGo.GetComponent<ARobot>();

        newRobot.transform.position = new Vector3(xSpawn, ySpawn, newRobot.transform.position.z);
        if (setParent)
            newRobot.transform.parent = transform;
        newRobot.ai = ai;
    }
    #endregion RobotCreation

    #region GenerateSpawnPosition
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
    #endregion GenerateSpawnPosition
}
