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

    private void Start()
    {
        bulletQueue = new Queue<PoolContent>();

        // ‰æ–ÊŠO‚É’e‚ğ–‘O‚É¶¬
        for (int i = 0; i < maxBullet; i++)
        {
            var templateObj = Instantiate(content);
            templateObj.transform.parent = transform;
            templateObj.transform.localPosition = Vector3.one * StartGenerateActorInfo.POSITION;
            bulletQueue.Enqueue(templateObj);
        }
    }

    public PoolContent Launch(Vector3 position, float angle, Laser.LaserOwner laserOwner)
    {
        if (bulletQueue.Count <= 0) { return null; }

        var temp = bulletQueue.Dequeue();
        temp.gameObject.SetActive(true);
        temp.ShowLaser(position, angle, laserOwner);
        return temp;
    }

    public void Collect(PoolContent bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }
}
