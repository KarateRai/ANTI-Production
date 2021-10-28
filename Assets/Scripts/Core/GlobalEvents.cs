using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents instance;
    public UnityEvent<Player> onPlayerJoined;
    public UnityEvent<Player> onPlayerLeft;
    public UnityEvent<Player> onGamePaused;
    public UnityEvent onGameUnpaused;
    public UnityEvent onMenuSceneStart;
    public UnityEvent onMenuSceneEnd;
    public UnityEvent onTeamSceneStart;
    public UnityEvent onTeamSceneEnd;
    public UnityEvent onStageSceneStart;
    public UnityEvent onStageSceneEnd;
    public UnityEvent onGameOver;
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
