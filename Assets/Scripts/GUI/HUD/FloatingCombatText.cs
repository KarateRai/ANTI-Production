using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingCombatText : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject trackedPrefab;
    private ClampGUIToTransform clampGUI;
    private ObjectTween tween;
    public CanvasGroup cGroup;
    public TMP_Text numberText;
    private void Start()
    {
        GlobalEvents.instance.onStageSceneEnd += FadeOutComplete;
    }
    internal void SetValues(int _value, bool _isDamage, Vector3 _impactPos, bool _isHostile)
    {
        //Debug.Log("Setting Values for new FCT: " + _value + " , " + _isDamage + " , " + _impactPos);
        numberText.text = _value.ToString();
        if (_value <= 35)
        {
            numberText.fontSize = 30;
        }
        else if (_value <= 65)
        {
            numberText.fontSize = 45;
        }
        else
        {
            numberText.fontSize = 60;
        }

        if (_isDamage)
        {
            if (_isHostile)
                numberText.color = PlayerManager.instance.GetColor(PlayerChoices.OutfitChoice.YELLOW);
            else
                numberText.color = PlayerManager.instance.GetColor(PlayerChoices.OutfitChoice.RED);
        }
        else
        {
            numberText.color = PlayerManager.instance.GetColor(PlayerChoices.OutfitChoice.GREEN);
        }
        clampGUI = Instantiate(trackedPrefab, GameObject.Find("InstantiatedObjects").transform).GetComponent<ClampGUIToTransform>();
        clampGUI.gameObject.transform.position = _impactPos;
        tween = clampGUI.gameObject.GetComponent<ObjectTween>();
        rectTransform.position = clampGUI.GetPos();
        LeanTween.alphaCanvas(cGroup, 1, 0.1f)
            .setIgnoreTimeScale(false);
        tween.onEnableComplete += FadeInComplete;
        tween.onDisableComplete += FadeOutComplete;
    }
    private void FadeInComplete()
    {
        StartCoroutine(FadeOut());
    }
    private void FadeOutComplete()
    {
        Destroy(this);
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.3f);
        LeanTween.alphaCanvas(cGroup, 0, 0.4f)
            .setIgnoreTimeScale(false);
        if (tween != null)
            tween.Disable();
    }
    private void FixedUpdate()
    {
        rectTransform.position = clampGUI.GetPos();
    }
    private void OnDestroy()
    {
        GlobalEvents.instance.onStageSceneEnd -= FadeOutComplete;
        if (tween != null)
        {
            tween.onEnableComplete -= FadeInComplete;
            tween.onDisableComplete -= FadeOutComplete;
        }
        //if (clampGUI.gameObject != null)
        //{
        //    Destroy(clampGUI.gameObject);
        //}
    }
}
