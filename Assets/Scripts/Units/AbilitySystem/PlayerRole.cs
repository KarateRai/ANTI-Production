using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Players/Player Role")]
public class PlayerRole : UnitRole
{
    public PlayerChoices.RoleChoice playerRole;
    public Sprite roleIcon;
}
