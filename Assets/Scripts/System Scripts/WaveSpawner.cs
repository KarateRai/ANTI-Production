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
    public int waveNumber = 0;
    float bossWave = 1;
    private List<List<Transform>> spawnNodes = new List<List<Transform>>();
    private List<List<int>> spawnNodesPointsUsed =  new List<List<int>>();
    //public GameObject spawnEffect;

    private float timeBetweenWaves = 8f;
    private float countdown = 10f;
    private float cameraDelay = 2f;
    //[SerializeField] public Text waveCountdownText;


    private void Start()
    {
        GameManager.instance.waveSpawner = this;
        WaveGenerator.InitializeGenerator();
        //Temp fix. Not sure why, but waveNumber is set to 1 on start.
        waveNumber = 0;
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

        if (enemiesAlive.Count == 0 && GameManager.instance.gameLogic.levelLives > 0)
        {
            GUIManager.instance.messageToast.NewMessage("Wave cleared!");
            GlobalEvents.instance.onWaveCleared?.Invoke();
        }
            
    }

    void Update()
    {
        if (enemiesAlive.Count > 0)
        {
            betweenWaves = false;
            return;
        }
        else            
            betweenWaves = true;

        #region Countdown Timer
        if (countdown <= 0)
        {
            countdown = timeBetweenWaves;
            ResetSpawnPoints();
            StartCoroutine(SpawnWave());
            return;
        }
        else
            countdown -= Time.deltaTime;
        #endregion
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
        waveNumber++;
        if (waveNumber % 10 == 0)
            WaveGenerator.GenerateBossWave(waveNumber, bossWave, ref waveEnemies);
        else
            WaveGenerator.GenerateWave(waveNumber, ref waveEnemies);
        GUIManager.instance.messageToast.NewMessage("Wave " + waveNumber);
        GameManager.instance.cameraDirector?.ChangeShotTo(CameraDirector.CameraShots.ENEMY_CAM);
        GameManager.instance.cameraDirector?.DelayedChangeShotTo(CameraDirector.CameraShots.GAMEPLAY_CAM, 3);
        yield return new WaitForSeconds(cameraDelay);

        for (int j = 0; j < waveEnemies.Count; j++)
        {
            SpawnEnemy(waveEnemies[j], j);
            if (j == Random.Range(3,6))
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void SpawnEnemy(GameObject enemy, int index)
    {
        int spawnNode = Random.Range(0, spawnNodesPointsUsed.Count); //Mellan 0-pointsLeft
        
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
        //Effect spawn
        GameObject effect = (GameObject)Instantiate(WaveGenerator.GetEffect(), transform.position, Quaternion.identity);
        effect.transform.parent = this.transform;
        //Enemy spawn
        GameObject instance = Instantiate(enemy, transform.position, transform.rotation);
        EnemyController eC = instance.GetComponent<EnemyController>();
        eC.SetSpawner(this);
        instance.name = instance.name + index;
        instance.transform.parent = this.transform;
        enemiesAlive.Add(instance);

        Destroy(effect, 1f);

        spawnNodesPointsUsed[spawnNode].Remove(spawnPoint);
    }
}
