using UnityEngine;
using System.Collections;

public class spiritVision : MonoBehaviour {

    public Camera cam;

    private NewtonVR.NVRHand hand;

    // Use this for initialization
    void Start() {
        hand = GetComponent<NewtonVR.NVRHand>();
        if (Display.displays.Length == 1)
        {
            Display.displays[1].Activate();
            Debug.Log("activated camera");
        }
	}
	
	// Update is called once per frame
	void Update() {
        if (hand.UseButtonPressed)
        {
            Debug.Log("button pressed");
            Show();
        }
        else
            Hide();
	}

    // Turn on the culling bit using an OR operation:
    private void Show()
    {
        cam.cullingMask |= 1 << 8;
    }

    // Turn off the culling bit using an AND operation with the complement of the shifted int:
    private void Hide()
    {
        Debug.Log("hide");
        cam.cullingMask &= ~(1 <<8);
    }
}
