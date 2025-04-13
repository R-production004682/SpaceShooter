using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    LaserPool laserPool;

    private void Start()
    {
        laserPool = transform.parent.GetComponent<LaserPool>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ’e‚ðŒ©‚¹‚é
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    public void ShowLaser(Vector3 position, float angle)
    {
        transform.position = position;
        transform.eulerAngles = new Vector3(0, angle, 0);
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
