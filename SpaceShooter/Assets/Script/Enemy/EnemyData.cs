using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Enemyの詳細パラメータ")]
    public float moveSpeed = 4.0f;
    public int enemyMaxLife = 3;
    public int giveDamage = 1;
}