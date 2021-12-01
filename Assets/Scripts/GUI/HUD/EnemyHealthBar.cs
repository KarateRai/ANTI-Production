using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject barPrefab;
    public ClampGUIToTransform clampGUI;
    private EnemySubBar subBar;
    private Vector2 pos;
    private void Awake()
    {
        subBar = Instantiate(barPrefab, GUIManager.instance.playerHUD.healthBars.transform).GetComponent<EnemySubBar>();
        pos = Vector2.zero;
    }
    private void OnDestroy()
    {
        if (subBar != null)
            subBar.Remove();
    }
    //private void Start()
    //{
    //    UpdateHealth(0);
    //    UpdateArmor(0);
    //}
    private void Update()
    {
        pos = new Vector2(clampGUI.GetPos().x, clampGUI.GetPos().y);
    }
    private void FixedUpdate()
    {
        subBar.barTransform.LeanSetPosX(clampGUI.GetPos().x);
        subBar.barTransform.LeanSetPosY(clampGUI.GetPos().y);
    }
    public void UpdateHealth(int percentage)
    {
        //Debug.Log(gameObject.name + "'s Health Updated To: " + percentage);
        subBar?.healthFillBar.UpdateValues(percentage);
    }
    public void UpdateArmor(int percentage)
    {
        //Debug.Log(gameObject.name + "'s Armor Updated To: " + percentage);
        subBar?.armorFillBar.UpdateValues(percentage);
    }
    public void SetImmediateHealth(int percentage)
    {
        //Debug.Log(gameObject.name + "'s Health Set To: " + percentage);
        subBar?.healthFillBar.SetImmediateValues(percentage);
    }
    public void SetImmediateArmor(int percentage)
    {
        //Debug.Log(gameObject.name + "'s Armor Set To: " + percentage);
        subBar?.armorFillBar.SetImmediateValues(percentage);
    }
}
