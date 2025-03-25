using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserData", menuName = "Player/LaserData", order = 1)]
public class LaserData : ScriptableObject
{
    [Header("�e�̏ڍ׃p�����[�^")]
    public float bulletSpeed = 6;
    public int giveDamage = 1;

}
