using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;

/// <summary>
/// 移動範囲制御クラス
/// </summary>
public class BoundaryController : MonoBehaviour
{
    /// <summary>
    /// 移動範囲の制約
    /// </summary>
    /// <returns></returns>
    public Vector2 RestrictionMovementRange(Vector2 currentPlayerPosition)
    {
        // Y軸方向の移動範囲の制限
        currentPlayerPosition.y = Mathf.Clamp(currentPlayerPosition.y, LimitedPosition.BOTTOM, LimitedPosition.TOP);
        // X軸方向のワープ処理
        if (currentPlayerPosition.x > LimitedPosition.RIGHT)
        {
            currentPlayerPosition.x = LimitedPosition.LEFT;
        }
        else if (currentPlayerPosition.x < LimitedPosition.LEFT)
        {
            currentPlayerPosition.x = LimitedPosition.RIGHT;
        }

        return currentPlayerPosition;
    }
}
