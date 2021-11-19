using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavMeshBuilder : MonoBehaviour
{
    public void BuildNavMesh()
    {
        Debug.Log("BuildingNavmesh");
#if UNITY_EDITOR

        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

#endif

    }
}
