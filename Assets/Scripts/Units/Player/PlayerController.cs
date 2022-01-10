using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController
{
    private Movement movement;
    private Vector2 input = Vector2.zero;
    private Vector2 aim = Vector2.zero;

    ///---------------Character variables---------------///
    [Header("Character settings")]
    private float invulnerable = 0;
    public PlayerStats stats;
    public UnitAbilities unitAbilities;
    [HideInInspector] public UnitRole role;

    [Header("Player Materials")]
    [HideInInspector] public Player player;
    public Material[] materialList;
    /*[HideInInspector]*/ public Material playerMaterial;
    public SkinnedMeshRenderer meshRenderer;

    ///--------------------Tower stuff--------------------///
    [Header("Tower settings")]
    private bool buildMode = false;
    private bool canBuild;
    public int maxTowers;
    public Transform buildTargetTransform;
    private GameObject towerPreview;
    private Transform targetTransform = null;
    [HideInInspector] public TowerManager towerManager;

    ///--------------------Sound--------------------///
    [Header("SoundEffects")]
    [SerializeField] SoundEffectPlayer takeDamageSound;
    [SerializeField] SoundEffectPlayer deathSound;

    ///--------------------Misc--------------------///
    [Header("Misc")]
    public PlayerMarker playerMarker;
    [HideInInspector] public Vector3 spawnPoint;
    public GameObject spawnEffect;


    void Start()
    {
        InitializeCharacter();
        movement.animator = GetComponent<Animator>();
        onDeath += OnDeath;
    }
    public void AssignPlayer(int playerID)
    {
        player = PlayerManager.instance.GetPlayerByID(playerID);
        role = PlayerManager.instance.GetPlayerRole(player.playerChoices.role);
        AssignMaterial();
        weaponController.equippedWeapon = Object.Instantiate(GameManager.instance.GetWeapon(player.playerChoices.weapon));
        //Activate for solo test
        //weaponController.equippedWeapon = Object.Instantiate(weaponController.equippedWeapon);
        unitAbilities.AddCooldowns(this);
        playerMarker.subMarker.SetValues("P" + (playerID + 1), PlayerManager.instance.GetColor(player.playerChoices.outfit));
        playerMarker.Toggle(true);
        weaponController.SetShootingPos();
        GUIManager.instance.playerHUD.playerHUDs[player.playerIndex].SetDisplayGroup(PlayerHUD.DisplayGroups.DEFAULT);
    }
    void AssignMaterial()
    {
        Material[] materialArray = meshRenderer.materials;

        switch (player.playerIndex)
        {
            case 0:
                playerMaterial = materialList[0];
                break;
            case 1:
                playerMaterial = materialList[2];
                break;
            case 2:
                playerMaterial = materialList[1];
                break;
            case 3:
                playerMaterial = materialList[5];
                break;
        }
        meshRenderer.materials[0] = playerMaterial;
    }
    void FixedUpdate()
    {
        movement?.Update(input, aim);
    }

    private void InitializeCharacter()
    {
        movement = new Movement(this);
        stats = new PlayerStats(this, stats.Health, stats.Shield, stats.Speed, stats.MaxSpeed);

    }

    public override void GainHealth(int amount)
    {
        GUIManager.instance.NewFloatingCombatText(amount, false, transform.position, false);
        stats.GainHealth(amount);
    }

    public override void TakeDamage(int amount)
    {
        if (invulnerable <= 0)
        {
            GUIManager.instance.NewFloatingCombatText(amount, true, transform.position, false);
            takeDamageSound.PlaySound();
            stats.TakeDamage(amount);
            GameManager.instance.cameraDirector.ShakeCamera(CameraDirector.ShakeIntensity.SMALL);
            invulnerable = 0.02f;
        }
        GUIManager.instance.NewFloatingCombatText(0, true, transform.position, false);
    }

    public void UseAbilityOne(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            unitAbilities.ActivateAbility(0);
        }
    }

    public void UseAbilityTwo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<UnitAbilities>().ActivateAbility(1);
        }
    }
    public void UseAbilityThree(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<UnitAbilities>().ActivateAbility(2);
        }
    }

    public void UseWeapon(InputAction.CallbackContext context)
    {
        weaponController.Fire();
    }

    public void Character_Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Character_Aim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    public void BuildMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Activate some build mode
            buildMode = !buildMode;
            if (!towerManager)
            {
                towerManager = FindObjectOfType<TowerManager>();
                if (!towerManager)
                {
                    Debug.Log("Didn't find tower manager");
                }
            }
            if (!towerPreview)
            {
                towerPreview = Instantiate(towerManager.myTowerPrefabs[(int)player.playerChoices.tower]);
                towerPreview.GetComponent<Tower>().SetPreview();
                GameObject parentTransform = GameObject.Find("InstantiatedObjects");
                towerPreview.transform.SetParent(parentTransform.transform);
            }
            if (buildMode)
            {
                GUIManager.instance.playerHUD.playerHUDs[player.playerIndex].SetDisplayGroup(PlayerHUD.DisplayGroups.BUILD);
                GUIManager.instance.playerHUD.playerHUDs[player.playerIndex].SetNumTowers(towerManager.CheckNumBuiltTowers(gameObject), maxTowers);
                towerPreview.GetComponent<Tower>().SetGhostOnOff(true);
            }
            else
            {
                GUIManager.instance.playerHUD.playerHUDs[player.playerIndex].SetDisplayGroup(PlayerHUD.DisplayGroups.DEFAULT);
                towerPreview.GetComponent<Tower>().SetGhostOnOff(false);
            }

            towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
            //Debug.Log("Building mode toggle!------------------------------------------------------" + buildMode);
        }

    }

    public void Build(InputAction.CallbackContext context)
    {
        if (context.performed && buildMode && canBuild)
        {
            //Place chosen tower
            if (towerManager.CheckTileClear(targetTransform.gameObject) && towerManager.CheckNumBuiltTowers(gameObject) < maxTowers)
            {
                GameObject newTower = Instantiate(towerManager.myTowerPrefabs[(int)player.playerChoices.tower]);
                newTower.transform.position = targetTransform.position + new Vector3(0, 0.5f, 0);
                newTower.GetComponent<Tower>().SetParents(targetTransform.gameObject, this.gameObject);
                GameObject parentTransform = GameObject.Find("InstantiatedObjects");
                newTower.transform.SetParent(parentTransform.transform);
                towerManager.AddTowerToList(newTower);
                //Debug.Log("Building!------------------------------------------------------");
            }
        }
    }

    public void Deconstruct(InputAction.CallbackContext context)
    {
        if (context.performed && buildMode)
        {
            //Debug.Log("Deconstructing!------------------------------------------------------");
            towerManager.DeleteTowerOnCell(targetTransform.gameObject);
        }
    }

    private void OnDeath()
    {
        if (towerPreview != null)
        {
            towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
        }
        playerMarker.Toggle(false);
        stats.ResetSpeed();
        weaponController.ResetWeapon();
        GlobalEvents.instance.onPlayerDeath?.Invoke(player);
    }
    public void Spawn()
    {
        //Move to starting position
        //Debug.Log("Spawned " + gameObject.name);
        transform.position = spawnPoint;
        stats.ResetHealth();
        gameObject.SetActive(true);
        isDead = false;
        playerMarker.Toggle(true);
        ParticleSystem sEffect = Instantiate(spawnEffect, transform.position, Quaternion.identity, GameObject.Find("InstantiatedObjects").transform).GetComponent<ParticleSystem>();
        sEffect.GetComponent<Renderer>().material = playerMaterial;
        
        GlobalEvents.instance.onPlayerRespawn?.Invoke(player);
    }

    public override void AffectSpeed(int amount)
    {
        stats.SetSpeed(amount);
    }
    public IEnumerator DelayedResetSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.ResetSpeed();
    }
    public override void Regen(int amountToRegen, float regenSpeed)
    {
        StartCoroutine(RegenCoroutine(amountToRegen, regenSpeed));
    }
    public IEnumerator RegenCoroutine(int amountToRegen, float regenSpeed)
    {
        while (amountToRegen > 0 || Channeling == true)
        {
            stats.GainHealth(5);
            amountToRegen -= 5;
            yield return new WaitForSeconds(regenSpeed);
        }

    }

    public void MakeInvulnerable(float time)
    {
        invulnerable += time;
    }
    void Update()
    {
        if (isGameOver)
        {
            input = new Vector2(0, 0);
            aim = new Vector2(0, 0);
            weaponController.StopAttacking();
            invulnerable = 5f;
        }
        if (player != null && towerManager != null)
        {
            GUIManager.instance.playerHUD.playerHUDs[player.playerIndex].SetNumTowers(towerManager.CheckNumBuiltTowers(gameObject), maxTowers);
        }
        if (invulnerable != 0)
        {
            invulnerable -= Time.deltaTime;
        }
        if (buildMode)
        {
            RaycastHit hit;

            LayerMask floorMask = LayerMask.GetMask("BuildableFloor");
            if (Physics.Raycast(buildTargetTransform.position, Vector3.down, out hit, float.MaxValue, floorMask))
            {
                canBuild = true;
                targetTransform = hit.transform;
                towerPreview.transform.position = targetTransform.position;
                towerPreview.transform.position += new Vector3(0, 0.5f, 0);

                if (towerManager.CheckTileClear(targetTransform.gameObject) && towerManager.CheckNumBuiltTowers(gameObject) < maxTowers)
                {
                    towerPreview.GetComponent<Tower>().SetGhostColour(false); //false for blue, true for red
                }
                else
                {
                    towerPreview.GetComponent<Tower>().SetGhostColour(true); //false for blue, true for red
                }
            }
            else
            {
                towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
                canBuild = false;
            }
        }
    }
}
