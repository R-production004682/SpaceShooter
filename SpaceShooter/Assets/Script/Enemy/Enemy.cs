using Constant;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyShooter enemyShooter;

    private EnemyHealth enemyHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator Not Found");
        }

        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth Not Found");
        }

        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEntityDied += () => {
                enemyShooter.DisableShoothing();
            };
        }
    }

    private void Update()
    {
        MoveHandler();
        enemyShooter.HandleShooting(enemyData);
        RepositionIfOutOfBounds();
        OnEnemyDied();
    }

    private void MoveHandler()
    {
        var enemyMoveSpeed = enemyData.moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * enemyMoveSpeed);
    }

    private void OnEnemyDied()
    {
        enemyShooter?.DisableShoothing();
    }

    private void RepositionIfOutOfBounds()
    {
        if (transform.position.y < LimitedPosition.DEAD_LINE)
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

            // Playerにダメージを与える。
            target?.TakeDamage(enemyData.giveDamage, other.gameObject);
        }
    }
}
