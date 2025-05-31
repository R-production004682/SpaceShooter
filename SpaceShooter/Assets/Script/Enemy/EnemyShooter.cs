using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public enum BulletType { NONE, SINGLE, DOUBLE, TRIPLE }
    public BulletType bulletType;

    [SerializeField] private LaserData laserData;
    [SerializeField] private float firstShotDelay = 0.3f; // オプション：画面に入ってから撃つまでの猶予時間

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
    /// 選択された射出タイプに乗っ取って射撃
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
    /// 射撃の射出位置
    /// </summary>
    /// <returns>射出位置を返す</returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) ? bulletPositionMap[bulletType] : new List<Vector3>();
    }

    /// <summary>
    /// 射撃をやめる
    /// </summary>
    public void DisableShoothing()
    {
        isShoot = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;

        // 最初のショットを少し遅らせる（画面外から撃たないように調整）
        enableShoothing = Time.time + firstShotDelay; 
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
