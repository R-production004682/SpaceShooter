using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    Laser laser;
    LaserPool laserPool;

    private void Start()
    {
        laserPool = transform.parent.GetComponent<LaserPool>();
        laser = GetComponent<Laser>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ’e‚ğŒ©‚¹‚é
    /// </summary>
    /// <param name="position">ËoˆÊ’u</param>
    /// <param name="angle">’e‚ÌËo•ûŒüiŠp“xj</param>
    public void ShowLaser(Vector3 position, float angle, Laser.LaserOwner laserOwner)
    {
        transform.position = position;
        transform.eulerAngles = new Vector3(0, angle, 0);

        if(laser != null)
        {
            var direction = angle == 180f ? Vector3.down : Vector3.up;
            laser.SetDirection(direction);
            laser.SetOwner(laserOwner);
        }
    }

    /// <summary>
    /// ’e‚ğ‰B‚·
    /// </summary>
    public void Hide()
    {
        Debug.Assert(gameObject.activeInHierarchy);
        if (laserPool == null)
        {
            laserPool = transform.parent.GetComponent<LaserPool>();
        }

        laserPool?.Collect(this);
    }
}