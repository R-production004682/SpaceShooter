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

    public void Update()
    {
        transform.Translate(Vector3.up * laserData.bulletSpeed * Time.deltaTime);

        // ‰æ–ÊŠO‚Éo‚½‚ç’e‚ð‰B‚·
        if (transform.localPosition.y > 10)
        {
            poolContent.Hide();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"LaserHit : { other.gameObject.name }");
        if (other.gameObject.CompareTag("Enemy"))
        {
            IHealth target = other.GetComponent<IHealth>();
            if (target != null)
            {
                target.TakeDamage(laserData.giveDamage, other.gameObject);
            }
            poolContent.Hide();
        }
    }
}
