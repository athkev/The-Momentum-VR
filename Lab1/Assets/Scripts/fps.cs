using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    public float sens = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public float speed = 0;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    void Start()
    {
        rb = playerBody.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        movementX = Input.GetAxis("Horizontal") * Time.deltaTime;
        movementY = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 movement = transform.right * movementX + transform.forward * movementY;

        rb.AddForce(movement * speed);
    }

        

    private void FixedUpdate()
    {
    }
}
