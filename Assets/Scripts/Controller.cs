using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;

    public float m_LookSensitivity = 3f;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;
    public SteamVR_Action_Boolean m_Jump = null;
    public SteamVR_Action_Vector2 m_Rotation = null;

    private float m_Speed = 0.0f;

    private Rigidbody rigidBody;
    public CapsuleCollider col;
    public Transform m_CameraRig;
    public Transform m_Head;
    Vector3 movement = Vector3.zero;

    private HandControls handL;
    public BoxCollider handLCollider;
    private HandControls handR;
    public BoxCollider handRCollider;
    public float sens = 45f;
    public float maxSpring = 100f;

    public floorCheck floorcheck;
    public float airborneMovementDeduction = 2;

    public bool testGrav = true;

    public float jumpHeightForce = 300;
    public bool jumped = false;
    public GameObject character;

    public bool released = false; //on release push, make player collider smaller for few seconds
    private float releaseTime;
    public float releaseTimer = 2.0f;

    public Vector3 previousVel;
    public float angleResetTime = 0.5f;

    public bool enablePortalControl = false;



    // Start is called before the first frame update
    void Start()
    {
        //m_CameraRig = SteamVR_Render.Top().origin;
        //m_Head = SteamVR_Render.Top().head;

        rigidBody = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        releaseTime = releaseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAngle(); //incase player teleported, reset angle of player 
        HandleHead();
        HandleHand();
        HandleHeight();
        CalculateRotation();
        CalculateMovement();

        if (released && releaseTime < 0.0f)
        {
            releaseTime = releaseTimer;
            released = false;
        }
        else if (released) // and releaseTime is still goin
        {
            releaseTime -= Time.deltaTime;
        }

    }
    private void LateUpdate()
    {
        previousVel = rigidBody.velocity;   
    }

    private void HandleAngle()
    {
        if (this.transform.eulerAngles.x != 0 || this.transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, 0, angleResetTime), transform.eulerAngles.y,
                Mathf.Lerp(transform.eulerAngles.z, 0, angleResetTime));
        }
    }

    private void HandleHead()
    {
        Vector3 oldPosition = m_Head.position;
        Quaternion oldRotation = m_Head.rotation;

        transform.eulerAngles = new Vector3(0.0f, m_CameraRig.rotation.eulerAngles.y, 0.0f);

        m_Head.position = oldPosition;
        m_Head.rotation = oldRotation;
    }
    private void CalculateMovement()
    {
        Quaternion orientation = CalculateOrientation();
        movement = Vector3.zero;

        //current problem
        //1) some walls are not climbable - check
        //2) using two hands then releasing one hand slowly drops the player - check
        //3) two handed jump too strong - check
        //4) awkward airborne movement, change it to force with clamp 
        //5) holding grip then entering wall let the player hold the wall - check

        //6) release jumps are disturbed by player capsule collider
        //      - the moment release jump is made, shrink the collider
        //      - 
        if (handL && handR)
        {
            rigidBody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            transform.position += transform.rotation * (handL.Delta + handR.Delta) / 2;
        }
        else if (handL)
        {
            rigidBody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;

            if (handL) setJoint(handL, transform.rotation);


        }
        else if (handR)
        {
            rigidBody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            if (handR) setJoint(handR, transform.rotation);
        }
        else
        {
            if (testGrav) GetComponent<Rigidbody>().useGravity = true;
            if (m_MoveValue.axis.magnitude == 0) m_Speed = 0;
            else
            {
                m_Speed = m_MoveValue.axis.magnitude * m_Sensitivity;
                m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);


            }
            movement += orientation * (m_Speed * Vector3.forward);

            if (floorcheck.grounded) rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
            else //airborne
            {
                // rigidBody.velocity = new Vector3(rigidBody.velocity.x + movement.x / airborneMovementDeduction,
                // rigidBody.velocity.y, rigidBody.velocity.z + movement.z / airborneMovementDeduction);

                /*
                if (Mathf.Abs(movement.x / airborneMovementDeduction) < Mathf.Abs(rigidBody.velocity.x)) movement.x = rigidBody.velocity.x;
                else movement.x = movement.x / airborneMovementDeduction;
                if (Mathf.Abs(movement.z / airborneMovementDeduction) < Mathf.Abs(rigidBody.velocity.z)) movement.z = rigidBody.velocity.z;
                else movement.z = movement.z / airborneMovementDeduction;
                */

                if ((movement.x > 0 && rigidBody.velocity.x < 0) || (movement.x < 0 && rigidBody.velocity.x > 0)) movement.x = rigidBody.velocity.x + movement.x;
                else if (Mathf.Abs(rigidBody.velocity.x) < m_MaxSpeed / airborneMovementDeduction) movement.x = rigidBody.velocity.x + movement.x;
                else movement.x = rigidBody.velocity.x;


                if ((movement.z > 0 && rigidBody.velocity.z < 0) || (movement.z < 0 && rigidBody.velocity.z > 0)) movement.z = rigidBody.velocity.z + movement.z;
                else if (Mathf.Abs(rigidBody.velocity.z) < m_MaxSpeed / airborneMovementDeduction) movement.z = rigidBody.velocity.z + movement.z;
                else movement.z = rigidBody.velocity.z;


                rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
            }
        }

        if (jumped) jumped = false;
        
        if ((floorcheck.grounded && rigidBody.velocity.y < 0) || (rigidBody.velocity.y < -5))
        {
            jumped = false;
            character.GetComponent<footIK>().enabled = true;
        }
            
        if (m_Jump.GetStateDown(SteamVR_Input_Sources.Any))
        {
            jump();
            jumped = true;
            character.GetComponent<footIK>().enabled = false;
        }

        


        
    }

    private void CalculateRotation()
    {
        transform.RotateAround(m_Head.position, Vector3.up, m_Rotation.axis.x * m_LookSensitivity);
    }

    private void setJoint(HandControls hand, Quaternion orientation)
    {
        //movement += hand.Delta * sens;
        //rigidBody.AddForce(-hand.transform.position);
        transform.position += orientation * hand.Delta;
    }
    public void release(HandControls hand)
    {
        if (!handL && !handR) //only release push when both hands are released
        {
            released = true;
            HandleHeight();
            Vector3 force;
            if ((hand.Delta * sens).magnitude > maxSpring) force = transform.rotation * hand.Delta.normalized * maxSpring;
            else force = transform.rotation* hand.Delta * sens;
            rigidBody.AddForce(force);
        }
    }

    private void HandleHeight()
    {
        if (released || handL || handR)
        {
            Vector3 newCenter = m_Head.localPosition;
            col.center = newCenter;
            col.height = col.radius * 2;
        }
        else
        {
            float headHeight = Mathf.Clamp(m_Head.localPosition.y, 0, 2);
            col.height = headHeight;

            Vector3 newCenter = Vector3.zero;
            newCenter.y = col.height / 2;

            newCenter.x = m_Head.localPosition.x;
            newCenter.z = m_Head.localPosition.z;

            //newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

            col.center = newCenter;
        }
    }

    private void HandleHand()
    {
        if (released)
        {
            handLCollider.enabled = false;
            handRCollider.enabled = false;
        }
        else
        {
            handLCollider.enabled = true;
            handRCollider.enabled = true;
        }
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

    public void SetHandL(HandControls hand)
    {
        if (handL) handL.releasePoint();
        handL = hand;
    }
    public void SetHandR(HandControls hand)
    {
        if (handR) handR.releasePoint();
        handR = hand;
    }
    public void ClearHandL()
    {
        handL = null;
        if (testGrav) GetComponent<Rigidbody>().useGravity = true;
    }
    public void ClearHandR()
    {
        handR = null;
        if (testGrav) GetComponent<Rigidbody>().useGravity = true;
    }

    public void jump()
    {
        if (floorcheck.grounded) //only jump when grounded or maybe a double jump implementation
        {
            //add force up
            rigidBody.AddForce(new Vector3(0, jumpHeightForce, 0));

        }
    }

}
