using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    public bool IsDestroy => isDestroy;

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
        if (gameObject.tag == "Enemy")
        {
            uIManager?.AddScore(10);
        }
        Collider2D collider2D = gameObject.GetComponent<Collider2D>();
        collider2D.enabled = false;
        Destroy(gameObject, 2.5f);
    }


    private void HandleDeath(GameObject targetGameObject)
    {
        animator = GameObject.Find(targetGameObject.name).GetComponent<Animator>();

        if (targetGameObject.CompareTag("Enemy"))
        {
            isDestroy = true;
            animator?.SetTrigger("IsDestroy");
        }

        if (targetGameObject.CompareTag("Player"))
        {
            // Playerが死んだ場合、Enemyのスポーンを止める
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if (spawnManager != null)
            {
                Debug.Log("Playerが撃墜された...");
            }
        }

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
}