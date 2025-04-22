using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Item
{
    protected override float DropSpeed => itemData.boostersItemDropSpeed;

    protected override void ApplyEffect(Player player)
    {
        player.EnableTripleShot(ItemEffectInfo.BOOSTERS_ITEM_ENABLE_TIME);
    }
}
