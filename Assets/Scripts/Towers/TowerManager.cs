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

    public void DeleteTowerOnTile(GameObject aParentTile)
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            //if (myConstructList[i].GetComponent<PlacedConstruct>().myParentTile == aParentTile)
            //{
            //    myConstructList[i].GetComponent<Construct>().Delete();
            //}
        }
    }

    public bool CheckTileClear(GameObject aTargetTile)
    {
        for (int i = 0; i < myTowerList.Count; i++)
        {
            //if (myConstructList[i].GetComponent<PlacedConstruct>().myParentTile == aTargetTile)
            //{
            //    return false;
            //}
        }
        return true;
    }
}
