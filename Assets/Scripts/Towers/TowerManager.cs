using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    List<GameObject> myTowerList;

    // Start is called before the first frame update
    void Start()
    {
        myTowerList = new List<GameObject>();
    }

    void Update()
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<GunTower>().myShouldBeDeleted)
            {
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
            if (myTowerList[i].GetComponent<GunTower>().myParentCell == aTargetCell)
            {
                myTowerList[i].GetComponent<GunTower>().Delete();
            }
        }
    }

    public bool CheckTileClear(GameObject aTargetCell)
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            if (myTowerList[i].GetComponent<GunTower>().myParentCell == aTargetCell)
            {
                return false;
            }
        }
        return true;
    }
}
