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


    // UI�Ɍ��݂�HP�𐏎��X�V���ĕ\��������A
    // ���S���ɉ���������i�G�t�F�N�g��T�E���h�j���ɕ֗��ɂȂ�悤�ɃC�x���g�Œ�`�i��������֗�����������ꉞ��`�j
    public UnityEvent<int,int> OnHealthChanged; // (����HP, �ő�HP)
    public UnityEvent OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// �_���[�W��^���鏈��
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="targetGameObject">�_���[�W��^����Ώ�</param>
    public virtual void TakeDamage(int damage , GameObject targetGameObject)
    {
        Debug.Log(isInvincible);
        if(isInvincible || currentHealth <= 0) return; 

        currentHealth -= damage;

        // ���̃N���X��OnHealthChanged�ɓo�^���ꂽ���\�b�h�����s
        OnHealthChanged?.Invoke(currentHealth, maxHealth);�@

        if (currentHealth <= 0)
        {
            Death(targetGameObject);

            if(targetGameObject.CompareTag("Player"))
            {
                // Player�����񂾏ꍇ�AEnemy�̃X�|�[�����~�߂�
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
                if(spawnManager != null) Debug.Log("Player�����񂾁B"); return; 
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
    /// ���S������
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void Death(GameObject gameObject)
    {
        Debug.Log($"{ gameObject.name } Death!");
        Destroy(gameObject);
    }
}