using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DressingRoom : MonoBehaviour
{
    public Mannequin[] mannequins;
    public Transform[] mannequinTransforms;
    //public GameObject[] lights;
    public PlayerChoices[] choices;
    public bool dressingRoomActive;
    public Material offMaterial, blueMaterial, greenMaterial, orangeMaterial, purpleMaterial, redMaterial, yellowMaterial;
    public CinemachineTargetGroup targetGroup;
    private void Awake()
    {
        GlobalEvents.instance.onTeamSceneStart += Begin;
        GlobalEvents.instance.onTeamSceneEnd += End;
        GUIManager.instance.dressingRoom = this;
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onTeamSceneStart -= Begin;
        GlobalEvents.instance.onTeamSceneEnd -= End;
    }
    public void MannequinActive(int id, bool state)
    {
        //lights[id]?.SetActive(state);
        if (state == true)
        {
            mannequins[id].SetMannequinState(Mannequin.MannequinState.ACTIVE);
            targetGroup.AddMember(mannequinTransforms[id], 1f, 0.5f);
        }
        else
        {
            mannequins[id].SetMannequinState(Mannequin.MannequinState.INACTIVE);
            targetGroup.RemoveMember(mannequinTransforms[id]);
        }
    }
    public void MannequinReady(int id, bool state)
    {
        if (state == true)
        {
            mannequins[id].SetMannequinState(Mannequin.MannequinState.READY);
        }
        else
        {
            mannequins[id].SetMannequinState(Mannequin.MannequinState.ACTIVE);
        }
    }
    public void Begin()
    {
        dressingRoomActive = true;
        foreach (Player p in PlayerManager.instance.players)
        {
            MannequinActive(p.playerIndex, true);
        }
    }
    public void End()
    {
        dressingRoomActive = false;
        //for (int i = 0; i < mannequins.Length; i++)
        //{
        //    lights[i]?.SetActive(false);
        //}
    }
    public void UpdateChoices(PlayerChoices newChoices, int pID)
    {
        choices[pID].Copy(newChoices);
        mannequins[pID].SetMannequinMaterial(MaterialFromChoice(choices[pID].outfit));
    }

    private Material MaterialFromChoice(PlayerChoices.OutfitChoice outfit)
    {
        switch (outfit)
        {
            case PlayerChoices.OutfitChoice.BLUE:
                return blueMaterial;
            case PlayerChoices.OutfitChoice.GREEN:
                return greenMaterial;
            case PlayerChoices.OutfitChoice.YELLOW:
                return yellowMaterial;
            case PlayerChoices.OutfitChoice.ORANGE:
                return orangeMaterial;
            case PlayerChoices.OutfitChoice.RED:
                return redMaterial;
            case PlayerChoices.OutfitChoice.PURPLE:
                return purpleMaterial;
            default:
                return offMaterial;
        }
    }
}
