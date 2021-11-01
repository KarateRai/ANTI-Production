using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    [Header("Menus")]
    //public MenuManager menuManager;
    public MenuController startMenu, pauseMenu, settingsMenu;
    public MenuController[] teamMenus;
    public TeamPanel[] teamPanels;
    [Header("HUD")]
    public CanvasGroup playerHUD;
    [Header("Screen Effects")]
    public CanvasGroup loadingScreen;
    public ScreenFader screenFader;
    public GUITween startText;
    private List<Menus> _openMenus;
    public enum Menus
    {
        NO_MENU,
        START_MENU,
        PAUSE_MENU,
        SETTINGS_MENU,
        TEAM_MENU_1,
        TEAM_MENU_2,
        TEAM_MENU_3,
        TEAM_MENU_4
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
        GlobalEvents.instance.onGamePaused += player => Pause(player);
        GlobalEvents.instance.onGameUnpaused += UnPause;
        PlayerManager.instance.allPlayersReady += PlayersReady;
        pauseMenu.tween.onEnableStarted += GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onDisableStarted += GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onEnableComplete += GameManager.instance.PauseAllowed;
        pauseMenu.tween.onDisableComplete += GameManager.instance.PauseAllowed;
    }
    private void OnDestroy()
    {
        screenFader.onFadeInStarted -= LoadingScreenOff;
        screenFader.onFadeOutComplete -= LoadingScreenOn;
        GlobalEvents.instance.onPlayerJoined -= player => PlayerJoined(player);
        GlobalEvents.instance.onMenuSceneStart -= MenuSceneStart;
        GlobalEvents.instance.onTeamSceneStart -= TeamSceneStart;
        GlobalEvents.instance.onGamePaused -= player => Pause(player);
        GlobalEvents.instance.onGameUnpaused -= UnPause;
        PlayerManager.instance.allPlayersReady -= PlayersReady;
        pauseMenu.tween.onEnableStarted -= GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onDisableStarted -= GameManager.instance.PauseNotAllowed;
        pauseMenu.tween.onEnableComplete -= GameManager.instance.PauseAllowed;
        pauseMenu.tween.onDisableComplete -= GameManager.instance.PauseAllowed;
    }

    private void UnPause()
    {
        CloseMenu("PAUSE_MENU");
    }

    private void Pause(Player player)
    {
        pauseMenu.AssignNoSelect(player);
        OpenMenu("PAUSE_MENU");
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
    }
    private void TeamSceneStart()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            teamPanels[i].SetChoices(PlayerManager.instance.players[i].playerChoices);
            OpenImmediate(GetMenuEnumByController(teamMenus[i]));
        }
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
            default:
                return Menus.NO_MENU;
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
        else { return Menus.NO_MENU; }
    }
    public void OpenMenu(string menuName)
    {
        Menus menu = GetMenuEnumByName(menuName);
        switch (menu)
        {
            case Menus.START_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
                break;
            case Menus.SETTINGS_MENU:
                StartCoroutine(DelayedOpenMenu(menu));
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
        if( menu == Menus.PAUSE_MENU) { GameManager.instance.pauseManager.UnPause(); }
        _openMenus.Remove(menu);
    }
    private void PlayersReady()
    {
        for (int i = _openMenus.Count-1; i > -1; i--)
        {
            CloseMenu(GetMenuNameByEnum(_openMenus[i]));
        }
        ChangeToScene("StageOne");
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
            default:
                return "NO_MENU";
        }
    }

    public void ChangeToScene(string sceneName)
    {
        GameManager.instance.sceneLoader.GoToScene(sceneName);
    }
}
