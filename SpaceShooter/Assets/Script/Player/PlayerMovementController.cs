using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ˆÚ“®§ŒäƒNƒ‰ƒX
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    private BoundaryController boundaryController;
    private bool isBoosting = false;
    private bool isMoving = false;
    private void Awake()
    {
        boundaryController = GetComponent<BoundaryController>();
    }

    public Vector2 InputMoveDirection()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical).normalized;

    }

    public void HandleMovement(PlayerData playerData)
    {
        if(!isMoving) return;

        var moveDirection = InputMoveDirection();
        var moveSpeed = isBoosting ? playerData.boostMoveSpeed : playerData.moveSpeed;

        var newPlayerPosition = (Vector2)transform.position + moveDirection
                                            * moveSpeed * Time.deltaTime;

        transform.position = boundaryController.RestrictionMovementRange(newPlayerPosition);
    }

    public void ActivateBoostSpeed(float duration)
    {
        StartCoroutine(BoostSpeedMovement(duration));
    }

    public IEnumerator BoostSpeedMovement(float duration)
    {
        isBoosting = true;
        yield return new WaitForSeconds(duration);
        isBoosting = false;
    }

    public void EnableControl() => isMoving = true;
    public void DisableControl() => isMoving = false;
    
}
