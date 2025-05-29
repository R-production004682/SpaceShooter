using Constant;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyShooter enemyShooter;

    private EnemyHealth enemyHealth;
    private CameraShaker cameraShaker;

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
                enemyShooter?.DisableShoothing();
            };
        }

        cameraShaker = Camera.main?.GetComponent<CameraShaker>();
    }

    private void Update()
    {
        MoveHandler();
        enemyShooter.HandleShooting(enemyData);
        RepositionIfOutOfBounds();
    }

    private void MoveHandler()
    {
        var enemyMoveSpeed = enemyData.moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * enemyMoveSpeed);
    }

    private void RepositionIfOutOfBounds()
    {
        if (transform.position.y < LimitedPosition.DEAD_LINE)
        {
            var randomX = Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT);
            transform.position = new Vector3(randomX, LimitedPosition.SPAWN_TOP, 0);
        }
    }

    public void OnDestoroyAnimationEnd()
    {
        if(cameraShaker != null)
        {
            cameraShaker.StartCoroutine(cameraShaker.Shake(CameraEffect.WEAK_SHAKE_X, CameraEffect.WEAK_SHAKE_Y));
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
