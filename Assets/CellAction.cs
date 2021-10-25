using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAction : MonoBehaviour
{
    public string NodeType;
    public List<GameObject> destinations;
    public List<GameObject> destinationPoints;
    public GameObject nodePoint;

    private void Start()
    {
        for (int i = 0; i < destinations.Count; i++)
        {
            destinationPoints.Add(destinations[i].GetComponent<CellAction>().nodePoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AI")
        {
            //other.gameObject.GetComponent<>()
            GameObject fromWhereYouCame = null;

            //other.GetComponent<AI>().Target = SetNewDestination(fromWhereYouCame);

        }
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
