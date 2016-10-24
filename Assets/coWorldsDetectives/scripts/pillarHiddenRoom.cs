using UnityEngine;
using System.Collections;

public class pillarHiddenRoom : MonoBehaviour {
    public GameObject hiddenDoor1;
    public GameObject hiddenDoor2;

    //change position sligtly so that we get ride of z-fighting
    public Vector3 movePos1;
    public Vector3 movePos2;

    public bool move;
    public float speed;
    public Vector3 direction;
    public float distance;

    Vector3 transform1;
    Vector3 transform2;
    float lenght;
    bool open;

    // Use this for initialization
    void Start () {
       // Unlock();
        lenght = 0;
        move = false;
        open = false;

        transform1 = hiddenDoor1.GetComponent<Transform>().localPosition;
        transform2 = hiddenDoor2.GetComponent<Transform>().localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            if (lenght > distance)
            {
                move = false;
            }
            else
            {
			    hiddenDoor1.GetComponent<Transform>().localPosition = hiddenDoor1.GetComponent<Transform>().localPosition + (direction * speed);
			    hiddenDoor2.GetComponent<Transform>().localPosition = hiddenDoor2.GetComponent<Transform>().localPosition + (direction * speed);

                lenght += speed;
            }
        }
    }

    // Update is called once per frame
    public void Unlock()
    {
        if (!open)
        {
            hiddenDoor1.GetComponent<Transform>().localPosition = movePos1;
            hiddenDoor2.GetComponent<Transform>().localPosition = movePos2;
            move = true;
            lenght = 0;
            open = true;
        }
    }
}
