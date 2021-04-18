//========= Copyright 2016-2017, HTC Corporation. All rights reserved. ===========
using HTC.UnityPlugin.StereoRendering;
using System.Collections;
using UnityEngine;

public class PortalSet : MonoBehaviour
{
    public GameObject hmdRig;
    public GameObject hmdEye;
    public Controller Control;

    private bool isColliding = false;
    
    public void OnPlayerEnter(StereoRenderer stereoRenderer, StereoRenderer otherPortal)
    {
        if(!isColliding)
        {
            StartCoroutine(MovePlayer(stereoRenderer, otherPortal));
        }
    }

    IEnumerator MovePlayer(StereoRenderer stereoRenderer, StereoRenderer otherPortal)
    {
        isColliding = true;

        Debug.Log(stereoRenderer.gameObject.transform.parent.name);

        stereoRenderer.shouldRender = false;

        Vector3 previousVel = Control.previousVel;
        Vector3 enterVel = -stereoRenderer.transform.InverseTransformDirection(previousVel);
        Transform anch = stereoRenderer.anchorTransform.transform;
        if (stereoRenderer.anchorTransform.eulerAngles.x != 0 || otherPortal.anchorTransform.eulerAngles.x != 0)
        {
            Debug.Log("not perp");
            hmdRig.GetComponent<Rigidbody>().velocity = previousVel.magnitude * stereoRenderer.anchorForward;
        }
        else
        {
            hmdRig.GetComponent<Rigidbody>().velocity = new Vector3(anch.TransformDirection(enterVel).x,
            -anch.TransformDirection(enterVel).y,
            anch.TransformDirection(enterVel).z);
        }
        

        //hmdRig.GetComponent<Rigidbody>().velocity = stereoRenderer.anchorRot * -stereoRenderer.anchorTransform.forward + stereoRenderer.anchorTransform.TransformDirection(enterVel);

        // adjust rotation
        Quaternion rotEntryToExit = stereoRenderer.anchorRot * Quaternion.Inverse(stereoRenderer.canvasOriginRot);
        hmdRig.transform.rotation = rotEntryToExit * hmdRig.transform.rotation;
        //hmdRig.transform.eulerAngles = new Vector3(stereoRenderer.gameObject.transform.eulerAngles.x, hmdRig.transform.eulerAngles.y, stereoRenderer.gameObject.transform.eulerAngles.z);


        // POSITION
        // adjust position
        Vector3 posDiff = new Vector3(stereoRenderer.stereoCameraHead.transform.position.x - hmdEye.transform.position.x,
                                      stereoRenderer.stereoCameraHead.transform.position.y - hmdEye.transform.position.y,
                                      stereoRenderer.stereoCameraHead.transform.position.z - hmdEye.transform.position.z);
        //Vector3 offset = stereoRenderer.anchorRot * -otherPortal.transform.forward;
        Vector3 offset;
        

        if (stereoRenderer.anchorTransform.eulerAngles.x != 0 || otherPortal.anchorTransform.eulerAngles.x != 0)
        {
            hmdRig.transform.position = otherPortal.transform.position + stereoRenderer.anchorForward * 1.0f ;
        }
        else
        {
            offset =   stereoRenderer.anchorRot * -stereoRenderer.anchorTransform.forward;
            offset.Normalize();
            Vector3 camRigTargetPos = hmdRig.transform.position + posDiff;

            // assign the target position to camera rig

            hmdRig.transform.position = camRigTargetPos;
        }



        
        
        //hmdRig.transform.position = otherPortal.transform.TransformPoint(stereoRenderer.transform.InverseTransformPoint(hmdEye.transform.position));

        //hmdRig.transform.eulerAngles =
        //hmdRig.GetComponent<Rigidbody>().velocity = stereoRenderer.gameObject.transform.forward *Control.previousVel.magnitude;
        

        stereoRenderer.shouldRender = true;

        yield return new WaitForSeconds(0.1f);
        isColliding = false;
    }
}
