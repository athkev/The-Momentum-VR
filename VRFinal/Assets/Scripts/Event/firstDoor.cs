using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstDoor : MonoBehaviour
{
    public GameObject door;
    private BoxCollider col;
    public Collider playerCol;

    public bool ended = false;

    private AudioSource audioSource;
   
    public AudioClip audio_1;
    public AudioClip audio_2;
    public AudioClip audio_3;

    public Animator doorAnim;
    public AudioSource doorAudioSource;
    public AudioClip audio_open;
    public AudioClip audio_close;

    public GameObject mirror;
    // Start is called before the first frame update
    void Start()
    {
        ended = false;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FirstScenario());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!ended)
        {
            StartCoroutine(FirstScenario());
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCol && ended)
        {
            doorAnim.SetBool("PlayerNear", true);
            doorAudioSource.clip = audio_open;
            doorAudioSource.Play(0);

            if (!mirror.active)
            {
                mirror.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCol)
        {
            doorAnim.SetBool("PlayerNear", false);
            doorAudioSource.clip = audio_close;
            doorAudioSource.Play(0);

            if (ended) //disable mirror for optimization
            {
                mirror.SetActive(false);
            }
        }
    }

    IEnumerator FirstScenario()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = audio_1;
        audioSource.Play(0);
        yield return new WaitForSeconds(10f);
        audioSource.clip = audio_2;
        audioSource.Play(0);
        yield return new WaitForSeconds(9f);
        audioSource.clip = audio_3;
        audioSource.Play(0);
        yield return new WaitForSeconds(1f);
        ended = true;
        doorAnim.SetBool("PlayerNear", true);
        doorAnim.SetBool("EndOfDialogue", true);
        doorAudioSource.clip = audio_open;
        doorAudioSource.Play(0);
        yield return new WaitForSeconds(8f);
        
    }
}
