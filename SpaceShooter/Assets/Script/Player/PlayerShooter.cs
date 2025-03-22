using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 射撃処理クラス
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private ObjectPool laserPool;
    private float enableShooting = -1f;

    private void Start()
    {
        // 初回のみFindで探す
        laserPool = GameObject.FindWithTag("ObjectPool").GetComponent<ObjectPool>();

        if(laserPool == null) { Debug.LogError("ObjectPool Not Found"); }
        
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

    private void ShootLaser()
    {
        if(laserPool == null) { return; }

        var laser = laserPool.Launch(this.transform.position, 0);
        if(laser == null) { return; }

        // ここでLaserクラスのメソッドを呼び出す（Updateで回しているため取得するだけでよい）
        laser.GetComponent<Laser>(); 
    }
}
