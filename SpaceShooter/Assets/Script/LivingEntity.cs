using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IHealth
{
    [SerializeField] protected int maxHealth;

    protected int currentHealth;
    protected bool isInvincible = false;


    // UI�Ɍ��݂�HP�𐏎��X�V���ĕ\��������A
    // ���S���ɉ���������i�G�t�F�N�g��T�E���h�j���ɕ֗��ɂȂ�悤�ɃC�x���g�Œ�`
    public UnityEvent<int,int> OnHealthChanged; // (����HP, �ő�HP)
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

        // ���̃N���X��OnHealthChanged�ɓo�^���ꂽ���\�b�h�����s
        OnHealthChanged?.Invoke(currentHealth, maxHealth);�@

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log($"{gameObject.name} Death!");

        // ���̃N���X��OnDeath�ɓo�^���ꂽ���\�b�h�����s
        OnDeath?.Invoke();
    }
}