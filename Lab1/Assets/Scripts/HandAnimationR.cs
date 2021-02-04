using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malimbe.PropertySerializationAttribute;
using Malimbe.XmlDocumentationAttribute;
using Zinnia.Action;

public class HandAnimationR : MonoBehaviour
{



    public Animator _anim;

    public bool val;
    void Start()
    {

        _anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        val = Input.GetKey("joystick button 5");

        if (val)
        {
            if (!_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", true);
            }
        }
        else
        {
            if (_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", false);
            }
        }

    }
}
