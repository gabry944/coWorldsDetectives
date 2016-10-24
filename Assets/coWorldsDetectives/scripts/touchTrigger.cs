using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class touchTrigger : MonoBehaviour {

    public GameObject activate;
    public GameObject keystone;

    private pillarHiddenRoom script;
    private Collider box;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        box = GetComponent<Collider>();
        box.isTrigger = true;
        script = activate.GetComponent<pillarHiddenRoom>();
        rb = keystone.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("froze all");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            script.Unlock();
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
