using UnityEngine;
using System.Collections;

public class pillarHiddenRoom : MonoBehaviour {
    public GameObject hiddenDoor1;
    public GameObject hiddenDoor2;
    public GameObject keystone;

    //change position sligtly so that we get ride of z-fighting
    public Vector3 movePos1;
    public Vector3 movePos2;

    public bool move;
    public float speed;
    public Vector3 direction;
    public float distance;

    float lenght;
    bool open;
    private Rigidbody rb;

    private AudioSource sound;
    
    // Use this for initialization
    void Start () {
        lenght = 0;
        move = false;
        open = false;

        sound = GetComponent<AudioSource>();

        //in order to kepp player from taking keystone from pillar 
        rb = keystone.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //trust that gameState knows what room we are in.
        int id = GameState.Instance.roomId;
        if (GameState.Instance.solved[id])
        {
            hiddenDoor1.GetComponent<Transform>().localPosition = hiddenDoor1.GetComponent<Transform>().localPosition + (direction * distance);
            hiddenDoor2.GetComponent<Transform>().localPosition = hiddenDoor2.GetComponent<Transform>().localPosition + (direction * distance);
            open = true;
            rb.constraints = RigidbodyConstraints.None;
        }
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

    public void Unlock()
    {
        if (!open)
        {
            hiddenDoor1.GetComponent<Transform>().localPosition = movePos1;
            hiddenDoor2.GetComponent<Transform>().localPosition = movePos2;
            move = true;
            lenght = 0;
            open = true;
            sound.Play();
            Debug.Log("Playing");
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
