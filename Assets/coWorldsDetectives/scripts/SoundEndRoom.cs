using UnityEngine;
using System.Collections;

public class SoundEndRoom : MonoBehaviour {

    public AudioClip babyBunny;
    public AudioClip startGame;
    public AudioClip wrong;
    private AudioSource sound;
    private bool boolBabyBunny = false;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if room already solved
        int id = GameState.Instance.roomId;
        if (!GameState.Instance.solved[id])
        {
            if (!boolBabyBunny)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = babyBunny;
                    sound.Play();
                    boolBabyBunny = true;
                }
            }
        }
    }

    public void PlayWrong()
    {
        sound.clip = wrong;
        sound.Play();
    }

    public void PlayStart()
    {
        sound.clip = startGame;
        sound.Play();
    }
}
