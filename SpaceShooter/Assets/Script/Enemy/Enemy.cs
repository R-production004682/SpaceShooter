using Constant;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private void Update()
    {
        transform.Translate(Vector3.down * enemyData.moveSpeed * Time.deltaTime);

        if(transform.position.y < -5f)
        {
            var randomX = Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT);
            transform.position = new Vector3(randomX, LimitedPosition.SPAWN_TOP, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IHealth target = other.GetComponent<IHealth>();
            if (target != null) {

                // Playerにダメージを与える。
                target.TakeDamage(enemyData.giveDamage, other.gameObject);
            }
        }
    }
}
