using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavMeshBuilder : MonoBehaviour
{
    public void BuildNavMesh()
    {
        Debug.Log("BuildingNavmesh");
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }
}
