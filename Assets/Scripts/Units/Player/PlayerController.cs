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

    public PlayerStats stats;
    public UnitAbilities unitAbilities;
    public UnitRole role;

    public Player player;
    public Material[] materialList;
    public Material playerMaterial;
    public SkinnedMeshRenderer meshRenderer;

    ///--------------------Tower stuff--------------------///
    private bool buildMode = false;
    public int maxTowers;
    public Transform buildTargetTransform;
    private GameObject towerPreview;
    private Transform targetTransform = null;
    public TowerManager towerManager;

    void Start()
    {
        InitializeCharacter();
        movement.animator = GetComponent<Animator>();
        //TEMP FOR TESTING, REMOVE UNDER HERE
        weaponController.equippedWeapon = Object.Instantiate(weaponController.equippedWeapon);
        weaponController.SetShootingPos();
    }
    public void AssignPlayer(int playerID)
    {
        player = PlayerManager.instance.players[playerID];
        role = PlayerManager.instance.GetPlayerRole(player.playerChoices.role);
        //TAKE AWAY COMMENT FOR REAL GAME
        //weaponController.equippedWeapon = Object.Instantiate(GameManager.instance.GetWeapon(player.playerChoices.weapon));
        weaponController.equippedWeapon = Object.Instantiate(weaponController.equippedWeapon);
        weaponController.SetShootingPos();
        unitAbilities.AddCooldowns(this);

        AssignMeterial();
    }

    private void LateUpdate()
    {
        //weaponController.Fire();
    }

    void AssignMeterial()
    {
        Material[] materialArray = meshRenderer.materials;


        switch (player.playerChoices.outfit)
        {
            case PlayerChoices.OutfitChoice.BLUE:
                playerMaterial = materialList[0];
                break;
            case PlayerChoices.OutfitChoice.GREEN:
                playerMaterial = materialList[1];
                break;
            case PlayerChoices.OutfitChoice.YELLOW:
                playerMaterial = materialList[2];
                break;
            case PlayerChoices.OutfitChoice.ORANGE:
                playerMaterial = materialList[3];
                break;
            case PlayerChoices.OutfitChoice.RED:
                playerMaterial = materialList[4];
                break;
            case PlayerChoices.OutfitChoice.PURPLE:
                playerMaterial = materialList[5];
                break;
        }
        materialArray[0] = playerMaterial;
        meshRenderer.materials = materialArray;
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
        stats.GainHealth(amount);
    }

    public override void TakeDamage(int amount)
    {
        stats.TakeDamage(amount);
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

            towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
            //Debug.Log("Building mode toggle!------------------------------------------------------" + buildMode);
        }

    }

    public void Build(InputAction.CallbackContext context)
    {
        if (context.performed && buildMode)
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

    public void Spawn()
    {
        //Move to starting position
        gameObject.SetActive(true);
        isDead = false;
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

    void Update()
    {
        if (buildMode)
        {
            RaycastHit hit;

            LayerMask floorMask = LayerMask.GetMask("BuildableFloor");
            if (Physics.Raycast(buildTargetTransform.position, Vector3.down, out hit, float.MaxValue, floorMask))
            {
                targetTransform = hit.transform;

                if (towerManager.CheckTileClear(targetTransform.gameObject))
                {
                    towerPreview.transform.position = targetTransform.position;
                    towerPreview.transform.position += new Vector3(0, 0.5f, 0);
                }
                else
                {
                    towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
                }
            }
            else
            {
                towerPreview.transform.position = new Vector3(-1000, -1000, -1000);
            }
        }
    }
}
