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
    public GameObject levelManager;


    public void SetDestinationValues(List<GameObject> listofDestinations)
    {

        destinations.AddRange(listofDestinations);
        
        //Debug.Log(gameObject.name);
        for (int i = 0; i < destinations.Count; i++)
        {
            if (destinations[i].GetComponent<CellAction>() != null)
            {
                if (destinations[i].GetComponent<CellAction>().nodePoint != null)
                {
                    destinationPoints.Add(destinations[i].GetComponent<CellAction>().nodePoint);
                }
                else { /*Debug.Log("eeh?");*/ }
            }
            //else
            //{
            //    Debug.Log("Ladies and gentlemen,        We got'm.");
            //}
        }

        if (NodeType == "Objective" || NodeType == "AiSpawnpoint")
        {
            Transform child = transform.Find("Spawnpoints");
            foreach (Transform item in child)
            {
                spawnPoints.Add(item.transform);
            }
        }
    }

    private void TroubleshootingLog(string v)
    {
        Debug.Log("TS LOG: "+v);
    }

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");

        runTimeGameLogic = GameObject.FindGameObjectWithTag("Runtime").GetComponent<RunTimeGameLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "AI")
        {
            if (NodeType == "Objective")
            {
                other.GetComponent<EnemyController>().Die();

                runTimeGameLogic.levelLives--;
                runTimeGameLogic.UpdateCorruption();
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
