using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dial : MonoBehaviour
{
    public GameObject highlightSource;
    highlight highlight;
    GameObject selected;

    bool init = false;
    Vector3 initScale;
    public float initAngle;
    public float angle;
    public float lastAngle;



    // Start is called before the first frame update
    void Start()
    {
        highlight = highlightSource.GetComponent<highlight>();
        angle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (highlight.previousGameObject != null)
        {
            if (highlight.previousGameObject.gameObject != selected)
            {
                init = true;
                selected = highlight.previousGameObject.gameObject;
            }
            if (init)
            {
                init = false;
                initScale = selected.transform.localScale;
                initAngle = transform.eulerAngles.y;
                lastAngle = initAngle;
            }
            angle = transform.eulerAngles.y - lastAngle;
            if (angle > 180) angle -= 360;
            if (angle < -180) angle += 360;
            lastAngle = transform.eulerAngles.y;

            initAngle += angle;

            //control object
            if (angle != 0) selected.transform.localScale = Vector3.Lerp(selected.transform.localScale, initScale * initAngle * 0.03f, Time.deltaTime);
        }

        if (highlight.previousGameObject == null)
        {
            init = true;
            selected = null;
        }



    }
}
