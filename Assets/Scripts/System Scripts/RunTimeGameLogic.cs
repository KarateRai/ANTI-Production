using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeGameLogic : MonoBehaviour
{
    public int levelLives;
    float gameTimer = 0;
    public static float score;

    bool gameStart;

    public void ResetGameValues()
    {
        int DefaultLives = 20;

        gameTimer = 0;
        levelLives = DefaultLives;

    }

    public void ActivateGameLoop()
    {
        gameStart = true;
    }

    public void Update()
    {
        if (levelLives <= 0)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        ResetGameValues();

        //Return to main menu.
    }

}
