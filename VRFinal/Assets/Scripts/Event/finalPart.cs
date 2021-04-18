using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalPart : MonoBehaviour
{
    private BoxCollider col;
    public Collider playerCol;
    private AudioSource audioSource;
    public Controller controller;

    public AudioClip audio_1;
    public AudioClip audio_2;



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
        controller.enablePortalControl = true;
        audioSource.clip = audio_1;
        audioSource.Play(0);
        yield return new WaitForSeconds(10f);
        audioSource.clip = audio_2;
        audioSource.Play(0);
    }
}