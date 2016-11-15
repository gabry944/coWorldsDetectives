using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class fallingSound : MonoBehaviour {

    private AudioSource sound;
    private Rigidbody rb;
    private bool landed = false;

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //if the keystone hasn't landed but uses gravity and has low speed
        if (!landed && rb.useGravity && rb.velocity.magnitude < 0.1)
        {
            Debug.Log("falling");
            landed = true;
            sound.Play();
        }
	}
}
