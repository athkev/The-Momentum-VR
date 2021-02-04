using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI ballCountT;
    public TextMeshProUGUI collectableCountT;
    public TextMeshProUGUI timer;
    
    public GameObject endTextvr;
    public GameObject endTextsim;

    private int ballCount;
    private int collectableCount;
    private float time = 0;
    private bool finished;

    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;

    // Start is called before the first frame update
    void Start()
    {
        ballCount = 0;
        collectableCount = 0;
        finished = false;

        SetCountText();
        SetCountText_ball();

        endTextvr.SetActive(false);
        endTextsim.SetActive(false);
    }

    void SetCountText()
    {
        collectableCountT.text = "Collectable: " + collectableCount.ToString();
        if (ballCount >= 3 && collectableCount >= 4)
        {
            endTextvr.SetActive(true);
            endTextsim.SetActive(true);
            finished = true;
        }
    }
    void SetCountText_ball()
    {
        ballCountT.text = "Balls: " + ballCount.ToString();

        if (ballCount >= 3 && collectableCount >= 4 )
        {
            endTextvr.SetActive(true);
            endTextsim.SetActive(true);
            finished = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            timer.text = "Time: " + (int)time + "s";
            time += Time.deltaTime;
        }

        //maybe add a trigger hitbox plane below for efficiency instead of update()
        if (ball1.activeSelf) checkScore(ball1);
        if (ball2.activeSelf) checkScore(ball2);
        if (ball3.activeSelf) checkScore(ball3);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            collectableCount++;

            SetCountText();
        }
    }

    void checkScore(GameObject o)
    {
        if (o.transform.position.y < -4)
        {
            o.SetActive(false);
            ballCount++;

            SetCountText_ball();
        }
    }
}
