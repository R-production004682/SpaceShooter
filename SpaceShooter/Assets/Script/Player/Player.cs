using Constant;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject leftEngine, rightEngine;

    private PlayerHealth health;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);
    }

    private void Update()
    {
        movementController.HandleMovement();
        shooter.HandleShooting();
        PlayerDamageLevel();
    }

    public void EnableTripleShot(float duration)
    {
        shooter.ActivateTripleShot(duration);
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
            leftEngine.SetActive(true);
        }
        else if(playerCurrentHealth == DamageLevel.MEDIUM_DEGREE) 
        {
            rightEngine.SetActive(true);
        }
    }
}