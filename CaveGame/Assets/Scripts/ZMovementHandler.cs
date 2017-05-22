using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMovementHandler : MonoBehaviour {

    public float layerDistance = 1;

    GameObject childCollider;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetButtonUp("Forward"))
        {
            player.GetComponent<PlayerPlatformerController>().canMoveBackward = true;
            if (player.transform.position.z == transform.position.z)
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(true);
            }
            else
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
            }
        }
        if (Input.GetButtonUp("Backward"))
        {
            player.GetComponent<PlayerPlatformerController>().headClearForward = true;
            player.GetComponent<PlayerPlatformerController>().playerClearForward = true;

            if (player.transform.position.z == transform.position.z)
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(true);
            }
            else
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
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
