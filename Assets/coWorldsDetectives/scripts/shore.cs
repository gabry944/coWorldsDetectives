using UnityEngine;
using System.Collections;

public class shore : MonoBehaviour {

    private floatingStoneBehavior fsb = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        fsb = other.gameObject.GetComponent<floatingStoneBehavior>();
        if (fsb)
        {
            Debug.Log("speed 0");
            fsb.Stop();
        }
    }
}
