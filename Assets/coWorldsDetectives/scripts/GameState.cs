using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public bool start = true;
    public bool arrived_with_teleporter = false;
    public static GameState Instance;
    public int roomId = 0;
    public bool startedMusicInRoom = true;
    public bool end = false;
    public bool[] solved;

    public AudioSource sound;
    public AudioClip outside;
    public AudioClip insideDay;
    public AudioClip insideNight;
    public AudioClip done;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();

        roomId = 0;
        // at begining of game, all rooms are unsolved
        solved = new bool[3];
        for (int i = 0; i < solved.Length; i++)
        {
            solved[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(!startedMusicInRoom && roomId == 0)
        {
            sound.clip = outside;
            if (!solved[roomId])
            {
                Debug.Log("startRoom");
            }
            else
            {
                Debug.Log("startRoom solved");

                //also activate kid
                if (solved[2])
                {
                    sound.clip = done;
                    end = true;
                }
            }
                startedMusicInRoom = true;
                sound.volume = 0.4f;
                sound.Play();
        }
        if (!startedMusicInRoom && roomId == 1)
        {
            if (!solved[roomId])
            {
                Debug.Log("puzzle1");
            }
            else
            {
                Debug.Log("puzzle1 solved");
            }
            sound.clip = insideDay;
            sound.volume = 0.4f;
            sound.Play();
            startedMusicInRoom = true;

            ////also activate kid
            //end = true;
        }
        if (!startedMusicInRoom && roomId == 2)
        {
            if (!solved[roomId])
            {
                Debug.Log("endRoom");
            }
            else
            { 
                Debug.Log("endRoom solved");
            }
            start = false;
            sound.clip = insideNight;
            sound.volume = 0.5f;
            sound.Play();
            startedMusicInRoom = true;
        }
        if (!startedMusicInRoom && roomId == 2)
        {
            if (!solved[roomId])
            {
                Debug.Log("puzzle1");
            }
            else 
            {
                Debug.Log("puzzle1 solved");
            }
            sound.clip = insideDay;
            sound.volume = 0.4f;
            sound.Play();
            startedMusicInRoom = true;

            ////also activate kid
            //end = true;
        }
        /*if (!startedMusicInRoom && room == 4)
        {
            sound.clip = done;
            sound.volume = 0.4f;
            sound.Play();
            startedMusicInRoom = true;
            Debug.Log("startRoom");

            //also activate kid
            end = true;
        }*/
    }

    public void changeRoom(string name)
    {
        solved[roomId] = true;
        roomId = getId(name);
    }

    //returns -1 if no room with that name was in the list
    public int getId(string name)
    {
        switch (name)
        {
            case "startRoom":
                return 0;
            case "puzzle1":
                return 1;
            case "endRoom":
                return 2;
            default:
                Debug.Log("Room with name " + name + " was not found");
                return -1;
        }
    }

}
