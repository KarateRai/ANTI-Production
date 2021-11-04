using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents instance;
    public UnityAction<Player> onPlayerJoined;
    public UnityAction<Player> onPlayerLeft;
    public UnityAction<Player> onGamePaused;
    public UnityAction<Camera> onCameraChange;
    public UnityAction onGameUnpaused;
    public UnityAction onMenuSceneStart;
    public UnityAction onMenuSceneEnd;
    public UnityAction onTeamSceneStart;
    public UnityAction onTeamSceneEnd;
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
