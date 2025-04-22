using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private PlayerData playerData;

    private void Update()
    {
        movementController.HandleMovement();
        shooter.HandleShooting();
    }

    public void EnableTripleShot(float duration)
    {
       shooter.ActivateTripleShot(duration);
    }

    public void EnableSpeedup(float duration)
    {
       movementController.ActivateBoostSpeed(duration);
    }

    public void EnableShield(float duration)
    {
        var health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.ActivateShield(duration);
        }
    }
}