using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followByZ : MonoBehaviour
{
    public GameObject highlightSource;
    highlight highlight;
    GameObject selected;

    public Transform target;
    Rigidbody rigidBody;
    public float horizontalSpeed;

    void Start()
    {
        highlight = highlightSource.GetComponent<highlight>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = transform.localPosition.x / 0.17f * 0.01f;
        if (highlight.previousGameObject != null) selected = highlight.previousGameObject.gameObject;

        if (target.localPosition.x < 0.17 && target.localPosition.x > -0.17) transform.localPosition = new Vector3(target.localPosition.x, 0, 0);

        //transform.localposition.x will be in range -0.17 to 0.17,
        //make it into horizontal speed
        if (highlight.previousGameObject != null)
        {
            if (highlight.previousGameObject.gameObject == selected)
            {
                selected.gameObject.transform.parent.position = new Vector3(selected.gameObject.transform.parent.position.x + transform.localPosition.x / 0.17f * 0.01f,
                    selected.gameObject.transform.parent.position.y, selected.gameObject.transform.parent.position.z);
            }
        }
    }
}
