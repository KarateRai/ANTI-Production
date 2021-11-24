using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    public Animator animator;

    public enum CameraShots
    {
        INIT_CAM,
        GAMEPLAY_CAM
    }
    private void Start()
    {
        StartCoroutine(DelayedChangeShotTo(CameraShots.GAMEPLAY_CAM, 3));
    }
    IEnumerator DelayedChangeShotTo(CameraShots toShot, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeShotTo(toShot);
        GUIManager.instance.messageToast.NewMessage("Get ready!");
    }
    public void ChangeShotTo(CameraShots toShot)
    {
        switch (toShot)
        {
            case CameraShots.INIT_CAM:
                animator.Play("INIT_CAM");
                break;
            case CameraShots.GAMEPLAY_CAM:
                animator.Play("GAMEPLAY_CAM");
                break;
        }
    }
}
