using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCatSound : MonoBehaviour
{
    public AudioClip animalSound;           // Clip de sonido del animal
    public float minDelay = 5f;             // Tiempo mínimo entre sonidos
    public float maxDelay = 10f;            // Tiempo máximo entre sonidos

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundRandomly());
    }

    IEnumerator PlaySoundRandomly()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio entre minDelay y maxDelay
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            // Reproducir el sonido si hay uno asignado
            if (animalSound != null)
            {
                audioSource.PlayOneShot(animalSound);
            }
        }
    }
}
