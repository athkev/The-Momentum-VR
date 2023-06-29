//========= Copyright 2016-2017, HTC Corporation. All rights reserved. ===========
using HTC.UnityPlugin.StereoRendering;
using UnityEngine;

[RequireComponent(typeof(StereoRenderer))]
public class PortalDoorCollideDetection : MonoBehaviour
{
    public Collider playerCollider;
    public PortalSet portalManger;
    private StereoRenderer stereoRenderer;
    public StereoRenderer otherPortal;

    

    /////////////////////////////////////////////////////////////////////////////////

    private void OnEnable()
    {
        stereoRenderer = GetComponent<StereoRenderer>();
    }

    public void flipSurface()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if hmd has collided with portal door, notify parent
        if (other == playerCollider)
        {
            Debug.Log("**" + stereoRenderer.gameObject.transform.parent.name);
            portalManger.OnPlayerEnter(stereoRenderer, otherPortal);
        }
    }
}
