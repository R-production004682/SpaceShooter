using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserPool : MonoBehaviour
{
    [SerializeField] private int maxBullet = 30;
    [SerializeField] PoolContent content = null;

    Queue<PoolContent> bulletQueue;

    public static LaserPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 重複排除
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        bulletQueue = new Queue<PoolContent>();

        // 画面外に弾を事前に生成
        for (int i = 0; i < maxBullet; i++)
        {
            var templateObj = Instantiate(content);
            templateObj.transform.parent = transform;
            templateObj.transform.localPosition = Vector3.one * StartGenerateActorInfo.POSITION;
            bulletQueue.Enqueue(templateObj);
        }
    }

    /// <summary>
    /// 弾を射出する
    /// </summary>
    /// <param name="position">射出位置</param>
    /// <param name="angle">射出方向（角度）</param>
    /// <param name="laserOwner">弾を発射した主のタイプ</param>
    /// <returns></returns>
    public PoolContent Launch(Vector3 position, float angle, Laser.LaserOwner laserOwner)
    {
        if (bulletQueue.Count <= 0)
        {
            Debug.LogWarning("弾が足りません！");
            return null;
        }

        var temp = bulletQueue.Dequeue();
        temp.gameObject.SetActive(true);
        temp.ShowLaser(position, angle, laserOwner);
        return temp;
    }

    /// <summary>
    /// 弾を集める
    /// </summary>
    /// <param name="bullet">集める弾</param>
    public void Collect(PoolContent bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }
}
