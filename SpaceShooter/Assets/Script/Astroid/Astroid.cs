using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3.0f;
    [SerializeField] private float fallSpeed = 5.0f;

    [SerializeField] private GameObject explosionPrefab;

    private Animator animator;

    private void Start()
    {
        explosionPrefab = Instantiate(explosionPrefab, this.transform);
        animator = explosionPrefab.GetComponent<Animator>();
        animator?.gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveHandler();
        AstroidRotate();
    }

    private void MoveHandler()
    {
        var astroidFallSpeed = fallSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * astroidFallSpeed);
    }

    private void AstroidRotate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            AudioManager.Instance?.PlayExplosion();
            animator?.gameObject.SetActive(true);
            Destroy(gameObject, 0.7f);
        }
    }
}
