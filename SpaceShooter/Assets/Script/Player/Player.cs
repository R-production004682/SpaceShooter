using Constant;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject leftEngineEffect, rightEngineEffect, destroyEffect;

    private PlayerHealth health;
    private CameraShaker cameraShaker;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        leftEngineEffect.SetActive(false);
        rightEngineEffect.SetActive(false);
        destroyEffect.SetActive(false);

        cameraShaker = Camera.main?.GetComponent<CameraShaker>();
    }

    private void Update()
    {
        movementController.HandleMovement(playerData);
        playerShooter.HandleShooting(playerData);
        PlayerDamageLevel();
    }

    public void EnableTripleShot(float duration)
    {
        playerShooter.ActivateTripleShot(duration);
    }

    public void EnableSpeedup(float duration)
    {
        movementController.ActivateBoostSpeed(duration);
    }

    public void EnableShield(float duration) => health?.ActivateShield(duration);

    private void PlayerDamageLevel()
    {
        var playerCurrentHealth = health.CurrentHealth;

        if(playerCurrentHealth == DamageLevel.INSIGNIFICANT)
        {
            leftEngineEffect.SetActive(true);
        }
        else if(playerCurrentHealth == DamageLevel.MEDIUM_DEGREE) 
        {
            rightEngineEffect.SetActive(true);
        }
        else if(playerCurrentHealth == DamageLevel.SERIOUS)
        {
            OnDestoryAnimationEnd();
            destroyEffect.SetActive(true);
        }
    }

    private void OnDestoryAnimationEnd()
    {
        if(cameraShaker != null)
        {
            cameraShaker.StartCoroutine(cameraShaker.Shake(CameraEffect.STRONG_SHAKE_X, CameraEffect.STRONG_SHAKE_Y));
        }
    }
}