using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using Valve.VR;
using UnityEngine;

public class DebugMouseInput : MonoBehaviour
{
    public Hand hand;
    private bool isGripping = false;
    void Start()
    {
        hand = GetComponent<Hand>();
    }
    
    void Update()
    {
        if (!isGripping && Input.GetMouseButton(0))
        {
            isGripping = true;
            hand.OnGrabGripDown();
        }

        if (isGripping && !Input.GetMouseButton(0))
        {
            isGripping = false;
            hand.OnGrabGripUp();
        }
    }
}
