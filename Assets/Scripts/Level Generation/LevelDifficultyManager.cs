using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficultyManager : MonoBehaviour
{

    [Header("Difficulity:")]
    [Range(1, 4)]
    public int difficulity;

    int easy = 1;
    int medium = 2;
    int hard = 3;
    int nightmare = 4;

    [Header("Starting values: min 1")]
    private int objectives;
    private int connections;
    private int spawners;

    [Header("Increase per level: 1/4 to 4/4")]
    [Range(1, 4)]
    public int objectiveNodeStep;

    [Range(1, 4)]
    public int spawnerNodeStep;

    [Range(1, 4)]
    public int connectionNodeStep;

    [Header("Modifiers:")]
    public bool forceMap;
    public bool obstacles;
    public bool forbidDiagonals;
    public bool levelDifficulity;

    Dictionary<string, int> levelInformation = new Dictionary<string, int>();

    public Dictionary<string, int> CalculateDifficulity(int difficulity)
    {
        if (difficulity == easy)
        {
            SetEasyValues();
        }
        else if (difficulity == medium)
        {
            SetMediumValues();
        }
        else if (difficulity == hard)
        {
            SetHardValues();
        }
        else if (difficulity == nightmare)
        {
            SetNightmareValues();
        }
        else
        {
            Debug.LogError("No Difficulity set, setting to default");
            SetMediumValues();
        }
        return levelInformation;
    }

    void SetEasyValues()
    {
        connectionNodeStep = 1;
        connections = connectionNodeStep;
        levelInformation.Add("Connections", connections);

        objectiveNodeStep = 1;
        objectives = objectiveNodeStep;
        levelInformation.Add("Objectives", objectives);

        spawnerNodeStep = 1;
        spawners = spawnerNodeStep;
        levelInformation.Add("Ai-spawner", spawners);
    }
    void SetMediumValues()
    {
        connectionNodeStep = 2;
        connections = connectionNodeStep;
        levelInformation.Add("Connections", connections);

        objectiveNodeStep = 2;
        objectives = objectiveNodeStep;
        levelInformation.Add("Objectives", objectives);

        spawnerNodeStep = 2;
        spawners = spawnerNodeStep;
        levelInformation.Add("Ai-spawner", spawners);
    }
    void SetHardValues()
    {
        connectionNodeStep = 3;
        connections = connectionNodeStep;
        levelInformation.Add("Connections", connections);

        objectiveNodeStep = 2;
        objectives = objectiveNodeStep;
        levelInformation.Add("Objectives", objectives);

        spawnerNodeStep = 2;
        spawners = spawnerNodeStep;
        levelInformation.Add("Ai-spawner", spawners);
    }
    void SetNightmareValues()
    {
        connectionNodeStep = 2;
        connections = connectionNodeStep;
        levelInformation.Add("Connections", connections);

        objectiveNodeStep = 2;
        objectives = objectiveNodeStep;
        levelInformation.Add("Objectives", objectives);

        spawnerNodeStep = 4;
        spawners = spawnerNodeStep;
        levelInformation.Add("Ai-spawner", spawners);
    }
    void SetProgressiveValues()
    {

    }
    void TransferDifficulity()
    {

    }
}
