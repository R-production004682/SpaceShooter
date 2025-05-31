using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserData", menuName = "Player/LaserData", order = 1)]
public class LaserData : ScriptableObject
{
    [Header("’e‚ÌÚ×ƒpƒ‰ƒ[ƒ^")]
    public float bulletSpeed = 20;
    public int giveDamage = 1;

    [ReadOnly]
    public List<Vector3> singleBulletPosition = new List<Vector3> {
        new Vector3(0.0f, 1.0f ,0.0f)
    };

    [ReadOnly]
    public List<Vector3> doubleBulletPosition = new List<Vector3> {
        new Vector3(-0.78f, -0.5f, 0.0f),
        new Vector3( 0.78f, -0.5f, 0.0f)
    };

    [ReadOnly]
    public List<Vector3> tripleBulletPosition = new List<Vector3>{
        new Vector3( 0.0f,   1.0f, 0.0f),
        new Vector3(-0.78f, -0.5f, 0.0f),
        new Vector3( 0.78f, -0.5f, 0.0f)
    };
}