using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeGameLogic : MonoBehaviour
{
    public int levelLives = 20;
    float gameTimer = 0;
    public static float score;
    int DefaultLives = 20;

    public List<PlayerController> players;
    public List<PlayerController> alivePlayer;
    public List<PlayerController> deadPlayers;

    WaveSpawner waveSpawner;

    bool gameStart = false;

    private IEnumerator coroutine;

    private void Awake()
    {
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        GameManager.instance.gameLogic = this;
    }

    public void ResetGameValues()
    {
        gameTimer = 0;
        levelLives = DefaultLives;

    }

    public void ActivateGameLoop(List<GameObject> spawnNodes)
    {
        ResetGameValues();
        Debug.Log("Spawning enemies");
        waveSpawner.StartWaves(spawnNodes);
        gameStart = true;
    }

    public int GetCorruption()
    {
        int totalProgress = (int)((double)levelLives / DefaultLives * 100);
        return totalProgress;
    }

    public void Update()
    {
        if (gameStart == true)
        {
            if (levelLives <= 0 || deadPlayers.Count == players.Count && players.Count > 0)
            {
                //Player stats are unset
                EndLevel();
            }
            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].IsDead() && deadPlayers.Contains(players[i]))
                {
                    deadPlayers.Remove(players[i]);
                }
                else if (players[i].IsDead() && !deadPlayers.Contains(players[i]))
                {
                    deadPlayers.Add(players[i]);
                }
            }
        }
    }

    public void EndLevel()
    {
        ResetGameValues();

        //Show mission failed UI
        GlobalEvents.instance.onGameOver?.Invoke();

        coroutine = FadeLoadingScreen(5);
        StartCoroutine(coroutine);

        

    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        GUIManager.instance.ChangeToScene("TeamScene");
    }
}
