using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeGameLogic : MonoBehaviour
{
    public int levelLives;
    float gameTimer = 0;
    public static float score;

    bool gameStart;

    private IEnumerator coroutine;

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

        //Show mission failed UI

        coroutine = FadeLoadingScreen(5);
        StartCoroutine(coroutine);

        

    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        GUIManager.instance.ChangeToScene("TeamScene");
    }
}
