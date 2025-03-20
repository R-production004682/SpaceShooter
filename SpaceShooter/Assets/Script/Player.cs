using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;


public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    public GameObject laserPrefub;
    private ObjectPool laserPool;

    private void Start()
    {
        transform.position = Vector3.zero;
        laserPool = FindObjectOfType<ObjectPool>();
    }

    private void Update()
    {
        HandleMovement();
        ShotLaser();
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(horizontal, vertical).normalized;
        var newPlayerPosition = (Vector2)transform.position + moveDirection 
                                            * playerData.moveSpeed * Time.deltaTime;

        transform.position = RestrictionMovementRange(newPlayerPosition);
    }

    private void ShotLaser()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var laser = laserPool.Launch(this.transform.position, 0);

            if(laser == null)
            {
                 return;
            }
            laser.GetComponent<Laser>();
        }
    }

    /// <summary>
    /// �ړ��͈͂̐���
    /// </summary>
    /// <returns></returns>
    private Vector2 RestrictionMovementRange(Vector2 currentPlayerPosition)
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
