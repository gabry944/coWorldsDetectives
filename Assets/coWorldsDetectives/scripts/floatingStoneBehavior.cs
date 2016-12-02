using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class floatingStoneBehavior : MonoBehaviour {

    private AudioSource sound;
    private Rigidbody rb;
    public bool landed;
    private NewtonVR.NVRInteractableItem nvrItem;
    public bool held;
    public float speed = 0.002f;
    public bool floating = false;
    float height;
    private GameObject theWater;

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
        if (floating)
        {
            //Debug.Log("speed " + speed);
            transform.position = new Vector3(transform.position.x + -speed, theWater.transform.position.y, transform.position.z);

            if (nvrItem != null && nvrItem.AttachedHand != null)
            {
                held = true;
            }
        }
        else
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
        if(held)
        {
            if (floating)
            {
                floating = false;
                rb.freezeRotation = false;
                rb.isKinematic = false;
            }
        }
    }

    //when hitting something but not landing
    void OnCollisionEnter(Collision collision)
    {
        //landed is needed or else the stones land and sound the first thing they do
        if (!landed)
        {
            sound.Play();
        }

        ////if stone is floting and touch shore
        /*if (floating)
        {
            floating = false;
            rb.freezeRotation = false;
            rb.isKinematic = false;
        }*/
    }

    public void returnToShore(GameObject water)
    {
        theWater = water;
        if (!floating)
        {
            floating = true;
            rb.freezeRotation = true;
            rb.isKinematic = true;
            speed = 0.002f;
        }
    }

    public void Stop()
    {
        speed = 0;
        rb.isKinematic = false;
        floating = false;
    }
}


