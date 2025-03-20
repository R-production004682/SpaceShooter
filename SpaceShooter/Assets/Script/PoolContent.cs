using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    ObjectPool pool;

    private void Start()
    {
        pool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ’e‚ðŒ©‚¹‚é
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    public void Show(Vector3 position, float angle)
    {
        transform.position = position;
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void Hide()
    {
        Debug.Assert(gameObject.activeInHierarchy);
        
        if(pool == null)
        {
            pool = transform.parent.GetComponent<ObjectPool>();
        }
        
        pool.Collect(this);
    }
}
