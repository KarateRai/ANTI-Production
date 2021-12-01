using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<GameObject> myTowerList;
    public List<GameObject> myTowerPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //myTowerList = new List<GameObject>();
    }

    void Update()
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<Tower>().shouldBeDeleted)
            {
                Debug.Log("Deleting!");
                Destroy(myTowerList[i]);
                myTowerList.RemoveAt(i);
            }
        }
    }

    public void AddTowerToList(GameObject aTowerToBeAdded)
    {
        myTowerList.Add(aTowerToBeAdded);
    }

    public void DeleteTowerOnCell(GameObject aTargetCell)
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<Tower>().parentCell == aTargetCell)
            {
                myTowerList[i].GetComponent<Tower>().Delete();
            }
        }
    }

    public bool CheckTileClear(GameObject aTargetCell)
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<Tower>().parentCell == aTargetCell)
            {
                return false;
            }
        }
        return true;
    }

    public int CheckNumBuiltTowers(GameObject aPlayerObject)
    {
        int retVal = 0;
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<Tower>().parentPlayer == aPlayerObject)
            {
                retVal++;
            }
        }
        return retVal;
    }
}
