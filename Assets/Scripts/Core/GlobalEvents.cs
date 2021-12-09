using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents instance;
    public UnityAction<Camera> onCameraChange;
    public UnityAction<Player> onPlayerJoined;
    public UnityAction<Player> onPlayerLeft;
    public UnityAction<Player> onGamePausedByPlayer;
    public UnityAction<Player> onPlayerDeath;
    public UnityAction<Player> onPlayerRespawn;
    public UnityAction onGamePaused;
    public UnityAction onGameUnpaused;
    public UnityAction onMenuSceneStart;
    public UnityAction onMenuSceneEnd;
    public UnityAction onTeamSceneStart;
    public UnityAction onTeamSceneEnd;
    public UnityAction onStageSettingsSceneStart;
    public UnityAction onStageSettingsSceneEnd;
    public UnityAction onStageSceneStart;
    public UnityAction onStageSceneEnd;
    public UnityAction onGameOver;
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
        }
    }
}
