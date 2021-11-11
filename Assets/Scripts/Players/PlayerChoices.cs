using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerChoices : MonoBehaviour
{
    public UnityAction onChanges;
    #region Enums
    public enum EnumTypes
    {
        OUTFIT,
        ROLE,
        WEAPON,
        TOWER,
        CONTROLS
    }
    public enum OutfitChoice
    {
        OUTFIT_A,
        OUTFIT_B,
        OUTFIT_C,
        OUTFIT_D
    }
    public OutfitChoice outfit;
    public enum RoleChoice
    {
        DAMAGE,
        TANK,
        HEALER
    }
    public RoleChoice role;
    public enum WeaponChoice
    {
        GUN,
        SWORD
    }
    public WeaponChoice weapon;
    public enum TowerChoice
    {
        TOWER,
        TRAP
    }
    public TowerChoice tower;
    public enum ControlsChoice 
    {
        MAP_A,
        MAP_B
    }
    public ControlsChoice controls;
    #endregion
    #region StringReturns
    public string OutfitText()
    {
        switch (outfit)
        {
            case OutfitChoice.OUTFIT_A:
                return "Outfit A";
            case OutfitChoice.OUTFIT_B:
                return "Outfit B";
            case OutfitChoice.OUTFIT_C:
                return "Outfit C";
            case OutfitChoice.OUTFIT_D:
                return "Outfit D";
            default:
                return "No Outfit";
        }
    }
    public string RoleText()
    {
        switch (role)
        {
            case RoleChoice.DAMAGE:
                return "Damage";
            case RoleChoice.TANK:
                return "Tank";
            case RoleChoice.HEALER:
                return "Healer";
            default:
                return "No Role";
        }
    }
    public string WeaponText()
    {
        switch (weapon)
        {
            case WeaponChoice.GUN:
                return "Gun";
            case WeaponChoice.SWORD:
                return "Sword";
            default:
                return "No Weapon";
        }
    }
    public string TowerText()
    {
        switch (tower)
        {
            case TowerChoice.TOWER:
                return "Tower";
            case TowerChoice.TRAP:
                return "Trap";
            default:
                return "No Tower";
        }
    }
    public string ControlsText()
    {
        switch (controls)
        {
            case ControlsChoice.MAP_A:
                return "Controls: A";
            case ControlsChoice.MAP_B:
                return "Controls: B";
            default:
                return "No Controls";
        }
    }
    #endregion
    #region StepThroughEnums
    public void StepEnum(EnumTypes type, int v)
    {
        int count;
        int currentIndex;
        switch (type)
        {
            case EnumTypes.OUTFIT:
                count = Enum.GetValues(typeof(OutfitChoice)).Length;
                currentIndex = (int)outfit;
                if (currentIndex + v == count) { outfit = (OutfitChoice)0; }
                else if (currentIndex + v == -1) { outfit = (OutfitChoice)(count - 1); }
                else { outfit = (OutfitChoice)(currentIndex + v); }
                break;
            case EnumTypes.ROLE:
                count = Enum.GetValues(typeof(RoleChoice)).Length;
                currentIndex = (int)role;
                if (currentIndex + v == count) { role = (RoleChoice)0; }
                else if (currentIndex + v == -1) { role = (RoleChoice)(count - 1); }
                else { role = (RoleChoice)(currentIndex + v); }
                break;
            case EnumTypes.WEAPON:
                count = Enum.GetValues(typeof(WeaponChoice)).Length;
                currentIndex = (int)weapon;
                if (currentIndex + v == count) { weapon = (WeaponChoice)0; }
                else if (currentIndex + v == -1) { weapon = (WeaponChoice)(count - 1); }
                else { weapon = (WeaponChoice)(currentIndex + v); }
                break;
            case EnumTypes.TOWER:
                count = Enum.GetValues(typeof(TowerChoice)).Length;
                currentIndex = (int)tower;
                if (currentIndex + v == count) { tower = (TowerChoice)0; }
                else if (currentIndex + v == -1) { tower = (TowerChoice)(count - 1); }
                else { tower = (TowerChoice)(currentIndex + v); }
                break;
            case EnumTypes.CONTROLS:
                count = Enum.GetValues(typeof(ControlsChoice)).Length;
                currentIndex = (int)controls;
                if (currentIndex + v == count) { controls = (ControlsChoice)0; }
                else if (currentIndex + v == -1) { controls = (ControlsChoice)(count - 1); }
                else { controls = (ControlsChoice)(currentIndex + v); }
                break;
        }
        onChanges?.Invoke();
    }
    #endregion
}
