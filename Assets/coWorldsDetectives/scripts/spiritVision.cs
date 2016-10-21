using UnityEngine;
using System.Collections;

public class spiritVision : MonoBehaviour {

    public Camera cam;
    public bool start = false;
    public bool update = false;

    private NewtonVR.NVRHand hand;

    // Use this for initialization
    void Start() {
        start = true;
        hand = GetComponent<NewtonVR.NVRHand>();
	}
	
	// Update is called once per frame
	void Update() {
        update = true;
        if (hand.UseButtonPressed)
        {
            Debug.Log("button pressed");
        }
	}
}
