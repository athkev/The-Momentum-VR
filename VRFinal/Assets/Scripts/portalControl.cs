using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class portalControl : MonoBehaviour
{
    public Transform portal;
    public Transform portalSurface;
    public Transform handpos;
    public bool enablePortal;
    public SteamVR_Action_Boolean m_trigger;
    public SteamVR_Action_Boolean m_guideLine;
    public LayerMask IgnoreMe;
    public float range = 30f;
    public float Yoffset = -1.0f;

    public bool held = false;
    public LineRenderer line;

    public bool enablePortalControl = false; //do not let player use portals until certain point
    public Controller controller;
    private AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        enablePortalControl = controller.enablePortalControl;
        if (enablePortalControl)
        {
            //if player presses trigger to correct surface (raycast)
            //enablePortal

            if (m_trigger.GetStateDown(SteamVR_Input_Sources.Any))
            {
                shoot();
            }
        //if player presses a button, disable portal

        //if player presses a button, shoot a guide line
        
            if (m_guideLine.GetStateDown(SteamVR_Input_Sources.Any))
            {
                held = true;
                line.gameObject.SetActive(true);
            }
            if (m_guideLine.GetStateUp(SteamVR_Input_Sources.Any))
            {
                held = false;
                line.gameObject.SetActive(false);
            }

            if (held)
            {
                guideLine();
            }

        }
        
    }
    private void shoot()
    {
        if (!portal.gameObject.active) portal.gameObject.SetActive(true);
        audioS.Play(0);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(handpos.transform.position, -handpos.transform.up, out hit, range, IgnoreMe);
        if (hasHit)
        {
            portal.forward = -hit.normal;
            if (portal.eulerAngles.x != 0)
            {
                Debug.Log(portal.eulerAngles.x);
                
            }

            portal.position = hit.point + (portal.up * Yoffset) +(-portal.forward * 0.1f);
        }
    }
    private void guideLine()
    {
        RaycastHit hit;
        line.SetPosition(0, handpos.transform.position);
        if (Physics.Raycast(handpos.transform.position, -handpos.transform.up, out hit, range))
        {
            line.SetPosition(1, hit.point);
        }
        else { line.SetPosition(1, handpos.transform.position + (-handpos.transform.up * range)); }
    }
    private void removeGuideLine()
    {

    }
}
