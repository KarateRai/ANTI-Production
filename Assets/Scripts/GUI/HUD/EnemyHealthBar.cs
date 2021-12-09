using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject barPrefab;
    public ClampGUIToTransform clampGUI;
    private EnemySubBar subBar;
    public Vector2 pos;
    private void Awake()
    {
        subBar = Instantiate(barPrefab, GUIManager.instance.playerHUD.trackedObjects.transform).GetComponent<EnemySubBar>();
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
        int xMax = 1910;
        int xMin = 10;
        int yMax = 1070;
        int yMin = 10;
        bool outOfBounds = false;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax) { outOfBounds = true; }

        if (!outOfBounds)
        {
            subBar.defaultDisplayGroup.alpha = 1;
            subBar.outOfBoundsDisplayGroup.alpha = 0;
        }
        else
        {
            subBar.defaultDisplayGroup.alpha = 0;
            subBar.outOfBoundsDisplayGroup.alpha = 1;
            if (pos.x < xMin) { pos.x = xMin; }
            else if (pos.x > xMax) { pos.x = xMax; }
            if (pos.y < yMin) { pos.y = yMin; }
            else if (pos.y > yMax) { pos.y = yMax; }
        }
            subBar.barTransform.LeanSetPosX(pos.x);
            subBar.barTransform.LeanSetPosY(pos.y);
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
