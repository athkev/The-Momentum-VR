using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public bool isLeftHand = true;
    GameObject hand;

    public bool attached;
    public float timer = 0f;
    public float timeToDetach = 2f;
    float lerp = 0f;

    public float currentScale = 1;
    public float newScale = 4;
    public float force = 3f;

    public float distanceBetweenHand;
    public float distanceBetweenHandX;

    // Start is called before the first frame update
    void Start()
    {
        if (isLeftHand) hand = GameObject.FindWithTag("LeftHand");
        else hand = GameObject.FindWithTag("RightHand");
        attached = true;
        currentScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < timeToDetach)
        {
            lerp += Time.deltaTime / timeToDetach;
            /*
            transform.position = hand.transform.position + new Vector3(0, 0, distanceBetweenHand);
            if (isLeftHand) transform.position = hand.transform.position + new Vector3(distanceBetweenHandX, 0, distanceBetweenHand);
            else transform.position = hand.transform.position + new Vector3(-distanceBetweenHandX, 0, distanceBetweenHand);
            */
            transform.parent = hand.transform;

            if (isLeftHand) transform.localPosition = new Vector3(distanceBetweenHandX, 0, distanceBetweenHand);
            else transform.localPosition = new Vector3(-distanceBetweenHandX, 0, distanceBetweenHand);

            float scale = Mathf.Lerp(currentScale, newScale, lerp);
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else //if balloon is fully grown
        {
            if (gameObject.GetComponent<Rigidbody>().isKinematic)
            {
                transform.parent = null;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1 * force, 0));
            }
        }


        //if objects height is higher than 20, remove
        if (transform.position.y > 40)
        {
            Destroy(gameObject);
        }


        timer += Time.deltaTime;
    }
}
