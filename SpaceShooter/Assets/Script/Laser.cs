using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Laser : MonoBehaviour
{
    [SerializeField] public LaserData laserData;
    
    private PoolContent poolContent;
    private Vector3 moveDirection;

    /// <summary>
    /// �ǂ���̋@�̂���ˏo���ꂽ�̂�����������Enum
    /// </summary>
    public enum LaserOwner
    {
        Player,
        Enemy
    }

    private LaserOwner owner;


    private void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
    }

    private void Update()
    {
        transform.Translate(moveDirection * laserData.bulletSpeed * Time.deltaTime);

        // ��ʊO�ɏo����e���B��
        if (transform.localPosition.y > 10 || transform.localPosition.y < -10)
        {
            poolContent.Hide();
        }
    }

    public void SetOwner(LaserOwner laserOwner)
    {
        owner = laserOwner;
    }


    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"LaserHit : {other.gameObject.tag},{ other.gameObject.name}");
        switch (owner)
        {
            case LaserOwner.Player:
                if (other.CompareTag("Enemy"))
                {
                    var target = other.GetComponent<IHealth>();
                    target?.TakeDamage(laserData.giveDamage, other.gameObject);
                    poolContent.Hide();
                }
                break;

            case LaserOwner.Enemy:
                if (other.CompareTag("Player"))
                {
                    var target = other.GetComponent<IHealth>();
                    target?.TakeDamage(laserData.giveDamage, other.gameObject);
                    poolContent.Hide();
                }
                break;

            default:
                break;
        }
    }
}
