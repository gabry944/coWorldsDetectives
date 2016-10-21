using UnityEngine;
using System.Collections;

public class spiritVision : MonoBehaviour {

    public Camera cam;
    public Camera notCam;

    private NewtonVR.NVRHand hand;

	// Use this for initialization
	void Start () {
        hand = GetComponent<NewtonVR.NVRHand>();
        cam.enabled = true;
        notCam.enabled = false;
        Debug.Log("camera switched");
	}
	
	// Update is called once per frame
	void Update () {
        if (hand.UseButtonPressed)
        {
            Debug.Log("button pressed");
        }
	}
}
