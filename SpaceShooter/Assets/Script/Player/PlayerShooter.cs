using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


    [SerializeField] private PlayerData playerData;
    [SerializeField] private LaserData laserData;

    private float enableShooting = -1f;
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
        // 初回のみFindで探す
        laserPool = GameObject.FindWithTag("LaserPool").GetComponent<LaserPool>();

        if(laserPool == null) { Debug.LogError("LaserPool Not Found"); }
        
    }

    /// <summary>
    ///  QueueとStackを利用したオブジェクトプールな射撃のロジック
    /// </summary>
    public void HandleShooting()
    {
        // スペースキーまたは左クリックを押している間、一定間隔で射撃する
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > enableShooting)
        {
            //再装填までの時間を設定
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

        // ここのforeachでLaseDataに格納されている射出位置を切り替えられるようにしたい。。。
        foreach (var position in BulletPosition())
        {
            var bulletPosition = transform.position + position;
            laserPool.Launch(bulletPosition, 0);
        }
    }

    /// <summary>
    /// 弾の射出位置と、射出タイプを決める。
    /// </summary>
    /// <returns>弾の射出位置を返す</returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) ? bulletPositionMap[bulletType] : new List<Vector3>();
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
}
