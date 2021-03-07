using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ui : MonoBehaviour
{

    public TextMeshProUGUI leverUI;
    public TextMeshProUGUI sliderUI;
    public TextMeshProUGUI dialUI;
    public TextMeshProUGUI selectedUI;

    public GameObject highlightSource;
    public GameObject leverSource;
    public GameObject dialSource;
    public GameObject sliderSource;

    highlight highlight;
    lever lever;
    dial dial;
    followByZ slide;

    // Start is called before the first frame update
    void Start()
    {
        highlight = highlightSource.GetComponent<highlight>();
        lever = leverSource.GetComponent<lever>();
        dial = dialSource.GetComponent<dial>();
        slide = sliderSource.GetComponent<followByZ>();
    }

    // Update is called once per frame
    void Update()
    {
        //lever UI
        if (lever.state == 0) leverUI.text = "Lever (z) : middle";
        else if (lever.state == 1) leverUI.text = "Lever (z) : Up";
        else leverUI.text = "Lever (z) : Down";

        //slider UI
        sliderUI.text = "Slider speed (x) : " + slide.horizontalSpeed * 100;

        //dial UI
        dialUI.text = "Dial scale : " + dial.initAngle * 0.03f;


        //selected UI
        if (highlight.previousGameObject != null) selectedUI.text = "Selected : " + highlight.previousGameObject.gameObject.name;
        else selectedUI.text = "Selected : none";
    }
}
