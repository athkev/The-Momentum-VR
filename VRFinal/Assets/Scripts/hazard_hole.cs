using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard_hole : MonoBehaviour
{
    public GameObject player;
    private BoxCollider col;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //respawn
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }
    }
}
