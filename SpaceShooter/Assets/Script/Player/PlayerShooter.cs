using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ˌ������N���X
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private ObjectPool laserPool;
    private float enableShooting = -1f;

    private void Start()
    {
        // ����̂�Find�ŒT��
        laserPool = GameObject.FindWithTag("ObjectPool").GetComponent<ObjectPool>();

        if(laserPool == null) { Debug.LogError("ObjectPool Not Found"); }
        
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

    private void ShootLaser()
    {
        if(laserPool == null) { return; }

        var laser = laserPool.Launch(this.transform.position, 0);
        if(laser == null) { return; }

        // ������Laser�N���X�̃��\�b�h���Ăяo���iUpdate�ŉ񂵂Ă��邽�ߎ擾���邾���ł悢�j
        laser.GetComponent<Laser>(); 
    }
}
