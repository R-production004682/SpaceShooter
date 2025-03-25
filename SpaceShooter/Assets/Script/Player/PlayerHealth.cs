using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private PlayerData playerData;

    protected override void Start()
    {
        maxHealth = playerData.playerMaxLife;
        base.Start();
    }

    public override void Death()
    {
        Debug.Log("Player Death!");
        base.Death();
    }
}
