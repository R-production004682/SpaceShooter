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
    /// �_���[�W��^���鏈��
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="targetGameObject">�_���[�W��^����Ώ�</param>
    public virtual void TakeDamage(int damage , GameObject targetGameObject)
    {
        if(isInvincible || currentHealth <= 0) return;

        currentHealth -= damage;

        if (targetGameObject.CompareTag("Player"))
        {
            // Player��UI�\����؂�ւ���
            uIManager?.UpdateLivesUI(currentHealth);
        }

        if (currentHealth <= 0)
        {
            HandleDeath(targetGameObject);
        }
    }

    /// <summary>
    /// ���S������
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
            // Player�����񂾏ꍇ�AEnemy�̃X�|�[�����~�߂�
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if (spawnManager != null)
            {
                Debug.Log("Player�����Ă��ꂽ...");
            }
        }

        Death(targetGameObject);
    }

    /// <summary>
    /// �V�[���h��L���ɂ���
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        // ���ꂼ��̃R���[�`���������ɑ��葱���ăo�O�ɂȂ�\�������邩��
        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    /// <summary>
    /// ���G��Ԃ̃R���[�`��
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