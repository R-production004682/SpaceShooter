using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedup : Item
{
    protected override float DropSpeed => itemData.speedupItemDropSpeed;

    protected override void ApplyEffect(Player player)
    {   
        player.EnableSpeedup(ItemEffectInfo.BOOSTERS_ITEM_ENABLE_TIME);
    }
}
