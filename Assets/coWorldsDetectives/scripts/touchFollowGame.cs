using UnityEngine;
using System.Collections;

public class touchFollowGame : MonoBehaviour {

    public Light[] lights;
    public Light[] SpiritLights;
    public GameObject[] buttons;
    public GameObject keystone;
    public float lightTime = 0.5f;
    private float originalLightTime = 0;

    private int[] order;
    private int number = 0;
    private int current = 0;
    private float currentTime = 0;
    private bool beforeStart = false;
    private bool lightOn = true;
    private bool start = false;
    private bool wait = true;
    private int lookingFor = 0;
    public bool solved = false;

    public AudioClip A;
    public AudioClip B;
    public AudioClip C;
    public AudioClip D;
    public AudioClip bad;
    private AudioSource sound;

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

        sound = GetComponent<AudioSource>();
        sound.clip = B;

        originalLightTime = lightTime;

        //if room already solved
        int id = GameState.Instance.roomId;
        if (GameState.Instance.solved[id])
        {
            start = false;
            solved = true;
            //get keystone to fall down
            keystone.GetComponent<Rigidbody>().useGravity = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(beforeStart)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<touchListener>().activated)
                {
                    StartGame();
                    break;
                }
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

                        allPlayerLightOn();
                    }
                    else
                    {
                        SpiritLights[order[current]].intensity = 0;
                        allPlayerLightOff();
                    }
                }
                else
                {
                    lightTime = originalLightTime;
                    SpiritLights[order[current]].intensity = 0;
                    allPlayerLightOff();
                    currentTime = 0;
                    if(lightOn)
                        current++;
                    lookingFor = 0;
                    lightOn = !lightOn;

                    if (lightOn && current < number)
                    {
                        playSound(current);
                    }
                }
            }
            else
            {
                start = false;
                wait = true;

                //deactivate all buttons that were activeted during the showing of the loop
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].GetComponent<touchListener>().activated = false;
                }
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
                            //raise the time the light should be off between error and loop
                            lightTime *= 3;

                            number++;
                            wait = false;
                            start = true;
                            current = 0;
                            lightOn = false;

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
                        //raise the time the light should be off between error and loop
                        lightTime *= 3;
                        sound.clip = bad;
                        sound.Play();
                        Debug.Log("incorrect");
                        buttons[i].GetComponent<touchListener>().activated = false;
                        wait = false;
                        start = true;
                        current = 0;
                        number = 1;
                        lightOn = false;
                        break;
                    }
                }
            }
        }
        
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<touchListener>().enter)
            {
                playSound(lookingFor);

                //light up the stone for the player
                lights[i].intensity = 2;

                //light up the stone for the spirit
                SpiritLights[i].intensity = 2;
                if (i == 1)
                    SpiritLights[i].intensity = 3;

                buttons[i].GetComponent<touchListener>().enter = false;
            }
            if (buttons[i].GetComponent<touchListener>().leave)
            {
                //make the stones normal again 
                lights[i].intensity = 0;
                SpiritLights[i].intensity = 0;
                buttons[i].GetComponent<touchListener>().leave = false;
            }
        }
    }

    void CreateGameOrder()
    {
        order = new int[1];
        order[0] = 1;
        /*order[1] = 0;
        order[2] = 3;
        order[3] = 1;
        order[4] = 2;*/
    }

    public void StartGame()
    {
        start = true;
        wait = false;
        beforeStart = false;
        number = 1;
        lightOn = false;
    }

    void playSound(int pos)
    {
        if (order[pos] == 0)
            sound.clip = A;
        else if (order[pos] == 1)
            sound.clip = B;
        else if (order[pos] == 2)
            sound.clip = C;
        else
            sound.clip = D;

        sound.Play();
    }

    void allPlayerLightOn()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            lights[i].intensity = 2;
        }
    }

    void allPlayerLightOff()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            lights[i].intensity = 0;
        }
    }
}
