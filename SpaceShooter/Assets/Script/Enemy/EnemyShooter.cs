using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public enum BulletType { NONE, SINGLE, DOUBLE, TRIPLE }
    public BulletType bulletType;

    [SerializeField] private LaserData laserData;
    [SerializeField] private float firstShotDelay = 0.3f; // �I�v�V�����F��ʂɓ����Ă��猂�܂ł̗P�\����

    private float enableShoothing = -1f;
    private bool isShoot;
    private bool isVisible;

    private LaserPool laserPool;
    private Dictionary<BulletType, List<Vector3>> bulletPositionMap;

    private void Awake()
    {
        laserPool = LaserPool.Instance;
        if (laserPool == null)
        {
            Debug.LogError("LaserPool Not Found");
        }

        bulletPositionMap = new Dictionary<BulletType, List<Vector3>>()
        {
            { BulletType.DOUBLE, laserData.doubleBulletPosition }
        };
    }

    private void Start()
    {
        isShoot = true;
        isVisible = false;
    }

    public void HandleShooting(EnemyData enemyData)
    {
        if (!isShoot || !isVisible) return;

        if (Time.time > enableShoothing)
        {
            enableShoothing = Time.time + enemyData.shotInterval;
            ShootLaser();
        }
    }

    /// <summary>
    /// �I�����ꂽ�ˏo�^�C�v�ɏ������Ďˌ�
    /// </summary>
    private void ShootLaser()
    {
        if (laserPool == null) return;

        foreach (var position in BulletPosition())
        {
            var bulletDirection = transform.position + position;
            laserPool.Launch(bulletDirection, 180f, Laser.LaserOwner.Enemy);
        }

        AudioManager.Instance?.PlayShoot();
    }

    /// <summary>
    /// �ˌ��̎ˏo�ʒu
    /// </summary>
    /// <returns>�ˏo�ʒu��Ԃ�</returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) ? bulletPositionMap[bulletType] : new List<Vector3>();
    }

    /// <summary>
    /// �ˌ�����߂�
    /// </summary>
    public void DisableShoothing()
    {
        isShoot = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;

        // �ŏ��̃V���b�g�������x�点��i��ʊO���猂���Ȃ��悤�ɒ����j
        enableShoothing = Time.time + firstShotDelay; 
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
