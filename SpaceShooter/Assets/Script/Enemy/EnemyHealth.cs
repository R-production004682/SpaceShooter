using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private EnemyData enemyData;

    protected override void Start()
    {
        maxHealth = enemyData.enemyMaxLife;
        base.Start();
    }

    public override void Death()
    {
        Debug.Log("Enemy Death!");
        base.Death();
    }
}
