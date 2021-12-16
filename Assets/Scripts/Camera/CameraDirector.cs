using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraDirector : MonoBehaviour
{
    public Animator animator;
    public CinemachineVirtualCamera gameplayCam;
    public CinemachineTargetGroup enemyGroupTarget;
    public enum CameraShots
    {
        INIT_CAM,
        GAMEPLAY_CAM,
        ENEMY_CAM
    }
    public enum ShakeIntensity
    {
        SMALL,
        MEDIUM,
        LARGE
    }
    private void Awake()
    {
        GameManager.instance.cameraDirector = this;
    }
    private void Start()
    {
        DelayedChangeShotTo(CameraShots.GAMEPLAY_CAM, 3);
        StartCoroutine(StartMessage());
    }

    IEnumerator StartMessage() 
    {
        yield return new WaitForSeconds(3);
        GUIManager.instance.messageToast.NewMessage("Get ready!");
        foreach(GameObject go in GameManager.instance.gameLogic.enemySpawnLocations)
        {
            enemyGroupTarget.AddMember(go.transform, 1, 3);
        }
    }
    IEnumerator DelayedChangeShot(CameraShots toShot, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeShotTo(toShot);
    }

    public void ShakeCamera(ShakeIntensity _intensity)
    {
        float intensity = 0;
        float duration = 0;
        switch (_intensity)
        {
            case ShakeIntensity.SMALL:
                intensity = 2f;
                duration = 0.1f;
                break;
            case ShakeIntensity.MEDIUM:
                intensity = 5f;
                duration = 0.2f;
                break;
            case ShakeIntensity.LARGE:
                intensity = 10f;
                duration = 0.4f;
                break;
        }
        CinemachineBasicMultiChannelPerlin perlin = gameplayCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        StartCoroutine(StopShake(duration));
    }
    IEnumerator StopShake(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        CinemachineBasicMultiChannelPerlin perlin = gameplayCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0f;

    }
    public void DelayedChangeShotTo(CameraShots toShot, float delay)
    {
        StartCoroutine(DelayedChangeShot(toShot, delay));
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
            case CameraShots.ENEMY_CAM:
                animator.Play("ENEMY_CAM");
                break;
        }
    }
}
