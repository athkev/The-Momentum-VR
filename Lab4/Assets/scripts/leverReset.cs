using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverReset : MonoBehaviour
{
    public Transform lever;
    public Transform slider;
    public Transform sliderParent;
    public GameObject hand;
    public GameObject handL;

    grab l;
    grab r;
    // Start is called before the first frame update
    void Start()
    {
        l = handL.GetComponent<grab>();
        r = hand.GetComponent<grab>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l.resetLever && r.resetLever)
        {
            lever.transform.position = lever.transform.parent.position;
            lever.transform.rotation = lever.transform.parent.rotation;
        }
        if (l.resetSlider && r.resetSlider)
        {
            slider.transform.position = sliderParent.transform.position;
            slider.transform.rotation = sliderParent.transform.rotation;
        }

    }
}
