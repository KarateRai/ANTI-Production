using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerSpawner")]
public class PowerSpawner : ScriptableObject
{
    public GameObject[] powerups;
    public void PowerGenerator(Transform transform)
    {
        float probablility = Random.Range(0f, 101f);
        if (probablility < 10f)
        {
            GameObject go = Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
            go.transform.parent = GameManager.instance.stageParent.transform;
        }
    }

}
