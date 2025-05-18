using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip powerupClip;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootClip);
    }

    public void PlayExplosion()
    {
        audioSource.PlayOneShot(explosionClip);
    }

    public void PlayPowerUp()
    {
        audioSource.PlayOneShot(powerupClip);
    }
}
