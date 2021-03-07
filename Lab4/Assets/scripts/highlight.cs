using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class highlight : MonoBehaviour
{
    //controllers
    public Transform leftController;
    public Transform rightController;

    public SteamVR_Action_Boolean guideAction;
    public SteamVR_Action_Boolean guideActionL;
    public SteamVR_Action_Boolean selectAction;
    public SteamVR_Action_Boolean selectActionL;

    public Material selectedObjectMaterial;
    public Material previousMaterial;
    public Transform previousGameObject;
    public LineRenderer line;
    public LineRenderer lineL;

    bool guideDown = false;
    bool guideDownL = false;

    // Start is called before the first frame update
    void Start()
    {
        line.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectAction.GetStateUp(SteamVR_Input_Sources.Any))
        {
            if (previousGameObject != null) previousGameObject.GetComponent<MeshRenderer>().material = previousMaterial;
            selectTest(rightController);
        }
        else if (selectActionL.GetStateUp(SteamVR_Input_Sources.Any))
        {
            if (previousGameObject != null) previousGameObject.GetComponent<MeshRenderer>().material = previousMaterial;
            selectTest(leftController);
        }

        if (guideAction.GetStateDown(SteamVR_Input_Sources.Any)) guideDown = true;
        else if (guideActionL.GetStateDown(SteamVR_Input_Sources.Any)) guideDownL = true;

        if (guideDown)
        {
            line.gameObject.SetActive(true);
            line.SetPosition(0, rightController.position);
            line.SetPosition(1, rightController.position + rightController.forward.normalized * 100);
        }
        if (guideDownL)
        {
            lineL.gameObject.SetActive(true);
            lineL.SetPosition(0, leftController.position);
            lineL.SetPosition(1, leftController.position + leftController.forward.normalized * 100);
        }

        if (guideAction.GetStateUp(SteamVR_Input_Sources.Any))
        {
            guideDown = false;
            line.gameObject.SetActive(false);
        }

        if (guideActionL.GetStateUp(SteamVR_Input_Sources.Any))
        {
            guideDownL = false;
            lineL.gameObject.SetActive(false);
        }
    }


    void selectTest(Transform controller)
    {

        Ray ray = new Ray(controller.position, controller.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            
            if (hit.transform.tag == "object")
            {
                previousGameObject = hit.transform;
                previousMaterial = hit.transform.GetComponent<MeshRenderer>().material;
                changeMat(hit.transform, selectedObjectMaterial);
            }
        }
        else
        {
            previousGameObject = null;
            previousMaterial = null;
        }
        
    }

    void changeMat(Transform obj, Material mat)
    {
        obj.GetComponent<MeshRenderer>().material = selectedObjectMaterial;
    }
}
