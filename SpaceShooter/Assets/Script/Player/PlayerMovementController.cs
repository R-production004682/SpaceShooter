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

    private void Awake()
    {
        boundaryController = GetComponent<BoundaryController>();
    }

    public void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(horizontal, vertical).normalized;
        var newPlayerPosition = (Vector2)transform.position + moveDirection
                                            * playerData.moveSpeed * Time.deltaTime;

        transform.position = boundaryController.RestrictionMovementRange(newPlayerPosition);
    }
}
