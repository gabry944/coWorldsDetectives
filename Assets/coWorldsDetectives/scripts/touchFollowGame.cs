using UnityEngine;
using System.Collections;

public class touchFollowGame : MonoBehaviour {

    public Light[] lights;
    public Light[] SpiritLights;
    public GameObject[] buttons;
    public GameObject keystone;
    public float lightTime = 1;

    private int[] order;
    private int number = 0;
    private int current = 0;
    private float currentTime = 0;
    private bool beforeStart = true;
    private bool lightOn = true;
    private bool start = false;
    private bool wait = false;
    private int lookingFor = 0;
    public bool solved = false;

	// Use this for initialization
	void Start () {
        CreateGameOrder();

        for(int i = 0; i < SpiritLights.Length; i++)
        {
            SpiritLights[i].intensity = 0;
        }

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(beforeStart)
        {
            //plus time
            currentTime += Time.deltaTime;

            //if current cube should still be lit
            if (currentTime < lightTime)
            {
                if(lightOn)
                    SpiritLights[order[0]].intensity = 2;
                else
                    SpiritLights[order[0]].intensity = 0;
            }
            else
            {
                currentTime = 0;
                lightOn = !lightOn;
            }

            if (buttons[order[0]].GetComponent<touchListener>().activated)
            {
                StartGame();
            }
        }
        //show order to touch
        else if (start)
        {
            //if not all cubes have been lit
            if (current < number)
            {
                //plus time
                currentTime += Time.deltaTime;

                //if current cube should still be lit
                if (currentTime < lightTime)
                {
                    if (lightOn)
                    {
                        SpiritLights[order[current]].intensity = 1;
                        if (order[current] == 1)
                            SpiritLights[order[current]].intensity = 2;
                    }
                    else
                        SpiritLights[order[current]].intensity = 0;
                }
                else
                {
                    SpiritLights[order[current]].intensity = 0;
                    currentTime = 0;
                    if(lightOn)
                        current++;
                    lookingFor = 0;
                    lightOn = !lightOn;
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
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<touchListener>().activated)
                {
                    if (i == order[lookingFor])
                    {
                        //go to the next step
                        Debug.Log("Activated " + i);
                        buttons[i].GetComponent<touchListener>().activated = false;
                        lookingFor++;
                        if (lookingFor == number)
                        {
                            number++;
                            wait = false;
                            start = true;
                            current = 0;

                            if(number > order.Length)
                            {
                                start = false;
                                solved = true;
                                //get keystone to fall down
                                keystone.GetComponent<Rigidbody>().useGravity = true;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("incorrect");
                        buttons[i].GetComponent<touchListener>().activated = false;
                        wait = false;
                        start = true;
                        current = 0;
                        number = 1;
                        break;
                    }
                }
            }
        }
        
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<touchListener>().enter)
            {
                //light up the stone for the player
                lights[i].intensity = 2;
                if (i == 1)
                    lights[i].intensity = 3;

                buttons[i].GetComponent<touchListener>().enter = false;
            }
            if (buttons[i].GetComponent<touchListener>().leave)
            {
                //make the stone normal again 
                lights[i].intensity = 0;
                buttons[i].GetComponent<touchListener>().leave = false;
            }
        }
    }

    void CreateGameOrder()
    {
        order = new int[5];
        order[0] = 1;
        order[1] = 0;
        order[2] = 3;
        order[3] = 1;
        order[4] = 2;
    }

    public void StartGame()
    {
        start = true;
        wait = false;
        beforeStart = false;
        number = 1;
        lightOn = false;
    }
}
