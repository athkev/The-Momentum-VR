using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstep : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public float rayLength = 0.1f;
    public AudioSource footstepSound;

    public bool steped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  only works when there is no footik 
        RaycastHit hit;
        bool hasHit = Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, IgnoreMe);
        if (hasHit && !steped)
        {
            steped = true;
            footstepSound.Play(0);
        }
        //FIX: walk animation does not have feet up
        //
        //
        //
        else if (!hasHit) //airborne or feet not touching the ground
        {
            //reset steped
            steped = false;
        }
        
    }
}
