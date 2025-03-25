using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Enemy‚ÌÚ×ƒpƒ‰ƒ[ƒ^")]
    public float moveSpeed = 4.0f;
    public int enemyMaxLife = 5;
    public int giveDamage = 1;
}