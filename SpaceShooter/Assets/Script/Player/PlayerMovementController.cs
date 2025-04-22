using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// à⁄ìÆêßå‰ÉNÉâÉX
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private BoundaryController boundaryController;

    private bool isBoosting = false;

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

    public void HandleMovement()
    {
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
}
