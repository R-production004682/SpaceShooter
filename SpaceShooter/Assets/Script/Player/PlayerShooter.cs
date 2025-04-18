using UnityEngine;

/// <summary>
/// 射撃処理クラス
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private LaserPool laserPool;
    private float enableShooting = -1f;

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

    private void ShootLaser()
    {
        if(laserPool == null) { return; }

        // 現在のPlayerのポジションから、ちょっと高い位置からLaserを発射する
        var laserPosition = this.transform.position + new Vector3(0, 0.8f, 0);

        var laser = laserPool.Launch(laserPosition, 0);
        if(laser == null) { return; }

        // ここでLaserクラスのメソッドを呼び出す（Updateで回しているため取得するだけでよい）
        laser.GetComponent<Laser>(); 
    }
}
