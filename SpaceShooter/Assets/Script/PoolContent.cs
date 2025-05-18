using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    LaserPool laserPool;
    Laser laser;

    private void Start()
    {
        laserPool = transform.parent.GetComponent<LaserPool>();
        laser = GetComponent<Laser>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ’e‚ðŒ©‚¹‚é
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
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