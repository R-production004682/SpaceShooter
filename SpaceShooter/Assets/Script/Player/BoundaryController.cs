using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;

/// <summary>
/// �ړ��͈͐���N���X
/// </summary>
public class BoundaryController : MonoBehaviour
{
    /// <summary>
    /// �ړ��͈͂̐���
    /// </summary>
    /// <returns></returns>
    public Vector2 RestrictionMovementRange(Vector2 currentPlayerPosition)
    {
        // Y�������̈ړ��͈͂̐���
        currentPlayerPosition.y = Mathf.Clamp(currentPlayerPosition.y, LimitedPosition.BOTTOM, LimitedPosition.TOP);
        // X�������̃��[�v����
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
