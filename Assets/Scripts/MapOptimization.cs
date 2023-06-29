using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOptimization : MonoBehaviour
{
    public Transform player;
    public Transform mapLocation;
    public GameObject map;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((mapLocation.position - player.position).magnitude) > range) map.gameObject.SetActive(false);
        else map.gameObject.SetActive(true);
    }
}
