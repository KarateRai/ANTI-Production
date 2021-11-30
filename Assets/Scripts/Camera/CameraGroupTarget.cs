using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CameraGroupTarget : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;
    private RunTimeGameLogic gameLogic;
    private List<PlayerController> trackedCharacters;
    private void Awake()
    {
        trackedCharacters = new List<PlayerController>();
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    
    private void Update()
    {
        if (gameLogic != null)
        {
            foreach (PlayerController pCon in gameLogic.players)
            {
                if (gameLogic.deadPlayers.Contains(pCon) && trackedCharacters.Contains(pCon)) 
                {
                    trackedCharacters.Remove(pCon);
                    targetGroup.RemoveMember(pCon.transform);
                }
                else if (!trackedCharacters.Contains(pCon) && !gameLogic.deadPlayers.Contains(pCon))
                {
                    trackedCharacters.Add(pCon);
                    targetGroup.AddMember(pCon.transform, 1, 0.5f);
                }
            }
        }
        else
        {
            gameLogic = FindObjectOfType<RunTimeGameLogic>();
        }        
    }
}
