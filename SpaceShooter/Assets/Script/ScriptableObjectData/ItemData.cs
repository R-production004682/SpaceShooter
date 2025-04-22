using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Item/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("強化アイテムパラメータ")]
    public float boostersItemDropSpeed = 3.0f;
    public float shieldItemDropSpeed = 6.5f;
    public float speedupItemDropSpeed = 4.5f;

}
