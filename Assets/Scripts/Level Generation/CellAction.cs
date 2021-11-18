using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAction : MonoBehaviour
{
    public string NodeType;
    public List<GameObject> destinations = new List<GameObject>();
    public List<GameObject> destinationPoints = new List<GameObject>();
    public List<Transform>spawnPoints = new List<Transform>();

    public GameObject nodePoint;

    RunTimeGameLogic runTimeGameLogic;
    GameObject levelManager;


    public void SetDestinationValues(List<GameObject> listofDestinations)
    {

        destinations.AddRange(listofDestinations);
        
        Debug.Log(gameObject.name);
        for (int i = 0; i<destinations.Count; i++)
        {
            destinationPoints.Add(destinations[i].GetComponent<CellAction>().nodePoint);
        }

        if (NodeType == "Spawnpoint || Ai-Spawnpoint")
        {
            Transform child = transform.Find("SpawnPoints");
            foreach (Transform item in child)
            {
                spawnPoints.Add(item.transform);
            }
        }
    }

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager.GetComponent<RunTimeGameLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "AI")
        {
            if (NodeType == "Objective")
            {
                runTimeGameLogic.levelLives--;
            }
            else
            {
                GameObject fromWhereYouCame = null;
                if (other.GetComponent<EnemyController>().fromObjPosition == null)
                {
                    other.GetComponent<EnemyController>().fromObjPosition = nodePoint;
                }
                fromWhereYouCame = other.GetComponent<EnemyController>().fromObjPosition;
                other.GetComponent<EnemyController>().toObjPosition = SetNewDestination(fromWhereYouCame);
            }

        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroying: " + gameObject.name);
    }

    GameObject SetNewDestination(GameObject origin)
    {

        List<GameObject> possibleDestinationPoints = new List<GameObject>();
        possibleDestinationPoints.Clear();
        possibleDestinationPoints.AddRange(destinationPoints);

        possibleDestinationPoints.Remove(origin);

        GameObject newAIDestination;

        if (possibleDestinationPoints.Count <= 1)
        {
            newAIDestination = destinations[0];
            return newAIDestination;
        }
        else if (possibleDestinationPoints.Count > 1)
        {
            newAIDestination = possibleDestinationPoints[Random.Range(0, possibleDestinationPoints.Count)];
            return newAIDestination;
        }
        return null;
    }
}
