using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

    public NewtonVR.NVRHand spirit;
    public NewtonVR.NVRHand person;
    public string nextLevel;

    private float currentTime;
    private bool activated;

    // Use this for initialization
    void Start () {
        currentTime = 0;
        activated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (spirit.UseButtonPressed && person.UseButtonPressed)
        {
            currentTime += Time.deltaTime;

            //if current cube should still be lit
            if (currentTime > 1.0f && !activated)
            {
                GameState.Instance.startedMusicInRoom = false;
                activated = true;
                GameState.Instance.changeRoom(nextLevel);
                Application.LoadLevelAsync(nextLevel);
            }
        }
        else
        {
            currentTime = 0;
        }
	
	}
}
