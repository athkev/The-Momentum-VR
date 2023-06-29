using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public float speedThreshold = 0;
    [Range(0, 1)]
    public float smooth = 0.3f;
    private Animator thisAnimator;
    public Animator mimicAnimator;
    private Vector3 previousPos;
    public Transform VRRig;
    public Rigidbody rig;
    public Controller cont;
    public floorCheck floor;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        previousPos = VRRig.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 headsetSpeed = (VRRig.position - previousPos) / Time.deltaTime;
        Vector3 headsetSpeed = rig.velocity;
        headsetSpeed.y = 0;

        Vector3 headsetLocalSpeed = VRRig.InverseTransformDirection(headsetSpeed);

        float previousDirectionX = thisAnimator.GetFloat("DirectionX"); 
        float previousDirectionY = thisAnimator.GetFloat("DirectionY");


        thisAnimator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        thisAnimator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -2, 2), smooth));
        thisAnimator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -2, 2), smooth));

        thisAnimator.SetBool("jump", cont.jumped);

        thisAnimator.SetBool("grounded", floor.grounded);

        mimicAnimator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        mimicAnimator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -2, 2), smooth));
        mimicAnimator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -2, 2), smooth));

        mimicAnimator.SetBool("jump", cont.jumped);

        mimicAnimator.SetBool("grounded", floor.grounded);
    }
}
