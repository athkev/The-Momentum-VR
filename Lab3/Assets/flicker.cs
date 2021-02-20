using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    public float time;
    public float minimumOnTime = 5f;
    public float flickingTime = 0.5f;
    bool flick = false;
    Light lightComp;
    void Start()
    {
        lightComp = GetComponent<Light>();
        StartCoroutine(flickering());
    }

    IEnumerator flickering()
    {
        while (true)
        {
            if (!flick)
            {
                yield return new WaitForSeconds(Random.Range(minimumOnTime, minimumOnTime+2));
                lightComp.enabled = true;

                time = 0;
                flick = true;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.05f, 0.12f));
                lightComp.enabled = !lightComp.enabled;
                time += Time.deltaTime;
                if (time > flickingTime)
                {
                    lightComp.enabled = true;
                    flick = false;
                }
                
            }
        }
    }
}
