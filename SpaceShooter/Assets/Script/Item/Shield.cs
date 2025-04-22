using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    protected override float DropSpeed => itemData.shieldItemDropSpeed;

    protected override void ApplyEffect(Player player)
    {
        player.EnableShield(ItemEffectInfo.BOOSTERS_ITEM_ENABLE_TIME);
    }
}
