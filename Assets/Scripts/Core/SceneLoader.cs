using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _onLoadingScreen;
    
    private List<string> _currentlyLoading;
    private List<string> _currentlyUnloading;
    private ScreenFader _screenFader;
    public Scene activeScene;
    public bool firstTime = true;
    private bool _isGeneratingLevel;
    private void Awake()
    {
        _currentlyLoading = new List<string>();
        _currentlyUnloading = new List<string>();
    }

    private void Start()
    {
        _screenFader = GUIManager.instance.screenFader;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        _screenFader.onFadeOutStarted += OnFadeOutStarted;
        _screenFader.onFadeOutComplete += OnFadeOutComplete;
        _screenFader.onFadeInStarted += OnFadeInStarted;
        _screenFader.onFadeInComplete += OnFadeInComplete;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        _screenFader.onFadeOutStarted -=OnFadeOutStarted;
        _screenFader.onFadeOutComplete-= OnFadeOutComplete;
        _screenFader.onFadeInStarted -= OnFadeInStarted;
        _screenFader.onFadeInComplete-= OnFadeInComplete;
    }
    public void Init()
    {
        GoToScene("MenuScene");
    }
    private void OnFadeInStarted()
    {
        _onLoadingScreen = false;
    }
    private void OnFadeInComplete()
    {
        if (firstTime)
        {
            GUIManager.instance.startText.Enable();
            PlayerManager.instance.CanJoin = true;
            firstTime = false;
        }
    }
    private void OnFadeOutStarted()
    {

    }
    private void OnFadeOutComplete()
    {
        _onLoadingScreen = true;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentlyLoading.Remove(scene.name);
        StartCoroutine(SceneLoaded(scene, mode));
    }
    IEnumerator SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        yield return new WaitWhile(() => LoadingStatus());
        yield return new WaitForSecondsRealtime(0.2f);
        GUIManager.instance.screenFader.FadeIn(1);
        activeScene = scene;
        FindCamera();
        switch (scene.name)
        {
            case "MenuScene":
                GlobalEvents.instance.onMenuSceneStart?.Invoke();
                break;
            case "TeamScene":
                GlobalEvents.instance.onTeamSceneStart?.Invoke();
                break;
            case "StageOne":
                GlobalEvents.instance.onStageSceneStart?.Invoke();
                break;
        }
    }

    private bool LoadingStatus()
    {
        if (_currentlyLoading.Count == 0 && _currentlyUnloading.Count == 0 /*&& _isGeneratingLevel == false*/)
            return false;
        else
            return true;
    }
    private void OnSceneUnloaded(Scene scene)
    {
        _currentlyUnloading.Remove(scene.name);
        switch (scene.name)
        {
            case "MenuScene":
                GlobalEvents.instance.onMenuSceneEnd?.Invoke();
                break;
            case "TeamScene":
                GlobalEvents.instance.onTeamSceneEnd?.Invoke();
                break;
            case "StageOne":
                GlobalEvents.instance.onStageSceneEnd?.Invoke();
                break;
        }
    }
    public void GoToScene(string sceneName)
    {
        if (activeScene != null && activeScene.isLoaded) 
        {
            UnloadScene(activeScene.name);
        }
        LoadScene(sceneName);
    }
    private void LoadScene(string sceneName)
    {
        //Debug.Log("Loading: " + sceneName);
        //if (_screenFader.fadeState != ScreenFader.FadeState.ON_BLACK)
        //{
        GUIManager.instance.screenFader.FadeOut(1);
        //}
        if (SceneManager.GetSceneByName(sceneName).isLoaded || _currentlyLoading.Contains(sceneName))
        {
            Debug.Log("Tried to load " + sceneName + " twice.");
            return;
        }
        _currentlyLoading.Add(sceneName);
        if (sceneName == "StageOne") { LevelGenerationStarted(); }
        StartCoroutine(FadeOutToLoad(sceneName));
    }
    private void UnloadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded || _currentlyUnloading.Contains(sceneName))
        {
            Debug.Log("Tried to unload " + sceneName + " twice.");
            return;
        }
        //Debug.Log("Unloading: "+sceneName);
        _currentlyUnloading.Add(sceneName);
        StartCoroutine(UnloadWhenReady(sceneName));
    }
    IEnumerator FadeOutToLoad(string sceneName)
    {
        yield return new WaitUntil(() => _onLoadingScreen);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
    IEnumerator UnloadWhenReady(string sceneName)
    {
        yield return new WaitUntil(() => _onLoadingScreen);

        SceneManager.UnloadSceneAsync(sceneName);
    }
    private void FindCamera()
    {
        Camera newCam = FindObjectOfType<Camera>();
        if (newCam != null)
        {
            GameManager.instance.ChangeCameraTo(newCam);
        }
    }
    private void LevelGenerationStarted()
    {
        _isGeneratingLevel = true;
    }
    public void OnLevelGenerated()
    {
        _isGeneratingLevel = false;
    }

}
