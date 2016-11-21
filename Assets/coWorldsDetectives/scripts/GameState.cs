using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public bool start = true;
    public bool arrived_with_teleporter = false;
    public static GameState Instance;
    public int room = 0;
    public bool startedMusicInRoom = true;
    public bool end = false;

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

    }
	
	// Update is called once per frame
	void Update () {
	    if(!startedMusicInRoom && room == 1)
        {
            sound.clip = insideDay;
            sound.volume = 0.4f;
            sound.Play();
            startedMusicInRoom = true;
            Debug.Log("puzzle1");
        }
        if (!startedMusicInRoom && room == 2)
        {
            sound.clip = insideNight;
            sound.volume = 0.5f;
            sound.Play();
            startedMusicInRoom = true;
            Debug.Log("endRoom");
        }
        if (!startedMusicInRoom && room == 3)
        {
            sound.clip = done;
            sound.volume = 0.5f;
            sound.Play();
            startedMusicInRoom = true;
            Debug.Log("startRoom");

            //also activate kid
            end = true;
        }
    }
}
