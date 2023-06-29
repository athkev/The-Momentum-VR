using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.StereoRendering;

public class portalDestination : MonoBehaviour
{
    public GameObject parentPortal;
    public GameObject targetPortal;
    public GameObject disablePlane; //shown when portal is not yet opened
    public float offset = 1.0f;
    private StereoRenderer stereoRenderer;
    public portalControl control;

    // Start is called before the first frame update
    void Start()
    {
        stereoRenderer = parentPortal.GetComponent<StereoRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!control.enablePortal)
        {
            //disable stereo renderer, maybe set texture
            parentPortal.SetActive(false);
            disablePlane.SetActive(true);
        }
        else
        {
            if (!parentPortal.active)
            {
                parentPortal.SetActive(true);
            }
            if (disablePlane.active)
            {
                disablePlane.SetActive(false);
            }
            this.transform.rotation = Quaternion.Euler(-targetPortal.transform.eulerAngles.x, targetPortal.transform.eulerAngles.y+180f, -targetPortal.transform.eulerAngles.z);
            this.transform.position = targetPortal.transform.position;

            this.transform.position += transform.forward * offset;
        }
    }
}
