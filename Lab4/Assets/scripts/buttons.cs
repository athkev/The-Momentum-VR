using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class buttons : MonoBehaviour
{
    public GameObject highlightSource;
    public GameObject dialSource;
    public GameObject sliderSource;
    highlight highlight;
    dial dial;
    followByZ slider;

    GameObject selected;
    // Start is called before the first frame update
    void Start()
    {
        highlight = highlightSource.GetComponent<highlight>();
        dial = dialSource.GetComponent<dial>();
        slider = sliderSource.GetComponent<followByZ>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "trigger")
        {
            //do something
            Debug.Log("Pressed!");

            if (highlight.previousGameObject != null)
            {
                highlight.previousGameObject.localScale = new Vector3(1, 1, 1); //reset scale
                dial.initAngle = 0;
                dial.lastAngle = 0;
                dial.angle = 0;

                dial.gameObject.transform.eulerAngles = new Vector3(0, 0, 0); //reset dial rotation
                dial.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                slider.target.transform.localPosition = Vector3.zero; //reset slider
                highlight.previousGameObject.transform.parent.localPosition = Vector3.zero; //reset selected object position
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
               
    }
}
