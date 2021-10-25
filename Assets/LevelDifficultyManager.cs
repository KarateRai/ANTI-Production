using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficultyManager : MonoBehaviour
{

    [Header("Difficulity:")]
    [Range(1, 4)]
    public int difficulity;

    [Header("Starting values: min 1")]
    public int objectives;
    public int connections;
    public int spawners;


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

    }
}
