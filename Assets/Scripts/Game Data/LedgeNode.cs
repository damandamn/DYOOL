using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeNode : MonoBehaviour {

    public bool isGrabbed;
    public Vector3 ledgePosition;
    public Vector3 ledgeGrabPosition;
    public bool ledgeGrabLeft;

    PlayerController playerHolding;
    public int ledgeDuration = 0;

    private void Start()
    {
        transform.position = ledgePosition;
    }

    private void Update()
    {
        if (playerHolding != null)
        {
            if (playerHolding.currLedge == this)
            {
                ledgeDuration++;
            } else
            {
                ledgeDuration = 0;
                playerHolding = null;
            }
            
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            //Checks players ledgegrab conditions
            if (player.currLedge != null)
            {
                return;
            }
            else
            if (player.vert <= -player.vertThreshold)
            {
               // Debug.Log("Holding down");
                return;
            } else
            if (ledgeGrabLeft && player.hori > player.horiThreshold)
            {
                //Debug.Log("Holding away right");
                return;
            } else
            if (!ledgeGrabLeft && player.hori < -player.horiThreshold)
            {
                //Debug.Log("Holding away left");
                return;
            }else
            if (!player.canGrabLedge)
            {
                //Debug.Log("Cant grab ledge");
                return;
            }else
            if (player.framesSinceLedgeGrab < 40)
            {
                Debug.Log(player.framesSinceLedgeGrab);
                return;
            }

            if (playerHolding != null)
            {
                playerHolding.moveState = PlayerController.MoveStates.AIRBORNE;
            }

            playerHolding = player;
            player.GrabLedge(this);

            other.transform.position = ledgeGrabPosition;

        }
    }
}
