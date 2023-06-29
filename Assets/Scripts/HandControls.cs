using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandControls : MonoBehaviour
{
    public SteamVR_Action_Boolean grab = null;
    public Controller controller = null;

    public Vector3 Delta = Vector3.zero;
    public GameObject point = null;
    public SteamVR_TrackedObject hand;
    private List<GameObject> contactPoints = new List<GameObject>();

    private Vector3 lastPosition = Vector3.zero;
    [SerializeField] private bool Left = true;

    public BoxCollider boxCollider;
    public Vector3 grabPosition; //never change after initial grab
    public float releaseOffset = 1.0f;

    private bool haventFoundWall = false;
    public SteamVR_Action_Vibration m_vib;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = hand.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if (grab.GetStateDown(SteamVR_Input_Sources.Any)) grabPoint();
        //if (grab.GetStateUp(SteamVR_Input_Sources.Any)) releasePoint();
        //handleHand();

        if (grab.GetStateDown(SteamVR_Input_Sources.Any))
        {
            grabPoint();
        }
        if (grab.GetStateUp(SteamVR_Input_Sources.Any))
        {
            releasePoint();
        }

        //FIX HERE 
        // IF the hand position is off by few distance release
        //https://forum.unity.com/threads/how-do-i-get-the-distance-between-two-gameobjects.77331/
        //if (point && transform.position ) 
        if (point)
        {
            if (Vector3.Distance(grabPosition, transform.position) > releaseOffset)
            {
                Debug.Log("faraway from wall, releasing");
                releasePoint();
            }
        }

        Delta = (lastPosition - hand.transform.localPosition);
        lastPosition = hand.transform.localPosition;

    }

    private void handleHand()
    {
        if (controller.released)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }

    }
    private void grabPoint()
    {
        point = GetNearest(transform.position, contactPoints);
        if (point)
        {
            if (Left)
            {
                controller.SetHandL(this);
                m_vib.Execute(0, 0.1f, 50, 0.5f, SteamVR_Input_Sources.LeftHand);
            }
            else
            {
                controller.SetHandR(this);
                m_vib.Execute(0, 0.1f, 50, 0.5f, SteamVR_Input_Sources.LeftHand);
            }
            boxCollider.enabled = false;


            Debug.Log("Grabbed");

            //get intial position of grabbed point
            //make sure the hand stays on that position 
            grabPosition = hand.transform.position;
        }
        else
        {
            haventFoundWall = true;
        }
    }
    public void releasePoint()
    {
        if (point)
        {
            if (Left) controller.ClearHandL();
            else controller.ClearHandR();
            Debug.Log("Released");
            //boxCollider.enabled = true ;
            controller.release(this);
        }

        point = null;
        haventFoundWall = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        AddPoint(other.gameObject);
        if (haventFoundWall)
        {
            haventFoundWall = false;
            grabPoint();
        }
    }

    private void AddPoint(GameObject newObject)
    {
        if (newObject.CompareTag("Climbable")) contactPoints.Add(newObject);
    }

    private void OnTriggerExit(Collider other)
    {
        RemovePoint(other.gameObject);
    }
    private void RemovePoint(GameObject newObject)
    {
        if (newObject.CompareTag("Climbable")) contactPoints.Remove(newObject);
    }

    public static GameObject GetNearest(Vector3 origin, List<GameObject> collection)
    {
        GameObject nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (GameObject entity in collection)
        {
            distance = (entity.gameObject.transform.position - origin).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = entity;
            }
        }

        return nearest;
    }
}