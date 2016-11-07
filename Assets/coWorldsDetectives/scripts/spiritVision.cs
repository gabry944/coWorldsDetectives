using UnityEngine;
using System.Collections;

public class spiritVision : MonoBehaviour {

    public Camera cam;

    //private NewtonVR.NVRHand hand;

    // Use this for initialization
    void Start() {
        //hand = GetComponent<NewtonVR.NVRHand>();
        Show();
	}
	
	// Update is called once per frame
	void Update() {
        /*if (hand.UseButtonPressed)
        {
            Show();
        }
        else
            Hide();*/
	}

    // Turn on the culling bit using an OR operation:
    private void Show()
    {
        cam.cullingMask |= 1 << 8;
    }

    // Turn off the culling bit using an AND operation with the complement of the shifted int:
    private void Hide()
    {
        cam.cullingMask &= ~(1 << 8);
    }
}
