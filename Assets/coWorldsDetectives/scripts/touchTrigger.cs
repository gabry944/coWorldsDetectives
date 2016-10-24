using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class touchTrigger : MonoBehaviour {

    public GameObject activate;
    private pillarHiddenRoom script;
    private Collider box;

	// Use this for initialization
	void Start () {
        box = GetComponent<Collider>();
        box.isTrigger = true;
        script = activate.GetComponent<pillarHiddenRoom>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            Debug.Log("Player touch");
            script.Unlock();
        }
        Debug.Log("Not player touch");
    }
}
