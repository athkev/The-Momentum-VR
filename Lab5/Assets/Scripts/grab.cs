using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class grab : MonoBehaviour
{
    public SteamVR_Action_Boolean grab_action;

    bool grabbing = false;
    bool grip = false;
    FixedJoint joint;

    Vector3 oldPos;
    Vector3 currentPos;
    Vector3 vel;


    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<FixedJoint>();
        currentPos = transform.position;
        oldPos = currentPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "props" && grip)
        {
            joint.connectedBody = other.GetComponent<Rigidbody>();
            grabbing = true;
        }

        if (!grip && grabbing)
        {
            grabbing = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = vel;
            Destroy(joint);

            joint = gameObject.AddComponent<FixedJoint>();
            
        }
    }

    private void OnJointBreak(float breakForce)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (grab_action.GetStateDown(SteamVR_Input_Sources.Any))
        {
            grip = true;
        }
        if (grab_action.GetStateUp(SteamVR_Input_Sources.Any))
        {
            grip = false;
        }


        currentPos = transform.position;
        vel = (currentPos - oldPos) / Time.deltaTime;

        oldPos = currentPos;

    }
}
