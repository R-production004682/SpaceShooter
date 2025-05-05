using Constant;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator animator;
    
    private float destroyTime = 0.5f;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null ) 
        {
            Debug.LogError("Animator Not Found"); 
        }

        enemyHealth = GetComponent<EnemyHealth>();
        if(enemyHealth == null)
        {
            Debug.LogError("EnemyHealth Not Found");
        }
    }

    private void Update()
    {
        MoveHandler();
        RepositionIfOutOfBounds();
    }

    private void MoveHandler()
    {
        var enemyMoveSpeed = enemyData.moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * enemyMoveSpeed);

        if (enemyHealth.IsDestroy)
        {
            var destroySpeed = Mathf.Lerp(0 , enemyData.moveSpeed, destroyTime);
            transform.Translate(Vector3.down * (enemyMoveSpeed - destroySpeed) * Time.deltaTime);
        }
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
