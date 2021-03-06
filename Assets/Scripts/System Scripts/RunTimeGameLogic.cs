using System;
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

    public List<GameObject> enemySpawnLocations;

    bool gameStart = false;

    private IEnumerator coroutine;
    private void Awake()
    {
        GameManager.instance.intensity = 2;
        GameManager.instance.gameLogic = this;
    }
    public void ResetGameValues()
    {
        gameTimer = 0;
        levelLives = DefaultLives;

    }

    public void ActivateGameLoop(List<GameObject> spawnNodes)
    {
        WaveSpawner waveSpawner = GameManager.instance.waveSpawner;
        enemySpawnLocations = spawnNodes;
        ResetGameValues();
        //Debug.Log("Spawning enemies");
        waveSpawner.StartWaves(spawnNodes);
        gameStart = true;
        UpdateCorruption();
        foreach(PlayerController playerController in players)
        {
            playerController.spawnPoint = playerController.transform.position;
        }
        GlobalEvents.instance.onWaveCleared += RespawnCheck;
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onWaveCleared -= RespawnCheck;
    }

    public void RespawnCheck()
    {
        //Debug.Log("Respawn Check... gameStart="+gameStart+" alivePlayerCount="+(players.Count-deadPlayers.Count));
        if (gameStart == true && deadPlayers.Count > 0 && deadPlayers.Count < players.Count)
        {
            //Debug.Log("Respawning dead units");
            foreach (PlayerController playerController in players)
            {
                if (playerController.IsDead())
                    playerController.Spawn();
            }
            GUIManager.instance.messageToast.NewMessage("Lost ANTI units restored!");
        }
        foreach (PlayerController pc in players)
        {
            pc.stats.ResetHealth();
        }
    }
    public void UpdateCorruption()
    {
        if (levelLives >= 0)
        {
            int totalProgress = (int)(((double)(DefaultLives - levelLives)) / DefaultLives * 100);
            GUIManager.instance.playerHUD.UpdateCorruption(totalProgress);
        }
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

            SetIntensity();
        }
    }

    /// <summary>
    /// Checks state of game to determine intensity level (2-10)
    /// </summary>
    private void SetIntensity()
    {
        int intensity = 2;
        //variables to potentially use: player average health, current wave #, corruption level
        if (GameManager.instance.waveSpawner.betweenWaves)
        {
            intensity = 2;
        }
        else if (players.Count > 0)
        {
            int averageHealth = 0;
            foreach (PlayerController p in players)
            {
                averageHealth += p.stats.GetHPP();
            }
            averageHealth /= players.Count;
            if (averageHealth < 50)
            {
                if (levelLives < DefaultLives / 2)
                {
                    intensity = 8;
                }
                else
                {
                    intensity = 6;
                }
            }
            else
            {
                if (levelLives < DefaultLives / 2)
                {
                    intensity = 7;
                }
                else
                {
                    intensity = 5;
                }
            }

        }
        GameManager.instance.intensity = intensity;
    }

    public void EndLevel()
    {
        ResetGameValues();

        //Show mission failed UI
        GlobalEvents.instance.onGameOver?.Invoke();

        coroutine = FadeLoadingScreen(7);
        StartCoroutine(coroutine);



    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        GUIManager.instance.ChangeToScene("TeamScene");
    }
}
