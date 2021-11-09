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

    void CalculateDifficulity()
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
    }

    void SetEasyValues()
    {
        
    }
    void SetMediumValues()
    {

    }
    void SetHardValues()
    {

    }
    void SetNightmareValues()
    {

    }
    void SetProgressiveValues()
    {

    }
    void TransferDifficulity()
    {

    }
}
