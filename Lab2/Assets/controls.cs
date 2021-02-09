using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class controls : MonoBehaviour
{
    public bool isLeftHand = true;

    public SteamVR_ActionSet actions;
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean spawnAction;
    public SteamVR_Action_Boolean shootAction;
    public Rigidbody balloon;
    public GameObject hand; //hand location for balloon
    public Transform point;

    public LineRenderer guideline;
    public LineRenderer line;

    public ParticleSystem gripParticle;

    public float force = 50.0f;

    private float timer = 0.0f;
    private float shootCD = 0.3f;

    private float timerLine = 0.0f;
    private float lineCD = 0.3f;

    private bool shot = false;
    private bool gripheld = false;
    private bool lineActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            spawnBalloons();
        }

        if (shootAction.GetStateDown(SteamVR_Input_Sources.Any) && !shot)
        {
            shoot();
            shot = true;
            lineActive = true;
        }

        if (gripAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            gripheld = true;
            guideline.gameObject.SetActive(true);
            gripParticle.gameObject.SetActive(true);
        }
        if (gripAction.GetStateUp(SteamVR_Input_Sources.Any))
        {
            gripheld = false;
            guideline.gameObject.SetActive(false);
            gripParticle.gameObject.SetActive(false);
        }


        if (gripheld) guide();
        if (shot)
        {
            gripParticle.Emit(1);

            if (timer > shootCD) { shot = false; timer = 0;}
            else timer += Time.deltaTime;

            if (timerLine > lineCD) { lineActive = false; timerLine = 0; line.gameObject.SetActive(false); }
            else if (lineActive) timerLine += Time.deltaTime;
        }

    }

    void spawnBalloons()
    {
        Rigidbody balloonInst;
        balloonInst = Instantiate(balloon, hand.transform.position, hand.transform.rotation) as Rigidbody;
        
    }

    void shoot()
    {
        line.gameObject.SetActive(true);
        RaycastHit hit;
        var pos = point.transform.position;
        var dir = point.transform.forward;

        line.SetPosition(0, pos);
        if (Physics.Raycast(pos, dir, out hit, 100))
        {
            if (hit.collider) line.SetPosition(1, hit.point);

            if (hit.transform.tag == "balloon")
            {
                if (isLeftHand) Destroy(hit.transform.gameObject); //destroy balloon with left hand trigger

                //bonus: apply force when hit
                //hit balloon with right hand trigger
                else hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * force);
            }
        }
        
        else
        {
            pos = pos + (dir.normalized * 100);
            line.SetPosition(1, pos);
        }

    }

    void guide()
    {
        RaycastHit hit;
        var pos = point.transform.position;
        var dir = point.transform.forward;

        guideline.SetPosition(0, pos);
        if (Physics.Raycast(pos, dir, out hit, 100))
        {
            if (hit.collider) guideline.SetPosition(1, hit.point);
        }

        else
        {
            pos = pos + (dir.normalized * 100);
            guideline.SetPosition(1, pos);
        }
    }
}
