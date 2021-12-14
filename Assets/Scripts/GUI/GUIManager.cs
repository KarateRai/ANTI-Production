using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    [Header("Menus")]
    //public MenuManager menuManager;
    public MenuController startMenu, pauseMenu, settingsMenu, creditsMenu, stageSettingsMenu;
    public MenuController[] teamMenus;
    public TeamPanel[] teamPanels;
    [Header("HUD")]
    public HUDManager playerHUD;
    public CanvasGroup trackedObjectsGroup;
    [Header("Screen Effects")]
    public CanvasGroup loadingScreen;
    public CanvasGroup blurredBG;
    public CanvasGroup gameOverScreenBG;
    public CanvasGroup titleGraphic;
    public GUITween gameOverText;
    public TMP_Text gameOverScoreText;
    public MessageToast messageToast;
    public Canvas blurredScreen;
    public ScreenFader screenFader;
    public GUITween startText;
    public GameObject floatingCombatTextPrefab;
    private List<Menus> _openMenus;
    [HideInInspector]
    public DressingRoom dressingRoom;
    private Animator menuCamAnimator;
    public Sprite buttonIconEast, buttonIconSouth, buttonIconWest, buttonIconNorth;
    public enum Menus
    {
        NO_MENU,
        START_MENU,
        PAUSE_MENU,
        SETTINGS_MENU,
        TEAM_MENU_1,
        TEAM_MENU_2,
        TEAM_MENU_3,
        TEAM_MENU_4,
        CREDITS_MENU,
        STAGESETTINGS_MENU
    }
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
    private void Start()
    {
        _openMenus = new List<Menus>();
        screenFader.onFadeInStarted += LoadingScreenOff;
        screenFader.onFadeOutComplete += LoadingScreenOn;
        GlobalEvents.instance.onPlayerJoined += player => PlayerJoined(player);
        GlobalEvents.instance.onMenuSceneStart += MenuSceneStart;
        GlobalEvents.instance.onTeamSceneStart += TeamSceneStart;
        GlobalEvents.instance.onStageSceneStart += StageSceneStart;
        GlobalEvents.instance.onStageSettingsSceneStart += StageSettingsSceneStart;
        GlobalEvents.instance.onGamePausedByPlayer += player => Pause(player);
        GlobalEvents.instance.onGameUnpaused += UnPause;
        GlobalEvents.instance.onCameraChange += cam => OnNewCamera(cam);
        GlobalEvents.instance.onGameOver += GameOverOn;
        GlobalEvents.instance.onStageSceneEnd += GameOverOff;
        GlobalEvents.instance.onPlayerDeath += player => OnPlayerDeath(player);
        GlobalEvents.instance.onPlayerRespawn += player => OnPlayerRespawn(player);
        PlayerManager.instance.allPlayersReady += PlayersReady;
        startText.onEnableComplete += PlayerManager.instance.JoinOn;
        pauseMenu.tween.onEnableStarted += GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onDisableStarted += GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onEnableComplete += GameManager.instance.PauseAllowed;
        pauseMenu.tween.onDisableComplete += GameManager.instance.PauseAllowed;
        pauseMenu.tween.onEnableComplete += BlurOn;
        pauseMenu.tween.onDisableStarted += BlurOff;
    }
    private void OnDestroy()
    {
        screenFader.onFadeInStarted -= LoadingScreenOff;
        screenFader.onFadeOutComplete -= LoadingScreenOn;
        GlobalEvents.instance.onPlayerJoined -= player => PlayerJoined(player);
        GlobalEvents.instance.onMenuSceneStart -= MenuSceneStart;
        GlobalEvents.instance.onTeamSceneStart -= TeamSceneStart;
        GlobalEvents.instance.onStageSceneStart -= StageSceneStart;
        GlobalEvents.instance.onStageSettingsSceneStart -= StageSettingsSceneStart;
        GlobalEvents.instance.onGamePausedByPlayer -= player => Pause(player);
        GlobalEvents.instance.onGameUnpaused -= UnPause;
        GlobalEvents.instance.onCameraChange -= cam => OnNewCamera(cam);
        GlobalEvents.instance.onGameOver -= GameOverOn;
        GlobalEvents.instance.onStageSceneEnd -= GameOverOff;
        GlobalEvents.instance.onPlayerDeath -= player => OnPlayerDeath(player);
        GlobalEvents.instance.onPlayerRespawn -= player => OnPlayerRespawn(player);
        PlayerManager.instance.allPlayersReady -= PlayersReady;
        startText.onEnableComplete -= PlayerManager.instance.JoinOn;
        pauseMenu.tween.onEnableStarted -= GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onDisableStarted -= GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onEnableComplete -= GameManager.instance.PauseAllowed;
        pauseMenu.tween.onDisableComplete -= GameManager.instance.PauseAllowed;
        pauseMenu.tween.onEnableComplete -= BlurOn;
        pauseMenu.tween.onDisableStarted -= BlurOff;
    }

    private void UnPause()
    {
        PlayerManager.instance.SetAllInputMaps(PlayerManager.InputStates.GAMEPLAY);
        trackedObjectsGroup.alpha = 1f;
        CloseMenu("PAUSE_MENU");
    }

    private void Pause(Player player)
    {
        PlayerManager.instance.SetAllInputMaps(PlayerManager.InputStates.INTERFACE);
        pauseMenu.AssignNoSelect(player);
        GlobalEvents.instance.onGamePaused?.Invoke();
        trackedObjectsGroup.alpha = 0f;
        OpenMenu("PAUSE_MENU");
    }
    private void OnPlayerDeath(Player player)
    {
        playerHUD.playerHUDs[player.playerIndex].SetDisplayGroup(PlayerHUD.DisplayGroups.DEAD);
    }
    private void OnPlayerRespawn(Player player)
    {
        playerHUD.playerHUDs[player.playerIndex].SetDisplayGroup(PlayerHUD.DisplayGroups.DEFAULT);
    }

    private void StageSceneStart()
    {
        trackedObjectsGroup.alpha = 0;
        StartCoroutine(TurnOnTrackedObjects());
    }
    IEnumerator TurnOnTrackedObjects()
    {
        yield return new WaitForSeconds(5);
        trackedObjectsGroup.alpha = 1;
    }
    public void OnPauseQuit()
    {
        PlayerManager.instance.DisableControls();
    }
    private void GameOverOn()
    {
        PlayerManager.instance.DisableControls();
        LeanTween.alphaCanvas(gameOverScreenBG, 1, 0.3f).setIgnoreTimeScale(true);
        gameOverText.Enable();
        trackedObjectsGroup.alpha = 0f;
        int wcleared = 0;
        if (GameManager.instance.waveSpawner != null)
            wcleared = GameManager.instance.waveSpawner.waveNumber; //Set minus one? Since the wave we are one we have not cleared.
        gameOverScoreText.text = "Waves cleared: " + wcleared;
    }

    private void GameOverOff()
    {
        PlayerManager.instance.EnableControls();
        LeanTween.alphaCanvas(gameOverScreenBG, 0, 0.3f).setIgnoreTimeScale(true);
        trackedObjectsGroup.alpha = 1f;
        gameOverText.Disable();
    }

    private void BlurOn()
    {
        LeanTween.alphaCanvas(blurredBG, 1, 0f).setIgnoreTimeScale(true);
    }
    private void BlurOff()
    {
        LeanTween.alphaCanvas(blurredBG, 0, 0f).setIgnoreTimeScale(true);
    }
    public void TitleOn()
    {
        LeanTween.alphaCanvas(titleGraphic, 1, 1f).setIgnoreTimeScale(true);
    }
    public void TitleOff()
    {
        LeanTween.alphaCanvas(titleGraphic, 0, 0.5f).setIgnoreTimeScale(true);
    }
    private void OnNewCamera(Camera camera)
    {
        blurredScreen.worldCamera = camera;
        blurredScreen.planeDistance = 1;
    }
    private void PlayerJoined(Player player)
    {
        if (player.playerIndex == 0 && GameManager.instance.sceneLoader.activeScene.name == "MenuScene")
        {
            OpenMenu("START_MENU");
            startText.Disable();
        }
        if (GameManager.instance.sceneLoader.activeScene.name == "TeamScene")
        {
            teamPanels[player.playerIndex].SetChoices(player.playerChoices);
            OpenImmediate(GetMenuEnumByController(teamMenus[player.playerIndex]));
        }
    }
    private void MenuSceneStart()
    {
        if (!GameManager.instance.sceneLoader.firstTime)
        {
            OpenMenu("START_MENU");
        }
        menuCamAnimator = GameObject.Find("StateDrivenCamera").GetComponent<Animator>();
        BlurOn();
    }
    private void TeamSceneStart()
    {
        foreach (Player p in PlayerManager.instance.players)
        {
            teamPanels[p.playerIndex].SetChoices(PlayerManager.instance.players[p.playerIndex].playerChoices);
            OpenImmediate(GetMenuEnumByController(teamMenus[p.playerIndex]));
        }
        BlurOff();
    }
    private void StageSettingsSceneStart()
    {
        OpenMenu("STAGESETTINGS_MENU");
    }
    public void LoadingScreenOn()
    {
        loadingScreen.alpha = 1;
    }
    public void LoadingScreenOff()
    {
        loadingScreen.alpha = 0;
    }
    public MenuController GetMenuByEnum(Menus menu) 
    {
        switch (menu)
        {
            case Menus.START_MENU:
                return startMenu;
            case Menus.SETTINGS_MENU:
                return settingsMenu;
            case Menus.PAUSE_MENU:
                return pauseMenu;
            case Menus.TEAM_MENU_1:
                return teamMenus[0];
            case Menus.TEAM_MENU_2:
                return teamMenus[1];
            case Menus.TEAM_MENU_3:
                return teamMenus[2];
            case Menus.TEAM_MENU_4:
                return teamMenus[3];
            case Menus.CREDITS_MENU:
                return creditsMenu;
            case Menus.STAGESETTINGS_MENU:
                return stageSettingsMenu;
            default:
                return null;
        }
    }
    
    private Menus GetMenuEnumByName(string menuName)
    {
        switch (menuName)
        {
            case "START_MENU":
                return Menus.START_MENU;
            case "SETTINGS_MENU":
                return Menus.SETTINGS_MENU;
            case "PAUSE_MENU":
                return Menus.PAUSE_MENU;
            case "TEAM_MENU_1":
                return Menus.TEAM_MENU_1;
            case "TEAM_MENU_2":
                return Menus.TEAM_MENU_2;
            case "TEAM_MENU_3":
                return Menus.TEAM_MENU_3;
            case "TEAM_MENU_4":
                return Menus.TEAM_MENU_4;
            case "CREDITS_MENU":
                return Menus.CREDITS_MENU;
            case "STAGESETTINGS_MENU":
                return Menus.STAGESETTINGS_MENU;
            default:
                return Menus.NO_MENU;
        }
    }
    private string GetMenuNameByEnum(Menus menu)
    {
        switch (menu)
        {
            case Menus.START_MENU:
                return "START_MENU";
            case Menus.SETTINGS_MENU:
                return "SETTINGS_MENU";
            case Menus.PAUSE_MENU:
                return "PAUSE_MENU";
            case Menus.TEAM_MENU_1:
                return "TEAM_MENU_1";
            case Menus.TEAM_MENU_2:
                return "TEAM_MENU_2";
            case Menus.TEAM_MENU_3:
                return "TEAM_MENU_3";
            case Menus.TEAM_MENU_4:
                return "TEAM_MENU_4";
            case Menus.CREDITS_MENU:
                return "CREDITS_MENU";
            case Menus.STAGESETTINGS_MENU:
                return "STAGESETTINGS_MENU";
            default:
                return "NO_MENU";
        }
    }

    private Menus GetMenuEnumByController(MenuController controller)
    {
        if (controller == startMenu) { return Menus.START_MENU; }
        else if (controller == settingsMenu) { return Menus.SETTINGS_MENU; }
        else if (controller == pauseMenu) { return Menus.PAUSE_MENU; }
        else if (controller == teamMenus[0]) { return Menus.TEAM_MENU_1; }
        else if (controller == teamMenus[1]) { return Menus.TEAM_MENU_2; }
        else if (controller == teamMenus[2]) { return Menus.TEAM_MENU_3; }
        else if (controller == teamMenus[3]) { return Menus.TEAM_MENU_4; }
        else if (controller == creditsMenu) { return Menus.CREDITS_MENU; }
        else if (controller == stageSettingsMenu) { return Menus.STAGESETTINGS_MENU; }
        else { return Menus.NO_MENU; }
    }
    public void OpenMenu(string menuName)
    {
        Menus menu = GetMenuEnumByName(menuName);
        switch (menu)
        {
            case Menus.START_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                if (menuCamAnimator != null)
                    menuCamAnimator?.Play("INIT_CAM");
                break;
            case Menus.SETTINGS_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                menuCamAnimator?.Play("SETTINGS_CAM");
                break;
            case Menus.PAUSE_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                break;
            case Menus.TEAM_MENU_1:
                OpenImmediate(menu);
                break;
            case Menus.TEAM_MENU_2:
                OpenImmediate(menu);
                break;
            case Menus.TEAM_MENU_3:
                OpenImmediate(menu);
                break;
            case Menus.TEAM_MENU_4:
                OpenImmediate(menu);
                break;
            case Menus.CREDITS_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                menuCamAnimator?.Play("CREDITS_CAM");
                break;
            case Menus.STAGESETTINGS_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                break;
        }

    }
    private void OpenImmediate(Menus menu)
    {
        _openMenus.Add(menu);
        GetMenuByEnum(menu)?.OpenMenu();
    }

    IEnumerator DelayedOpenMenu(Menus menu)
    {
        yield return new WaitUntil(() => CanOpen());
        _openMenus.Add(menu);
        
        GetMenuByEnum(menu)?.OpenMenu();
    }

    private bool CanOpen()
    {
        //add more conditions if needed
        return _openMenus.Count == 0;
    }

    public void CloseMenu(string menuName)
    {
        Menus menu = GetMenuEnumByName(menuName);
        //StartCoroutine(MenuClosed(menu, GetMenuByEnum(menu).tween.disableDuration));
        
        GetMenuByEnum(menu)?.CloseMenu();
    }

    public void MenuClosed(Menus menu)
    {
        if (menu == Menus.PAUSE_MENU)
        {
            GameManager.instance.pauseManager.UnPause();
        }
        _openMenus.Remove(menu);
    }
    private void PlayersReady()
    {
        for (int i = _openMenus.Count-1; i > -1; i--)
        {
            CloseMenu(GetMenuNameByEnum(_openMenus[i]));
        }
        ChangeToScene("StageSettingsScene");
    }

    public void ChangeToScene(string sceneName)
    {
        GameManager.instance.sceneLoader.GoToScene(sceneName);
    }

    public void NewFloatingCombatText(int _value, bool _isDamage, Vector3 _impactPos, bool _isHostile)
    {
        if (GameManager.instance.ActiveCamera != null)
        {
            FloatingCombatText newFloatingText = Instantiate(floatingCombatTextPrefab, trackedObjectsGroup.transform).GetComponent<FloatingCombatText>();
            newFloatingText.SetValues(_value, _isDamage, _impactPos, _isHostile);
        }
    }
}
