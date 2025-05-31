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
            Destroy(gameObject); // �d���r��
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        bulletQueue = new Queue<PoolContent>();

        // ��ʊO�ɒe�����O�ɐ���
        for (int i = 0; i < maxBullet; i++)
        {
            var templateObj = Instantiate(content);
            templateObj.transform.parent = transform;
            templateObj.transform.localPosition = Vector3.one * StartGenerateActorInfo.POSITION;
            bulletQueue.Enqueue(templateObj);
        }
    }

    /// <summary>
    /// �e���ˏo����
    /// </summary>
    /// <param name="position">�ˏo�ʒu</param>
    /// <param name="angle">�ˏo�����i�p�x�j</param>
    /// <param name="laserOwner">�e�𔭎˂�����̃^�C�v</param>
    /// <returns></returns>
    public PoolContent Launch(Vector3 position, float angle, Laser.LaserOwner laserOwner)
    {
        if (bulletQueue.Count <= 0)
        {
            Debug.LogWarning("�e������܂���I");
            return null;
        }

        var temp = bulletQueue.Dequeue();
        temp.gameObject.SetActive(true);
        temp.ShowLaser(position, angle, laserOwner);
        return temp;
    }

    /// <summary>
    /// �e���W�߂�
    /// </summary>
    /// <param name="bullet">�W�߂�e</param>
    public void Collect(PoolContent bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }
}
