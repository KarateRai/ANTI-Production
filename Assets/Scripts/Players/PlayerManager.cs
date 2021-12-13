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
    public UnityAction noPlayersRemain, allPlayersReady;
    public PlayerRole[] playerRoles;
    public Color[] colors;
    public enum InputStates
    {
        INTERFACE,
        GAMEPLAY
    }
    private bool CanJoin
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
    private void Start()
    {
        PlayerInputManager.instance.playerPrefab.SetActive(false);
        GlobalEvents.instance.onTeamSceneStart += JoinOn;
        allPlayersReady += JoinOff;
        allPlayersReady += DisableControls;
        allPlayersReady += CancelSuspendedJoining;
        GlobalEvents.instance.onStageSceneStart += StageSceneStart;
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onTeamSceneStart -= JoinOn;
        allPlayersReady -= JoinOff;
        allPlayersReady -= DisableControls;
        allPlayersReady -= CancelSuspendedJoining;
        GlobalEvents.instance.onStageSceneStart -= StageSceneStart;
    }
    private void StageSceneStart()
    {
        SuspendInput(5f);
    }
    public void DisableControls()
    {
        foreach (Player p in players)
        {
            //Debug.Log("Disabled Player: " + p.playerIndex);
            p.ActivateInput(false);
        }
    }
    public void EnableControls()
    {
        foreach (Player p in players)
        {
            //Debug.Log("Enabled Player: " + p.playerIndex);
            p.ActivateInput(true);
        }
    }
    public void SuspendInput(float duration)
    {
        StartCoroutine(TempSuspendInput(duration));
    }
    IEnumerator TempSuspendInput(float duration)
    {
        DisableControls();
        yield return new WaitForSecondsRealtime(duration);
        EnableControls();
    }
    public void JoinOn()
    {
        CanJoin = true;
        //Debug.Log("Joining: ON");
    }
    public void JoinOff()
    {
        CanJoin = false;
        //Debug.Log("Joining: OFF");
    }
    public void SuspendJoining()
    {
        StartCoroutine(TempSuspendJoining());
    }
    
    IEnumerator TempSuspendJoining()
    {
        JoinOff();
        //Debug.Log("Can't join...");
        yield return new WaitForSecondsRealtime(0.5f);
        JoinOn();
        //Debug.Log("Can join again!");
    }
    private void CancelSuspendedJoining()
    {
        //Debug.Log("Cancel Suspended Joining");
        StopCoroutine(TempSuspendJoining());
    }
    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (!players.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            Player newPlayer = pi.gameObject.GetComponent<Player>();
            newPlayer.playerIndex = pi.playerIndex;
            players.Add(newPlayer);
            if (pi.playerIndex == 0 && GameManager.instance.sceneLoader.activeScene.name == "MenuScene") { JoinOff(); }
            //Debug.Log("Player ID: " + pi.playerIndex + " has joined.");
            GlobalEvents.instance.onPlayerJoined.Invoke(newPlayer);
        }
        else
        {
            Destroy(pi.gameObject);
        }
    }
    public void RemovePlayer(Player player)
    {
        inputManager.playerLeftEvent.Invoke(player.playerInput);
    }
    public void HandlePlayerLeft(PlayerInput pi)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerIndex == pi.playerIndex)
            {
                Player player = players[i];
                players.RemoveAt(i);
                Destroy(player.gameObject);
                if (players.Count <= 0) { noPlayersRemain?.Invoke(); }
                return;
            }
        }
    }
    public void ReadyCheck()
    {
        bool result = true;
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].isReady) { result = false; }
        }
        if (result == true)
        {
            allPlayersReady?.Invoke();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].isReady = false;
            }
        }
    }
    public void SetAllInputMaps(InputStates inputState)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetControlMap(inputState);
        }
    }
    public Player GetPlayerByID(int pID)
    {
        foreach (Player player in players)
        {
            if (player.playerIndex == pID)
            {
                return player;
            }
        }
        return null;
    }

    public PlayerRole GetPlayerRole(PlayerChoices.RoleChoice roleChoice)
    {
        switch (roleChoice)
        {
            case PlayerChoices.RoleChoice.DAMAGE:
                return playerRoles[0];
            case PlayerChoices.RoleChoice.HEALER:
                return playerRoles[1];
            case PlayerChoices.RoleChoice.TANK:
                return playerRoles[2];
            default:
                return playerRoles[0];
        }
    }

    public Color GetColor(PlayerChoices.OutfitChoice outfitChoice)
    {
        switch (outfitChoice)
        {
            case PlayerChoices.OutfitChoice.BLUE:
                return colors[0];
            case PlayerChoices.OutfitChoice.GREEN:
                return colors[1];
            case PlayerChoices.OutfitChoice.YELLOW:
                return colors[2];
            case PlayerChoices.OutfitChoice.ORANGE:
                return colors[3];
            case PlayerChoices.OutfitChoice.RED:
                return colors[4];
            case PlayerChoices.OutfitChoice.PURPLE:
                return colors[5];
            default:
                return colors[0];
        }
    }
}
