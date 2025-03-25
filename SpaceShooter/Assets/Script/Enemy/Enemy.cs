using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    PoolContent poolContent;

    private void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * enemyData.moveSpeed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
        }
    }
}
