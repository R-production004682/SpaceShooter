using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerShooter shooter;

    private void Update()
    {
        movementController.HandleMovement();
        shooter.HandleShooting();
    }


}