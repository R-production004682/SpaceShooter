using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] public LaserData laserData;
    
    PoolContent poolContent;
    
    private void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();    
    }

    private void Update()
    {
        transform.Translate(Vector3.up * laserData.bulletSpeed * Time.deltaTime);

        if(transform.localPosition.y > 10)
        {
            poolContent.Hide();
        }
    }
}
