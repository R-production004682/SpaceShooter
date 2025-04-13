using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] PoolContent content = null;
    Queue<PoolContent> bulletQueue;

    [SerializeField] private int maxBullet = 30; // ���O�ɐ������Ă����e�̐�

    private void Start()
    {
        bulletQueue = new Queue<PoolContent>();

        for (int i = 0; i < maxBullet; i++)
        {
            var templateObj = Instantiate(content);
            templateObj.transform.parent = transform;

            // ��ʊO�ɔz�u
            templateObj.transform.localPosition = Vector3.one * 100;
            bulletQueue.Enqueue(templateObj);
        }
    }

    public PoolContent Launch(Vector3 position, float angle)
    {
        if(bulletQueue.Count <= 0) { return null; }
        
        var temp = bulletQueue.Dequeue();
        temp.gameObject.SetActive(true);
        temp.ShowLaser(position, angle);
        return temp;
    }

    public void Collect(PoolContent bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }
}
