using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class stepsTrigger : MonoBehaviour {

    public GameObject activate;
    private footstepBehavior script;
    private Collider col;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
        col.isTrigger = true;
        script = activate.GetComponent<footstepBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered: " + other.gameObject.name);
        if(other.gameObject.layer == 9)
        {
            Debug.Log("Spirit hand entered");
            script.PlayWalk();
        }
    }
}
