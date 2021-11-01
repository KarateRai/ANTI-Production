using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressingRoom : MonoBehaviour
{
    public GameObject[] lights;
    public bool dressingRoomActive;
    public void LightControl(int id, bool state)
    {
        lights[id]?.SetActive(state);
    }

    public void Begin()
    {
        dressingRoomActive = true;
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            LightControl(i, true);
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
