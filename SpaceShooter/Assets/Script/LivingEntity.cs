using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IHealth
{
    [SerializeField] protected int maxHealth;

    protected int currentHealth;
    protected bool isInvincible = false;


    // UIに現在のHPを随時更新して表示したり、
    // 死亡時に何かをする（エフェクトやサウンド）時に便利になるようにイベントで定義
    public UnityEvent<int,int> OnHealthChanged; // (現在HP, 最大HP)
    public UnityEvent OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public virtual void TakeDamage(int damage)
    {
        if(isInvincible || currentHealth <= 0) return;

        currentHealth -= damage;

        // 他のクラスでOnHealthChangedに登録されたメソッドを実行
        OnHealthChanged?.Invoke(currentHealth, maxHealth);　

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log($"{gameObject.name} Death!");

        // 他のクラスでOnDeathに登録されたメソッドを実行
        OnDeath?.Invoke();
    }
}