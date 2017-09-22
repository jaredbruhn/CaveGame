using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMovementHandler : MonoBehaviour {

    public float layerDistance = 1;

    GameObject childCollider;
    GameObject player;
    Collider2D coll;
    void Start()
    {
        player = GameObject.Find("Player");
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Forward"))
        {
            player.GetComponent<PlayerPlatformerController>().canMoveBackward = true;
            if (player.transform.position.z == transform.position.z)
            {
                coll.isTrigger = false;
            }
            else
            {
                coll.isTrigger = true;        
            }
        }
        if (Input.GetButtonUp("Backward"))
        {
            player.GetComponent<PlayerPlatformerController>().headClearForward = true;
            player.GetComponent<PlayerPlatformerController>().playerClearForward = true;

            if (player.transform.position.z == transform.position.z)
            {
                coll.isTrigger = false;
            }
            else
            {
                coll.isTrigger = true;
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D playerCol)
    //{
    //    if (playerCol.tag == "ZCheckHead" && playerCol.transform.position.z == transform.position.z - layerDistance) {
    //        playerCol.GetComponentInParent<PlayerPlatformerController>().canMoveForward = false;
    //        //print("CanNotMoveForward");
    //    }
    //}

    void OnTriggerStay2D(Collider2D playerCol)
    {
        if (playerCol.tag == "ZCheckHead" && playerCol.transform.position.z == transform.position.z - layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().headClearForward = false;
        }
        if (playerCol.tag == "Player" && playerCol.transform.position.z == transform.position.z - layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().playerClearForward = false;
        }
        if (playerCol.tag == "Player" && playerCol.transform.position.z == transform.position.z + layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().canMoveBackward = false;
        }
    }

    void OnTriggerExit2D(Collider2D playerCol )
    {
        if (playerCol.tag == "ZCheckHead" && playerCol.transform.position.z == transform.position.z - layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().headClearForward = true;
        }
        if (playerCol.tag == "Player" && playerCol.transform.position.z == transform.position.z - layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().playerClearForward = true;
        }
        if (playerCol.tag == "Player" && playerCol.transform.position.z == transform.position.z + layerDistance)
        {
            playerCol.GetComponentInParent<PlayerPlatformerController>().canMoveBackward = true;
        }
    }
}
