using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    /// <summary>
    /// アイテムの種類を定義
    /// </summary>
    public enum ItemType { POWERUP, SHIELD, SPEEDUP };
    public ItemType itemType;

    [SerializeField] protected ItemData itemData;
    protected abstract float DropSpeed { get; }
    protected abstract void ApplyEffect(Player player);

    protected virtual void Update()
    {
        transform.Translate(Vector3.down * itemData.boostersItemDropSpeed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                ApplyEffect(player);
            }
            Destroy(this.gameObject);
        }
    }
}
