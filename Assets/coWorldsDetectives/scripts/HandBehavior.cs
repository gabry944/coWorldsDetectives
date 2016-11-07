using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandBehavior : MonoBehaviour
{
    public GameObject hand;
    public GameObject foot;

    private bool visible;
    private Vector3 position;

    void Start()
    {
        position = hand.transform.position;
        visible = false;
        //hide the object under ground
        hand.transform.position = new Vector3(0, -10, 0);
    }

    void Update()
    {
        if (!visible)
        {
            if (foot.GetComponent<footstepBehavior>().done)
            {
                //show object
                hand.transform.position = position;
                visible = true;
            }
        }
    }

}
