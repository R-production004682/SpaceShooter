using UnityEngine;

public interface IHealth 
{
    void TakeDamage(int damage, GameObject gameObject);
    void Death(GameObject gameObject);
}
