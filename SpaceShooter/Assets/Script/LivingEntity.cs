using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IHealth
{
    [SerializeField] protected int maxHealth;
    private SpawnManager spawnManager;

    protected int currentHealth;
    protected bool isInvincible;


    // UIに現在のHPを随時更新して表示したり、
    // 死亡時に何かをする（エフェクトやサウンド）時に便利になるようにイベントで定義（あったら便利そうだから一応定義）
    public UnityEvent<int,int> OnHealthChanged; // (現在HP, 最大HP)
    public UnityEvent OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// ダメージを与える処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="targetGameObject">ダメージを与える対象</param>
    public virtual void TakeDamage(int damage , GameObject targetGameObject)
    {
        Debug.Log(isInvincible);
        if(isInvincible || currentHealth <= 0) return; 

        currentHealth -= damage;

        // 他のクラスでOnHealthChangedに登録されたメソッドを実行
        OnHealthChanged?.Invoke(currentHealth, maxHealth);　

        if (currentHealth <= 0)
        {
            Death(targetGameObject);

            if(targetGameObject.CompareTag("Player"))
            {
                // Playerが死んだ場合、Enemyのスポーンを止める
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
                if(spawnManager != null) Debug.Log("Playerが死んだ。"); return; 
            }
        }
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void Death(GameObject gameObject)
    {
        Debug.Log($"{ gameObject.name } Death!");
        Destroy(gameObject);
    }
}