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
    private List<Dictionary<bool, Transform>> spawnNodes;
    //public GameObject spawnEffect;

    public float timeBetweenWaves = 1f;
    private float countdown = 2f;
    //[SerializeField] public Text waveCountdownText;

    private int waveIndex = 0;

    private void Start()
    {
        WaveGenerator.InitializeGenerator();
    }

    public void StartWaves(List<GameObject> spawnNodes)
    {
        waveEnemies = WaveGenerator.GenerateWave(waveNumber);
        for (int i = 0; i < spawnNodes.Count; i++)
        {
            this.spawnNodes[i] = spawnNodes[i].GetComponent<CellAction>().spawnPoint;
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
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        #endregion

        countdown -= Time.deltaTime;
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
        Dictionary<bool, Transform> nodeDict = spawnNodes[Random.Range(0, spawnNodes.Count)].GetComponent<CellAction>().spawnPoints;
        int spawnNode = Random.Range(0, spawnNodes.Count);
        Transform transform;

        if (!spawnNodes[spawnNode].TryGetValue(true, out transform))
        {
            //Out of spawnpoints
            return;
        }
        GameObject instance = Instantiate(enemy, transform.position, transform.rotation);
        instance.GetComponent<EnemyController>().toObjPosition = this.GetComponent<CellAction>().destinations[0];
        enemiesAlive.Add(instance);
    }
}
