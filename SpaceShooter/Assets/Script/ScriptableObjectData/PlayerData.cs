using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Playerの詳細パラメータ")]
    public float moveSpeed = 3.5f;
    public float boostMoveSpeed = 5.0f;
    public float shotInterval = 0.25f;
    public int playerMaxLife = 5;
}
