using UnityEngine;
using System.Collections;

public class SoundPuzzle1 : MonoBehaviour {

    public AudioClip look;
    private AudioSource sound;
    private bool boolLook = false;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if last room already solved
        int id = 2;
        if (GameState.Instance.solved[id])
        {
            if (!boolLook)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = look;
                    sound.Play();
                    boolLook = true;
                }
            }
        }
    }
}
