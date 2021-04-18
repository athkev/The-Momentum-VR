using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, -0.04f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
        transform.rotation = target.transform.rotation;
    }
}
