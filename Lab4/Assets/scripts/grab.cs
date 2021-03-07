using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class grab : MonoBehaviour
{
    public SteamVR_Action_Vibration haptic;

    public SteamVR_Action_Boolean grabAction;
    bool grip = false;
    public bool resetLever =true;
    public bool resetSlider = true;
    FixedJoint joint;
    public bool Left = false;
    bool hapticReset = false;
    SteamVR_Input_Sources source;

    // Start is called before the first frame update
    void Start()
    {
        if (Left) source = SteamVR_Input_Sources.LeftHand;
        else source = SteamVR_Input_Sources.RightHand;

        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {

        if (grabAction.GetStateDown(SteamVR_Input_Sources.Any)) grip = true;
        if (grabAction.GetStateUp(SteamVR_Input_Sources.Any))
        {
            resetLever = true;
            resetSlider = true;
            grip = false;
            hapticReset = true;
   
            if (joint.connectedBody != null)
            {
                if (joint.connectedBody.gameObject.tag == "grabbableLever")
                {
                    joint.connectedBody.gameObject.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    joint.connectedBody.gameObject.transform.parent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
                Destroy(joint);
                joint = gameObject.AddComponent<FixedJoint>();
            }
        }
        
       
    }
            

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grabbable" && grip)
        {
            if (hapticReset)
            {
                haptic.Execute(0, 0.5f, 150, 75, source);
                hapticReset = false;
            }
            joint.connectedBody = other.GetComponent<Rigidbody>();
            resetSlider = false;
        }
        if ((other.gameObject.tag == "grabbableLever") && grip)
        {
            if (hapticReset)
            {
                haptic.Execute(0, 0.5f, 150, 75, source);
                hapticReset = false;
            }
            joint.connectedBody = other.GetComponent<Rigidbody>();
            resetLever = false;
        }
    }
}
