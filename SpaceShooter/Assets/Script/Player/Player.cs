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
}