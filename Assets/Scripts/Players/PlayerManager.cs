using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerInputManager inputManager;
    public List<Player> players;
    public UnityEvent firstPlayerJoined, noPlayersRemain;
    public bool CanJoin
    {
        get { return inputManager.joiningEnabled; }
        set
        {
            if (value == true) { inputManager.EnableJoining(); }
            else { inputManager.DisableJoining(); }
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            players = new List<Player>();
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (!players.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            Player newPlayer = pi.gameObject.GetComponent<Player>();
            players.Add(newPlayer);
            if (pi.playerIndex == 0) { firstPlayerJoined.Invoke(); }
            Debug.Log("Player ID: " + pi.playerIndex + " has joined.");
        }
    }

    public void HandlePlayerLeft(PlayerInput pi)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].PlayerIndex == pi.playerIndex)
            {
                Player player = players[i];
                players.RemoveAt(i);
                Destroy(player.gameObject);
                if (players.Count <= 0) { noPlayersRemain.Invoke(); }
                return;
            }
        }
    }
}
