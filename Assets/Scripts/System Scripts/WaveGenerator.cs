using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WaveGenerator
{
    static GameObject[] prefabs;
    ///för varje prefab i enemymap skapa ett spawnable object
    ///
    public static void InitializeGenerator()
    {
        prefabs = Resources.LoadAll("Enemies").Cast<GameObject>().ToArray();
        foreach (GameObject item in prefabs)
        {
            Debug.Log(item.ToString());
        }
    }
    public static List<GameObject> GenerateWave(int difficulty)
    {
        
        List<GameObject> waveEnemies = new List<GameObject>();
        //If wave > 10 ... N
        //Boss > level 10
        //50% chance of creating grunt
    //generatedWave.wavePerSecond = 1;
        //Random indexnr för lista med olika fiender av samma svårighetsgrad
        //TESTAREA
        for (int i = 0; i < 5; i++)
        {
            float seed = Random.value;
            if (seed > 0.9)
            {
                //Typ av fiende
                waveEnemies.Add(prefabs[2]);
            }
            else if (seed > 0.7)
            {
                //Typ av fiende
                waveEnemies.Add(prefabs[1]);
            }
            else
            {
                //Typ av fiende
                waveEnemies.Add(prefabs[0]);
            }
        }
        
        return waveEnemies;
    }
}
