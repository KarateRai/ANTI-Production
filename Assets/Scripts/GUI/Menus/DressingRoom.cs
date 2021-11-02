using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressingRoom : MonoBehaviour
{
    public GameObject[] lights;
    public bool dressingRoomActive;

    private void Awake()
    {
        GlobalEvents.instance.onTeamSceneStart += Begin;
        GlobalEvents.instance.onTeamSceneEnd += End;
        GUIManager.instance.dressingRoom = this;
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onTeamSceneStart -= Begin;
        GlobalEvents.instance.onTeamSceneEnd -= End;
    }
    public void LightControl(int id, bool state)
    {
        lights[id]?.SetActive(state);
    }

    public void Begin()
    {
        dressingRoomActive = true;
        foreach (Player p in PlayerManager.instance.players)
        {
            LightControl(p.playerIndex, true);
        }
    }
    public void End()
    {
        dressingRoomActive = false;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i]?.SetActive(false);
        }
    }
}
