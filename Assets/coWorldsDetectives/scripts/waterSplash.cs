using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class waterSplash : MonoBehaviour {

    private AudioSource audio;
    public float timeBeforeStart = 1;
    public float currTime = 0;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currTime += Time.deltaTime;

    }

    void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if(rb != null && currTime > timeBeforeStart && rb.velocity.y < 0)
            audio.Play();
    }
}
