using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorCheck : MonoBehaviour
{
    public bool grounded = false;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(cam.transform.localPosition.x, 0, cam.transform.localPosition.z);
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Climbable" || other.tag == "floor")
        {
            grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Climbable" || other.tag == "floor")
        {
            grounded = false;
        }
    }
}
