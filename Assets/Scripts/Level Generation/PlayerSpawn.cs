using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSpawn : MonoBehaviour
{
    public List<GameObject> playerCharacter = new List<GameObject>();
    public List<GameObject> possiblePlayerSpawnNodes = new List<GameObject>();
    List<PlayerController> activePlayers = new List<PlayerController>();

    public List<PlayerController> SpawnPlayers(List<GameObject> nodes)
    {
        possiblePlayerSpawnNodes.AddRange(nodes);

        for (int i = 0; i < playerCharacter.Count; i++)
        {
            if (PlayerManager.instance.players.Any(p => p.playerIndex == i))
            {
                PlayerManager.instance.players[i].SubscribeTo(playerCharacter[i].GetComponent<ControlledObject>());
                playerCharacter[i].SetActive(true);
                PlayerController pCon = playerCharacter[i].GetComponent<PlayerController>();
                pCon.AssignPlayer(i);
                activePlayers.Add(pCon);
            }
            else
            {
                playerCharacter[i].SetActive(false);
            }
            playerCharacter[i].transform.position = possiblePlayerSpawnNodes[0].GetComponent<CellAction>().spawnPoints[i].position;
        }
        return activePlayers;
    }
}
