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
    /// �_���[�W��^���鏈��
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="targetGameObject">�_���[�W��^����Ώ�</param>
    public virtual void TakeDamage(int damage , GameObject targetGameObject)
    {
        if(isInvincible || currentHealth <= 0) return;
        currentHealth -= damage;

        var flash = targetGameObject.GetComponent<FlashEffect>();
        flash?.Flash();

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

    /// <summary>
    /// Player�����Ă��ꂽ�ۂɁA�S�Ă̍s�����~������B
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

            // �S�Ă�EnemyShooter���~
            foreach(var enemyShooter in FindObjectsOfType<EnemyShooter>())
            {
                enemyShooter.DisableShoothing();
            }

            // player�̐����������
            var player = targetGameObject.GetComponent<Player>();
            player?.GetComponent<PlayerShooter>()?.DisableShoothing();
            player?.GetComponent<PlayerMovementController>()?.DisableControl();

            Debug.Log("`Player�����Ă��ꂽ�B�B�B���ׂĂ̍s�����~����B");
        }
    }
}