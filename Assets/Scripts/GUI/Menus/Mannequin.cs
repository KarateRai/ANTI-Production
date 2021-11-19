using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public Animator animator;
    public Material onMaterial, offMaterial;
    public SkinnedMeshRenderer meshRenderer;
    public GameObject spotLight;
    public enum MannequinState
    {
        INACTIVE,
        ACTIVE,
        READY
    }
    public MannequinState mannequinState;

    private void CheckState()
    {
        switch (mannequinState)
        {
            case MannequinState.INACTIVE:
                animator.SetBool("isActive", false);
                animator.SetBool("isReady", false);
                spotLight.SetActive(false);
                ChangeMaterial(false);
                break;
            case MannequinState.ACTIVE:
                animator.SetBool("isActive", true);
                animator.SetBool("isReady", false);
                spotLight.SetActive(true);
                ChangeMaterial(true);
                break;
            case MannequinState.READY:
                animator.SetBool("isReady", true);
                break;
        }        
    }
    private void ChangeMaterial(bool onOrOff)
    {
        Material[] materialArray = meshRenderer.materials;
        if (onOrOff == true)
        {
            materialArray[0] = onMaterial;
        }
        else
        {
            materialArray[0] = offMaterial;
        }
        meshRenderer.materials = materialArray;
    }
    public void SetMannequinState(MannequinState newState)
    {
        mannequinState = newState;
        CheckState();
    }
    public void SetMannequinMaterial(Material newMaterial)
    {
        onMaterial = newMaterial;
        CheckState();
    }
}
