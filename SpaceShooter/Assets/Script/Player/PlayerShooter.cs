using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Constant;

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
    ///  Queue�𗘗p�����I�u�W�F�N�g�v�[���Ȏˌ��̃��W�b�N
    /// </summary>
    public void HandleShooting(PlayerData playerData)
    {
        if(!isShoothing) return;

        // �X�y�[�X�L�[�܂��͍��N���b�N�������Ă���ԁA���Ԋu�Ŏˌ�����
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > enableShooting)
        {
            // �đ��U�܂ł̎��Ԃ�ݒ�
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

        foreach (var position in BulletPosition())
        {
            var launchPosition = transform.position + position;
            laserPool.Launch(launchPosition, 0, Laser.LaserOwner.Player);
        }

        AudioManager.Instance?.PlayShoot();
    }

    /// <summary>
    /// �ˏo�^�C�v�����߂�B
    /// </summary>
    /// <returns></returns>
    private List<Vector3> BulletPosition()
    {
        return bulletPositionMap.ContainsKey(bulletType) 
                       ? bulletPositionMap[bulletType] : new List<Vector3>();
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

    public void EnableShooting() => isShoothing = true;

    public void DisableShoothing() => isShoothing = false;

}
