using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Playerฬฺืp[^")]
    public float moveSpeed = 3.5f;
    public float shotInterval = 0.5f; // หิu
    public int playerMaxLife = 5;
    public int giveDamage = 1;

    [Header("Playerฬฺฎย\ออ")]
    public float moveLimitTop = 0;
    public float moveLimitButtom = -3.8f;
    public float moveLimitLeft = -11f;
    public float moveLimitRight = 11f;
}
