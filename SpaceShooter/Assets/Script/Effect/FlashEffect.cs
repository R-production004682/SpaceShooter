using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 2;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void Flash()
    
        {
        if(spriteRenderer != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    /// <summary>
    /// ”í’e‚ÉÔ‚Æ’ÊíF‚ğŒğŒİ‚É“_–Å‚³‚¹‚é
    /// </summary>
    /// <returns></returns>
    public IEnumerator FlashRoutine()
    {
        for(var i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
