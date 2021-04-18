using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private BoxCollider col;
    public Collider playerCol;
    public AudioSource audioSource;

    public Animator doorAnim;
    public AudioClip audio_open;
    public AudioClip audio_close;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCol)
        {
            doorAnim.SetBool("PlayerNear", true);
            audioSource.clip = audio_open;
            audioSource.Play(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == playerCol)
        {
            doorAnim.SetBool("PlayerNear", false);
            audioSource.clip = audio_close;
            audioSource.Play(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
