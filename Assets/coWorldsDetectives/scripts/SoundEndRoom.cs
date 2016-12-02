using UnityEngine;
using System.Collections;

public class SoundEndRoom : MonoBehaviour {

    public AudioClip babyBunny;
    private AudioSource sound;
    private bool boolBabyBunny = false;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update ()
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
