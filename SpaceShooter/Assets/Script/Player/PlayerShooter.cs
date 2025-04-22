using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �ˌ������N���X
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    /// <summary>
    /// �e�̎ˏo�^�C�v���`
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
        // ����̂�Find�ŒT��
        laserPool = GameObject.FindWithTag("LaserPool").GetComponent<LaserPool>();

        if(laserPool == null) { Debug.LogError("LaserPool Not Found"); }
        
    }

    /// <summary>
    ///  Queue��Stack�𗘗p�����I�u�W�F�N�g�v�[���Ȏˌ��̃��W�b�N
    /// </summary>
    public void HandleShooting()
    {
        // �X�y�[�X�L�[�܂��͍��N���b�N�������Ă���ԁA���Ԋu�Ŏˌ�����
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > enableShooting)
        {
            //�đ��U�܂ł̎��Ԃ�ݒ�
            enableShooting = Time.time + playerData.shotInterval;
            ShootLaser();
        }
    }

    /// <summary>
    /// �I�����ꂽ�ˏo�^�C�v�ɏ������Ďˌ�
    /// </summary>
    private void ShootLaser()
    {
        if(laserPool == null) { return; }

        // ������foreach��LaseData�Ɋi�[����Ă���ˏo�ʒu��؂�ւ�����悤�ɂ������B�B�B
        foreach (var position in BulletPosition())
        {
            var bulletPosition = transform.position + position;
            laserPool.Launch(bulletPosition, 0);
        }
    }

    /// <summary>
    /// �e�̎ˏo�ʒu�ƁA�ˏo�^�C�v�����߂�B
    /// </summary>
    /// <returns>�e�̎ˏo�ʒu��Ԃ�</returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) ? bulletPositionMap[bulletType] : new List<Vector3>();
    }

    /// <summary>
    /// �O�A�˂���莞�Ԃ̂ݗL���ɂ���
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateTripleShot(float duration)
    {
        StartCoroutine(TripleShotRoutine(duration));
    }

    /// <summary>
    /// �O�A�˂�L���ɂ���R���[�`��
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator TripleShotRoutine(float duration)
    {
        // ��莞�Ԏˌ��^�C�v���u�O�A�ˁv�ɂ���B���̌�u�P���ˌ��v�ɖ߂��B
        bulletType = BulletType.TRIPLE;
        yield return new WaitForSeconds(duration);
        bulletType =  BulletType.SINGLE;
    }
}
