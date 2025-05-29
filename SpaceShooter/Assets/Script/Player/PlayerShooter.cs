using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Constant;

/// <summary>
/// 射撃処理クラス
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    /// <summary>
    /// 弾の射出タイプを定義
    /// </summary>
    public enum BulletType { NONE, SINGLE, DOUBLE, TRIPLE }
    public BulletType bulletType;


    [SerializeField] private LaserData laserData;

    private float enableShooting = -1f;
    private bool isShoothing;
    private LaserPool laserPool;
    private Dictionary<BulletType, List<Vector3>> bulletPositionMap;

    private void Awake()
    {
        bulletPositionMap = new Dictionary<BulletType, List<Vector3>>()
        {
            { BulletType.SINGLE, laserData.singleBulletPosition },
            { BulletType.DOUBLE, laserData.doubleBulletPosition },
            { BulletType.TRIPLE, laserData.tripleBulletPosition }
        };
    }

    private void Start()
    {
        laserPool = LaserPool.Instance;
        if (laserPool == null)
        {
            Debug.LogError("LaserPool Not Found");
        }
        enableShooting = 0;
    }

    /// <summary>
    ///  Queueを利用したオブジェクトプールな射撃のロジック
    /// </summary>
    public void HandleShooting(PlayerData playerData)
    {
        if(!isShoothing) return;

        // スペースキーまたは左クリックを押している間、一定間隔で射撃する
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > enableShooting)
        {
            // 再装填までの時間を設定
            enableShooting = Time.time + playerData.shotInterval;
            ShootLaser();
        }
    }

    /// <summary>
    /// 選択された射出タイプに乗っ取って射撃
    /// </summary>
    private void ShootLaser()
    {
        if(laserPool == null) { return; }

        foreach (var position in BulletPosition())
        {
            var launchPosition = transform.position + position;
            laserPool.Launch(launchPosition, 0, Laser.LaserOwner.Player);
        }

        AudioManager.Instance?.PlayShoot();
    }

    /// <summary>
    /// 射出タイプを決める。
    /// </summary>
    /// <returns></returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) 
                       ? bulletPositionMap[bulletType] : new List<Vector3>();
    }

    /// <summary>
    /// 三連射を一定時間のみ有効にする
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateTripleShot(float duration)
    {
        StartCoroutine(TripleShotRoutine(duration));
    }

    /// <summary>
    /// 三連射を有効にするコルーチン
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator TripleShotRoutine(float duration)
    {
        // 一定時間射撃タイプを「三連射」にする。その後「単発射撃」に戻す。
        bulletType = BulletType.TRIPLE;
        yield return new WaitForSeconds(duration);
        bulletType =  BulletType.SINGLE;
    }

    public void EnableShooting() => isShoothing = true;

    public void DisableShoothing() => isShoothing = false;

}
