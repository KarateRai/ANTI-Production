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
    public UnityEvent<Camera> onCameraChange;
    public Camera ActiveCamera { get; private set;}
    [HideInInspector]
    public SceneLoader sceneLoader;
    [HideInInspector]
    public PauseManager pauseManager;
    [HideInInspector]
    public bool allowPause = true;
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
        PlayerManager.instance.noPlayersRemain.AddListener(() => ResetGame());
    }
    
    public void TryToPause(Player player)
    {
        pauseManager.TogglePause(player);
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
            onCameraChange.Invoke(camera);
            Debug.Log("New camera set.");
        }
    }
}
