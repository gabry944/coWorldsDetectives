using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class floatingStoneBehavior : MonoBehaviour {

    private AudioSource sound;
    private Rigidbody rb;
    public bool landed;
    private NewtonVR.NVRInteractableItem nvrItem;
    public bool held;

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        nvrItem = GetComponent<NewtonVR.NVRInteractableItem>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!landed && rb.useGravity && rb.velocity.magnitude < 0.1)
        {
            landed = true;
            sound.Play();
        }

        if (nvrItem != null && nvrItem.AttachedHand != null)
        {
            held = true;
        }

        if (landed && held && nvrItem.AttachedHand == null)
        {
            landed = false;
            held = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!landed)
        {
            sound.Play();
        }
    }
}


