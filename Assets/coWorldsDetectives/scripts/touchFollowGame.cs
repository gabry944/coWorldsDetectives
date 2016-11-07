using UnityEngine;
using System.Collections;

public class touchFollowGame : MonoBehaviour {

    public Light[] lights;
    public GameObject[] buttons;
    public GameObject keystone;
    public float lightTime = 1;

    private int[] order;
    private int number = 0;
    private int current = 0;
    private float currentTime = 0;
    private bool start = false;
    private bool wait = false;

	// Use this for initialization
	void Start () {
        CreateGameOrder();
	}
	
	// Update is called once per frame
	void Update () {
        //show order to touch
        if (start)
        {
            //if not all cubes have been lit
            if (current < number)
            {
                //plus time
                currentTime += Time.deltaTime;

                //if current cube should still be lit
                if (currentTime < lightTime)
                    lights[current].intensity = 1;
                else
                {
                    lights[current].intensity = 0;
                    currentTime = 0;
                    current++;
                }
            }
            else
            {
                start = false;
                wait = true;
            }
        }
        //wait for touch
        else if (wait)
        {
            if(buttons[order[current]].GetComponent<touchListener>().activated)
            {

            }
        }
	}

    void CreateGameOrder()
    {
        order = new int[5];
        order[0] = 2;
        order[1] = 1;
        order[2] = 4;
        order[3] = 2;
        order[4] = 3;
    }

    void StartGame()
    {
        start = true;
        number = 1;
    }
}
