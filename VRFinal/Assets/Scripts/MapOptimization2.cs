using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOptimization2 : MonoBehaviour
{
    public Collider player;

    public GameObject map;
    public GameObject othermap;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            map.SetActive(true);
            othermap.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
