using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public GameObject highlightSource;
    highlight highlight;
    GameObject selected;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        highlight = highlightSource.GetComponent<highlight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (highlight.previousGameObject != null && highlight.previousGameObject.gameObject != selected) selected = highlight.previousGameObject.gameObject;

        if (transform.rotation.x > 0.45)
        {
            //lever up
            state = 1;

            if (selected != null)
            {
                selected.gameObject.transform.parent.transform.position = new Vector3(
                    selected.gameObject.transform.parent.transform.position.x, selected.gameObject.transform.parent.transform.position.y,
                    selected.gameObject.transform.parent.transform.position.z + 0.01f);
            }
        }
        else if (transform.rotation.x < -0.45)
        {
            //lever down
            state = -1;

            if (selected != null)
            {
                selected.gameObject.transform.parent.transform.position = new Vector3(
                    selected.gameObject.transform.parent.transform.position.x, selected.gameObject.transform.parent.transform.position.y,
                    selected.gameObject.transform.parent.transform.position.z - 0.01f);
            }
        }
        else state = 0;
    }
}
