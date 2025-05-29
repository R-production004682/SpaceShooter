using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackedDisplay : MonoBehaviour
{
    [Range(0, 1)]
    public float crackIntensity = 0.0f;

    [SerializeField] private Material crackedDisplayMaterial;
    [SerializeField] private RawImage rawImage;

    private Color color;

    private void Start()
    {
        color.a = 0;
    }

    public void SetRedFog(float intensity)
    {
        crackedDisplayMaterial.SetFloat("_RedFogIntensity", Mathf.Clamp01(intensity));
    }

    public void SetCrack(float value)
    {
        crackIntensity = Mathf.Clamp01(value);
        crackedDisplayMaterial.SetFloat("_CrackIntensity", crackIntensity);

        color = rawImage.color;
        color.a = crackIntensity;
        rawImage.color = color;
    }

    public void ExplodeScreen()
    {
        // Œ‚”j‚³‚ê‚½‚çˆê‹C‚ÉŠ„‚é
        SetCrack(1.0f);
        SetRedFog(1.0f);
    }
}
