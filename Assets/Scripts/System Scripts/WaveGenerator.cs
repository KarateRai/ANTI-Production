using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WaveGenerator
{
    static GameObject[] prefabs;
    static GameObject enemyToAdd;
    public static void InitializeGenerator()
    {
        prefabs = Resources.LoadAll("Enemies").Cast<GameObject>().ToArray();

    }
    public static void GenerateWave(int difficulty, ref List<GameObject> waveEnemies)
    {
        for (int i = 0; i < 5; i++)
        {
            float seed = Random.value;
            if (seed > 0.98)
            {
                //Typ av fiende
                enemyToAdd = prefabs[2];
            }
            else if (seed > 0.7)
            {
                //Typ av fiende
                enemyToAdd = prefabs[1];
            }
            else
            {
                //Typ av fiende
                enemyToAdd = prefabs[0];
            }
            enemyToAdd.GetComponent<EnemyController>().IncreaseLevel(difficulty);
            waveEnemies.Add(enemyToAdd);


        }
    }

    public static void GenerateBossWave(int difficulty, float bossWave, ref List<GameObject> waveEnemies)
    {
        float multiplier = bossWave / 100f;
        for (int i = 0; i < 5; i++)
        {
            float seed = Random.value;
            if (seed > 0.91 - multiplier)
            {
                //Boss
                enemyToAdd = prefabs[0];
            }
            else if (seed > 0.7)
            {
                //Runner
                enemyToAdd = prefabs[1];
            }
            else
            {
                //Grunt
                enemyToAdd = prefabs[2];
            }
            enemyToAdd.GetComponent<EnemyController>().IncreaseLevel(difficulty+(int)bossWave);
            waveEnemies.Add(enemyToAdd);
        }
    }
}
