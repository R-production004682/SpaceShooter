using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    /// <summary>
    /// 弾の射出タイプを定義
    /// </summary>
    public enum BulletType { NONE, SINGLE, DOUBLE, TRIPLE }
    public BulletType bulletType;

    [SerializeField] private LaserData laserData;

    private float enableShoothing = -1f;
    private bool isShoot;
    private LaserPool laserPool;
    private Dictionary<BulletType, List<Vector3>> bulletPositionMap;

    private void Awake()
    {
        bulletPositionMap = new Dictionary<BulletType, List<Vector3>>()
        {
            { BulletType.DOUBLE, laserData.doubleBulletPosition }
        };
    }

    private void Start()
    {
        laserPool = GameObject.FindWithTag("LaserPool").GetComponent<LaserPool>();
        if (laserPool == null)
        {
            Debug.LogError("LaserPool Not Found");
        }

        isShoot = true;
    }

    public void HandleShooting(EnemyData enemyData)
    {
        if(!isShoot) return;

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
        if (laserPool == null) { return; }

        foreach (var position in BulletPosition())
        {
            var bulletDirection = transform.position + position;
            laserPool.Launch(bulletDirection, 180f, Laser.LaserOwner.Enemy);
        }

        AudioManager.Instance?.PlayShoot();
    }

    /// <summary>
    /// 弾の射出位置と射出タイプを決める。
    /// </summary>
    /// <returns></returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) ?
               bulletPositionMap[bulletType] : new List<Vector3>();
    }

    public void DisableShoothing()
    {
        isShoot = false;
    }

}