using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelNavMeshBuilder : MonoBehaviour
{
    public NavMeshSurface[] navMeshSurfaces;
    public void BuildNavMesh()
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
