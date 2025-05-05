using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : Effect
{
    [SerializeField] private GameObject shieldEffectPrefab;
    private GameObject shieldEffectInstance;

    public override void Activate()
    {
        if(shieldEffectPrefab != null)
        {
            if (shieldEffectInstance == null && shieldEffectPrefab != null)
            {
                shieldEffectInstance = Instantiate(shieldEffectPrefab, transform.position, Quaternion.identity, this.transform);
            }
            shieldEffectInstance?.SetActive(true);
        }
    }

    public override void Deactivate() 
            => shieldEffectInstance?.SetActive(false);
}
