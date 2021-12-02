using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelNavMeshBuilder : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public void BuildNavMesh()
    {
        Debug.Log("BuildingNavmesh");
        //#if UNITY_EDITOR

        //        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();

        //#else

        //#endif
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        //GameManager.instance.meshSurface.BuildNavMesh();
        //GameManager.instance.stageOneN
    }
}
