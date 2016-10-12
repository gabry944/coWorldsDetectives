using UnityEngine;
using System.Collections;

public class pillarHiddenRoom : MonoBehaviour {
    public GameObject hiddenDoor1;
    public GameObject hiddenDoor2;
    public bool move;

    Vector3 direction;
    float distance;
    // Use this for initialization
    void Start () {
        move = false;
        distance = 0.1f;
        direction = new Vector3(0.0f, 1.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            transform.localPosition = transform.localPosition + (direction * distance);
        }
    }

    // Update is called once per frame
    public void Unlock()
    {
        move = true;
    }
}
