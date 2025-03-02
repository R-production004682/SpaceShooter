using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;


public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(horizontal, vertical).normalized;
        transform.Translate(moveDirection * playerData.moveSpeed * Time.deltaTime);


        Vector2 currentPosition = transform.position;

        // X軸方向の移動範囲の制限と、Y軸方向の移動範囲の制限
        currentPosition.x = Mathf.Clamp(currentPosition.x, LimitPlayerMove.LEFT, LimitPlayerMove.RIGHT);
        currentPosition.y = Mathf.Clamp(currentPosition.y, LimitPlayerMove.BUTTOM, LimitPlayerMove.TOP);
        transform.position = new Vector2(currentPosition.x, currentPosition.y);
    }
}
