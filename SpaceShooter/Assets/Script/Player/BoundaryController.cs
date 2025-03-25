using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ړ��͈͐���N���X
/// </summary>
public class BoundaryController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// �ړ��͈͂̐���
    /// </summary>
    /// <returns></returns>
    public Vector2 RestrictionMovementRange(Vector2 currentPlayerPosition)
    {
        // Y�������̈ړ��͈͂̐���
        currentPlayerPosition.y = Mathf.Clamp(currentPlayerPosition.y, playerData.moveLimitButtom, playerData.moveLimitTop);
        // X�������̃��[�v����
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
