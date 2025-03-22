using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動範囲制御クラス
/// </summary>
public class BoundaryController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// 移動範囲の制約
    /// </summary>
    /// <returns></returns>
    public Vector2 RestrictionMovementRange(Vector2 currentPlayerPosition)
    {
        // Y軸方向の移動範囲の制限
        currentPlayerPosition.y = Mathf.Clamp(currentPlayerPosition.y, playerData.moveLimitButtom, playerData.moveLimitTop);
        // X軸方向のワープ処理
        if (currentPlayerPosition.x > playerData.moveLimitRight)
        {
            currentPlayerPosition.x = playerData.moveLimitLeft;
        }
        else if (currentPlayerPosition.x < playerData.moveLimitLeft)
        {
            currentPlayerPosition.x = playerData.moveLimitRight;
        }

        return currentPlayerPosition;
    }
}
