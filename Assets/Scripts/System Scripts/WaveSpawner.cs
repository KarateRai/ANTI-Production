using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public int waveCount;
    public float wavePerSecond;
}
public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> waveEnemies = new List<GameObject>();
    public List<GameObject> enemiesAlive = new List<GameObject>();

    public bool betweenWaves;
    public int waveNumber = 1;
    float bossWave = 1;
    private List<List<Transform>> spawnNodes = new List<List<Transform>>();
    private List<List<int>> spawnNodesPointsUsed =  new List<List<int>>();
    //public GameObject spawnEffect;

    public float timeBetweenWaves = 20f;
    private float countdown = 10f;
    //[SerializeField] public Text waveCountdownText;


    private void Start()
    {
        GameManager.instance.waveSpawner = this;
        WaveGenerator.InitializeGenerator();
    }

    public void StartWaves(List<GameObject> aiSpawnNodes)
    {
        WaveGenerator.GenerateWave(waveNumber, ref waveEnemies);
        for (int sp = 0; sp < aiSpawnNodes.Count; sp++)
        {
            spawnNodes.Add(new List<Transform>());
            spawnNodesPointsUsed.Add(new List<int>());
        }
        for (int i = 0; i < aiSpawnNodes.Count; i++)
        {
            this.spawnNodes[i].AddRange(aiSpawnNodes[i].GetComponent<CellAction>().spawnPoints);
            for (int j = 0; j < this.spawnNodes[i].Count; j++)
            {
                spawnNodesPointsUsed[i].Add(j);
            }
        }
    }

    //Debugging
    public void KillWave()
    {
        foreach (GameObject item in enemiesAlive)
        {
            Destroy(item);
        }
        enemiesAlive.Clear();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemiesAlive.Remove(enemy);
        if (enemiesAlive.Count == 0)
        {
            GUIManager.instance.messageToast.NewMessage("Wave cleared!");
            countdown = timeBetweenWaves;
        }
            
    }

    void Update()
    {
        if (enemiesAlive.Count > 0)
        {
            betweenWaves = false;
            return;
        }
        else if (countdown >= timeBetweenWaves)
        {
            ResetSpawnPoints();
            betweenWaves = true;
        }

        #region Countdown Timer

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            return;
        }

        #endregion

        countdown -= Time.deltaTime;
    }

    private void ResetSpawnPoints()
    {
        if (spawnNodesPointsUsed.Count != 0)
        {
            return;
        }

        for (int i = 0; i < spawnNodes.Count; i++)
        {
            spawnNodesPointsUsed.Add(new List<int>());
            for (int j = 0; j < spawnNodes[i].Count; j++)
            {
                spawnNodesPointsUsed[i].Add(j);
            }
        }
    }

    IEnumerator SpawnWave()
    {
        if (waveNumber % 10 == 0)
            WaveGenerator.GenerateBossWave(waveNumber, bossWave, ref waveEnemies);
        else
            WaveGenerator.GenerateWave(waveNumber, ref waveEnemies);
        GUIManager.instance.messageToast.NewMessage("Wave " + waveNumber);
        //GameObject effect = (GameObject)Instantiate(spawnEffect, spawnPoint.transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(0.4f);
        
        for (int j = 0; j < waveEnemies.Count; j++)
        {
            SpawnEnemy(waveEnemies[j]);
            if (j == Random.Range(3,6))
            {
                yield return new WaitForSeconds(1f);
            }
        }
        
        
        waveNumber++;
        //Destroy(effect, 1f);
        }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnNode = Random.Range(0, spawnNodesPointsUsed.Count); //Mellan 0-pointsLeft
        if (spawnNode > spawnNodesPointsUsed.Count - 1)
        {
            Debug.Log("ERROR: SPAWNNODE RANDOM NR: " + spawnNode + " , SPAWNNODESSUSED COUNT: " + spawnNodesPointsUsed.Count);
        }
        else
        {
            Debug.Log("OK: SPAWNNODE RANDOM NR: " + spawnNode + " , SPAWNNODESSUSED COUNT: " + spawnNodesPointsUsed.Count);
        }
        if (spawnNodesPointsUsed[spawnNode].Count == 0)
        {
            spawnNodesPointsUsed.Remove(spawnNodesPointsUsed[spawnNode]);
            if (spawnNodesPointsUsed.Count == 0)
            {
                ResetSpawnPoints();
            }
        }
        int spawnPoint = Random.Range(0, spawnNodesPointsUsed[spawnNode].Count); //Mellan 0-pointsLeft

        //Spawn enemy
        Transform transform;
        transform = spawnNodes[spawnNode][spawnPoint];
        GameObject instance = Instantiate(enemy, transform.position, transform.rotation);
        instance.GetComponent<EnemyController>().SetSpawner(this);
        instance.transform.parent = this.transform;
        enemiesAlive.Add(instance);

        spawnNodesPointsUsed[spawnNode].Remove(spawnPoint);
    }
}
