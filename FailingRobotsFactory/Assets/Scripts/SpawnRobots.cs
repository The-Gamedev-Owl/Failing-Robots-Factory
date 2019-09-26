using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRobots : MonoBehaviour
{
    public int maxRobots;
    public GameObject basicRobotPrefab;

    public int actualRobots;

    void Start()
    {
        actualRobots = 0;
    }

    void Update()
    {
       if (gameObject.transform.childCount < maxRobots)
       {
           SpawnRobot(); // Argument with random between 1 -> 3
       }
    }

    private void SpawnRobot()
    {
        // Later check on how many random got
        SpawnBasicRobot(Random.Range(0, 101));
    }
    
    private void SpawnBasicRobot(int rand)
    {
        float ySpawn = GetYSpawn();

        if (rand <= 75)
        {
            if (Random.Range(0, 2) == 0)
                CreateBasicRobot(BasicRobot.AIBasicRobot.MOVE_LEFT, ySpawn);
            else
                CreateBasicRobot(BasicRobot.AIBasicRobot.MOVE_RIGHT, ySpawn);
        }
        else
            CreateBasicRobot(BasicRobot.AIBasicRobot.STILL, ySpawn);
        actualRobots += 1;
    }

    public BasicRobot CreateBasicRobot(BasicRobot.AIBasicRobot ai, float ySpawn)
    {
        GameObject newGo = Instantiate(basicRobotPrefab) as GameObject;
        BasicRobot newRobot = newGo.GetComponent<BasicRobot>();
        
        newRobot.transform.position = new Vector3(newRobot.transform.position.x, ySpawn, newRobot.transform.position.z);
        newRobot.transform.parent = transform;
        newRobot.AIRobot = ai;
        return newRobot;
    }

    private float GetYSpawn()
    {
        GameParameters gameParameters = FindObjectOfType<GameParameters>();
        float randomYPos = Random.Range(0, 101);
        float yPos;

        if (randomYPos <= 33)
            yPos = gameParameters.ySpawn1;
        else if (randomYPos <= 66)
            yPos = gameParameters.ySpawn2;
        else
            yPos = gameParameters.ySpawn3;
        return yPos;
    }
}
