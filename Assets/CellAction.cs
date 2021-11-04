using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAction : MonoBehaviour
{
    public string NodeType;
    public List<GameObject> destinations;
    public List<GameObject> destinationPoints;
    public GameObject nodePoint;


    public void SetDestinationValues(List<GameObject> listofDestinations)
    {
        destinationPoints = new List<GameObject>();
        destinations = new List<GameObject>();

        destinations.AddRange(listofDestinations);
        
        Debug.Log("Im getting Destintations");


        for (int i = 0; i<destinations.Count; i++)
        {
            destinationPoints.Add(destinations[i].GetComponent<CellAction>().nodePoint);
            Debug.Log(destinationPoints[i].name);
        }
        Debug.Log("I have destinations");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AI")
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

    private void OnDestroy()
    {
        Debug.Log("Im commiting suicide!" + gameObject.name);
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
