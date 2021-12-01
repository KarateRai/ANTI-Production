using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PauseManager))]
[RequireComponent(typeof(SceneLoader))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Range(1,10)]
    public int intensity = 1;
    public Camera ActiveCamera { get; private set;}
    [HideInInspector]
    public SceneLoader sceneLoader;
    [HideInInspector]
    public PauseManager pauseManager;
    [HideInInspector]
    public WaveSpawner waveSpawner;
    [HideInInspector]
    public GameObject stageParent;
    [HideInInspector]
    public RunTimeGameLogic gameLogic;
    [HideInInspector]
    public bool allowPause = true;
    //[HideInInspector]
    public int gameDifficulty = 2;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        pauseManager = GetComponent<PauseManager>();
        sceneLoader.Init();
        Cursor.visible = false;
        PlayerManager.instance.noPlayersRemain += ResetGame;
        GlobalEvents.instance.onStageSceneStart += SetGlobalParent;
    }
    private void OnDestroy()
    {
        PlayerManager.instance.noPlayersRemain -= ResetGame;
        GlobalEvents.instance.onStageSceneStart -= SetGlobalParent;
    }
    public void TryToPause(Player player)
    {
        pauseManager.TogglePause(player);
    }
    public void PauseAllowed()
    {
        allowPause = true;
    }
    public void PauseNotAllowed()
    {
        allowPause = false;
    }
    public void ResetGame()
    {
        //todo: reset everything
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
    public void ChangeCameraTo(Camera camera)
    {
        if (ActiveCamera != camera)
        {
            ActiveCamera = camera;
            GlobalEvents.instance.onCameraChange.Invoke(camera);
            //Debug.Log("New camera set.");
        }
    }

    public void SetGlobalParent() 
    {
        stageParent = GameObject.Find("InstantiatedObjects");
    }
}
