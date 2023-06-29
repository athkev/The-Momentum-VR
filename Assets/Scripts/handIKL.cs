using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handIKL : MonoBehaviour
{
    [SerializeField] public GameObject followObject;
    //[SerializeField] private float rotateSpeed = 100f;
    [SerializeField] public Vector3 positionOffset;
    [SerializeField] public Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        //_body.position = _followTarget.position;
        //_body.rotation = _followTarget.rotation;
    }

    private void PhysicsMove()
    {
        /*
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        //var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        //var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        //q.ToAngleAxis(out float angle, out Vector3 axis);
        //_body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        _body.transform.eulerAngles = _followTarget.eulerAngles;
        _body.transform.RotateAround(transform.position, _followTarget.transform.up, rotationOffset.x);
        _body.transform.RotateAround(transform.position, _followTarget.transform.forward, rotationOffset.y);
        */
        transform.position = _followTarget.TransformPoint(positionOffset);
        transform.rotation = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsMove();
    }

}
