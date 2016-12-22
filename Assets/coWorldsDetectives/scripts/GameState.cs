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
        solved = new bool[4];
        for (int i = 0; i < solved.Length; i++)
        {
            solved[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!startedMusicInRoom && roomId == 0)
        {
            sound.clip = outside;
            startedMusicInRoom = true;
            sound.volume = 0.3f;
            sound.Play();
        }
        if (!startedMusicInRoom && roomId == 1)
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
                }
            }
            startedMusicInRoom = true;
            sound.volume = 0.3f;
            sound.Play();
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
        if (!startedMusicInRoom && roomId == 3)
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
    }

    public void changeRoom(string name)
    {
        solved[roomId] = true;
        Debug.Log(roomId + " = true");
        roomId = getId(name);
        if(solved[getId("endRoom")] == true)
            end = true;
    }

    //returns -1 if no room with that name was in the list
    public int getId(string name)
    {
        switch (name)
        {
            case "menuRoom":
                return 0;
            case "startRoom":
                return 1;
            case "puzzle1":
                return 2;
            case "endRoom":
                return 3;
            default:
                Debug.Log("Room with name " + name + " was not found");
                return -1;
        }
    }
    public string getRoom()
    {
        switch (roomId)
        {
            case 0:
                return "menuRoom";
            case 1:
                return "startRoom";
            case 2:
                return "puzzle1";
            case 3:
                return "endRoom";
            default:
                return "null";
        }
    }
}
