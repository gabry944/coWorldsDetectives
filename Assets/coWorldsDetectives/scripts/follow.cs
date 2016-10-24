using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

    public GameObject followThis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = followThis.transform.position;
	}
}
