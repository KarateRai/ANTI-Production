using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    //public List<GameObject> waveEnemies = new List<GameObject>();
    public int waveCount;
    public float wavePerSecond;
}
public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> waveEnemies = new List<GameObject>();
    public List<GameObject> enemiesAlive = new List<GameObject>();

    private bool waveOut = false;
    public static int EnemiesAlive = 0;
    int waveNumber = 1;
    private List<List<Transform>> spawnNodes = new List<List<Transform>>();
    private List<List<int>> spawnNodesPointsUsed =  new List<List<int>>();
    //public GameObject spawnEffect;

    public float timeBetweenWaves = 1f;
    private float countdown = 2f;
    //[SerializeField] public Text waveCountdownText;


    private void Start()
    {
        WaveGenerator.InitializeGenerator();
    }

    public void StartWaves(List<GameObject> aiSpawnNodes)
    {
        waveEnemies = WaveGenerator.GenerateWave(waveNumber);
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
    }

    void Update()
    {
        //Old, might use, might not
        if (enemiesAlive.Count > 0)
        {
            return;
        }

        #region Countdown Timer
       
        if (countdown > 0 && countdown < 0.99)
        {
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            //waveCountdownText.text = string.Format("{0:0.00}", countdown);
        }
        else if (countdown >= 0 && !waveOut)
        {
            //waveCountdownText.text = Mathf.FloorToInt(countdown + 1).ToString();
        }
        else if (countdown >= -1)
        {
            //waveCountdownText.text = "Wave " + (waveIndex + 1);
        }
        else if (countdown <= -2)
        {
            ResetSpawnPoints();
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
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
        waveOut = true;
        //GameObject effect = (GameObject)Instantiate(spawnEffect, spawnPoint.transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(0.4f);
        
        EnemiesAlive = waveEnemies.Count;
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

        waveOut = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnNode = Random.Range(0, spawnNodesPointsUsed.Count); //Mellan 0-pointsLeft
        int spawnPoint = Random.Range(0, spawnNodesPointsUsed[spawnNode].Count); //Mellan 0-pointsLeft

        //Spawn enemy
        Transform transform;
        transform = spawnNodes[spawnNode][spawnPoint];
        GameObject instance = Instantiate(enemy, transform.position, transform.rotation);
        //Set random destination? atm only uses 0
        //Debug.Log(this.GetComponent<CellAction>().destinations);
        //instance.GetComponent<EnemyController>().toObjPosition = this.GetComponent<CellAction>().destinations[0];
        enemiesAlive.Add(instance);

        spawnNodesPointsUsed[spawnNode].Remove(spawnPoint);
        if (spawnNodesPointsUsed[spawnNode].Count == 0)
        {
            spawnNodesPointsUsed.Remove(spawnNodesPointsUsed[spawnNode]);
            if (spawnNodesPointsUsed.Count == 0)
            {
                ResetSpawnPoints();
            }
        }
    }
}
