using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headIK : MonoBehaviour
{
    [SerializeField] public Transform rootObject, followObject;
    [SerializeField] public Vector3 positionOffset, rotationOffset, headBodyOffset, cameraOffset;

    public GameObject character;
    public GameObject bottom;
    private footIK footIK;
    public Vector3 L_foot, R_foot;
    // Start is called before the first frame update

    private void Start()
    {
        footIK = character.GetComponent<footIK>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        L_foot = footIK.L_foot;
        R_foot = footIK.R_foot;
        //rootObject.position = transform.position + headBodyOffset;
        // new Vector3(transform.position.x + headBodyOffset.x, Mathf.Abs(L_foot.y - R_foot.y) / 2, transform.position.z + headBodyOffset.z)
        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized ;

        transform.position = followObject.TransformPoint(positionOffset) + cameraOffset;
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
