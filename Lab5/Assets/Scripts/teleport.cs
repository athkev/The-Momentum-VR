using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class teleport : MonoBehaviour
{
    public Transform head;
    public Transform rig;
    public SteamVR_Action_Boolean teleport_action;
    public SteamVR_Action_Boolean teleportInitialize_action;
    public GameObject teleport_point;
    public LineRenderer guideline;

    bool teleportHeld = false;

    float fadeduration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        teleport_point.SetActive(false);
        guideline.gameObject.SetActive(false);
    }

    void test()
    {
        //create a guideline
        guideline.gameObject.SetActive(true);
        guideline.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "floor") //if raycast hits the floor tag
            {
                if (!teleport_point.activeSelf) teleport_point.SetActive(true);
                teleport_point.transform.position = hit.point; //update pointer position on ray hitpoint

                guideline.SetPosition(1, teleport_point.transform.position);
            }
            else guideline.SetPosition(1, transform.position + transform.forward.normalized * 100);
        }
        else guideline.SetPosition(1, transform.position + transform.forward.normalized * 100);
    }

    void move()
    {
        //move rig position related to head position
        Vector3 offset = teleport_point.transform.position - new Vector3(head.position.x, 0, head.position.z);
        rig.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (teleport_action.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (!teleport_point.activeSelf) teleport_point.SetActive(true);
            teleportHeld = true;
        }

        if (teleportHeld) test();
        if (teleport_action.GetStateUp(SteamVR_Input_Sources.Any)) //disable pointer
        {
            if (teleport_point.activeSelf) teleport_point.SetActive(false);
            if (guideline.gameObject.activeSelf) guideline.gameObject.SetActive(false); 
            teleportHeld = false;
        }

        if (teleportHeld && teleportInitialize_action.GetStateUp(SteamVR_Input_Sources.Any))
        {
            //fade in
            fadein();

            //teleport function
            Invoke("move", fadeduration);

            //fade out
            Invoke("fadeout", fadeduration);

        }
    }

    void fadein()
    {
        SteamVR_Fade.Start(Color.clear, 0f);
        SteamVR_Fade.Start(Color.black, fadeduration);
    }
    void fadeout()
    {
        SteamVR_Fade.Start(Color.black, 0f);
        SteamVR_Fade.Start(Color.clear, fadeduration);
    }
}
