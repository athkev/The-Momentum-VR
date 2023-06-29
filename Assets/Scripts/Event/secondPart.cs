using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondPart : MonoBehaviour
{
    private BoxCollider col;
    public Collider playerCol;
    private AudioSource audioSource;

    public AudioClip audio_1;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCol)
        {
            StartCoroutine(start());
        }
    }

    IEnumerator start()
    {
        audioSource.clip = audio_1;
        audioSource.Play(0);
        yield return new WaitForSeconds(10f);
    }
}
