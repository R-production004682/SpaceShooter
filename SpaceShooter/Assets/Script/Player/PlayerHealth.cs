using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CrackedDisplay crackedDisplay;

    protected override void Start()
    {
        maxHealth = playerData.playerMaxLife;
        base.Start();
    }

    public override void TakeDamage(int damage, GameObject target)
    {
        base.TakeDamage(damage, target);
    }

    public override void Death(GameObject target)
    {
        base.Death(target);

        if (target.CompareTag("Player") && crackedDisplay != null)
        {
            crackedDisplay.ExplodeScreen();
        }
    }
}
