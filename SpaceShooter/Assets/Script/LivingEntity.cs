using System.Collections;
using UnityEngine;
using Constant;
using System;

public class LivingEntity : MonoBehaviour, IHealth
{
    [SerializeField] protected int maxHealth;
    [SerializeField] private ShieldEffect shieldEffect;
    
    private UIManager uIManager;
    private SpawnManager spawnManager;
    private Animator animator;
    private Coroutine shieldCoroutine;

    protected int currentHealth;
    public int CurrentHealth => currentHealth;

    protected bool isDestroy;
    public event Action OnEntityDied;

    protected bool isInvincible;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        isDestroy = false;

        uIManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if(uIManager == null)
        {
            Debug.LogError("UIManager Not Found"); 
        }
    }

    /// <summary>
    /// ダメージを与える処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="targetGameObject">ダメージを与える対象</param>
    public virtual void TakeDamage(int damage , GameObject targetGameObject)
    {
        if(isInvincible || currentHealth <= 0) return;
        currentHealth -= damage;

        var flash = targetGameObject.GetComponent<FlashEffect>();
        flash?.Flash();

        if (targetGameObject.CompareTag("Player"))
        {
            // PlayerのUI表示を切り替える
            uIManager?.UpdateLivesUI(currentHealth);
        }

        if (currentHealth <= 0)
        {
            HandleDeath(targetGameObject);
        }
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void Death(GameObject gameObject)
    {
        AudioManager.Instance?.PlayExplosion();

        if (gameObject.tag == "Enemy")
        {
            uIManager?.AddScore(10);
        }
        Collider2D collider2D = gameObject.GetComponent<Collider2D>();
        collider2D.enabled = false;

        if(gameObject.tag == "Player")
        {
            Destroy(gameObject, 0.9f);
        }
        else
        {
            Destroy(gameObject, 2.5f);
        }
    }


    private void HandleDeath(GameObject targetGameObject)
    {
        var mainCamera = Camera.main;
        var shaker = mainCamera?.GetComponent<CameraShaker>();
        if (shaker != null)
        {
            if (targetGameObject?.tag == "Player")
            {
                shaker.StartCoroutine(shaker.Shake(CameraEffect.STRONG_SHAKE_X, CameraEffect.STRONG_SHAKE_Y));
            }
            else if (targetGameObject?.tag == "Enemy")
            {
                shaker.StartCoroutine(shaker.Shake(CameraEffect.WEAK_SHAKE_X, CameraEffect.WEAK_SHAKE_Y));
            }
        }

        animator = targetGameObject.GetComponent<Animator>();

        if (targetGameObject.CompareTag("Enemy"))
        {
            isDestroy = true;
            animator?.SetTrigger("IsDestroy");
        }

        PlayerDestory(targetGameObject);

        OnEntityDied?.Invoke();
        Death(targetGameObject);
    }

    /// <summary>
    /// シールドを有効にする
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        // それぞれのコルーチンが同時に走り続けてバグになる可能性があるから
        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    /// <summary>
    /// 無敵状態のコルーチン
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator ShieldRoutine(float duration)
    {       
        isInvincible = true;
        shieldEffect.Activate();
        yield return new WaitForSeconds(duration);
        isInvincible = false;
        shieldEffect.Deactivate();
    }

    /// <summary>
    /// Playerが撃墜された際に、全ての行動を停止させる。
    /// </summary>
    /// <param name="targetGameObject"></param>
    private void PlayerDestory(GameObject targetGameObject)
    {
        if(targetGameObject.CompareTag("Player"))
        {
            spawnManager = GameObject.Find("SpawnManager")?.GetComponent<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.StopAllSpawn();
            }

            // 全てのEnemyShooterを停止
            foreach(var enemyShooter in FindObjectsOfType<EnemyShooter>())
            {
                enemyShooter.DisableShoothing();
            }

            // playerの制御も無効化
            var player = targetGameObject.GetComponent<Player>();
            player?.GetComponent<PlayerShooter>()?.DisableShoothing();
            player?.GetComponent<PlayerMovementController>()?.DisableControl();

            Debug.Log("`Playerが撃墜された。。。すべての行動を停止する。");
        }
    }
}