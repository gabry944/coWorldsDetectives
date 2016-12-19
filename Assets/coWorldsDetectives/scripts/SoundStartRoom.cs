using UnityEngine;
using System.Collections;

public class SoundStartRoom : MonoBehaviour {

    public AudioClip hello;
    public AudioClip mission;
    public AudioClip momAndDad;
    public AudioClip honey;
    public AudioClip goodJob;
    private AudioSource sound;

    private bool boolhello = false;
    private bool boolmission = false;
    private bool boolmomAndDad = false;
    private bool boolhoney = false;
    private bool boolgoodJob = false;
    private float currentTime = 0;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //if room already solved
        if (!GameState.Instance.solved[GameState.Instance.roomId])
        {
            if (!boolhello)
            {
                if (currentTime > 10 && !sound.isPlaying)
                {
                    sound.clip = hello;
                    sound.Play();
                    boolhello = true;
                }
                currentTime += Time.deltaTime;
            }
            else if (!boolmission)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = mission;
                    sound.Play();
                    boolmission = true;
                }
            }
        }
        else if (GameState.Instance.end)
        {
            if (!boolmomAndDad)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = momAndDad;
                    sound.Play();
                    boolmomAndDad = true;
                }
            }
            else if (!boolhoney)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = honey;
                    sound.Play();
                    boolhoney = true;
                }
            }
            else if (!boolgoodJob)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = goodJob;
                    sound.Play();
                    boolgoodJob = true;
                }
            }
        }
	}
}
