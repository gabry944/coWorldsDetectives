using UnityEngine;
using System.Collections;

public class footstepBehavior : MonoBehaviour {

    /*public GameObject left;
    public GameObject left1;
    public GameObject left2;
    public GameObject left3;
    public GameObject left4;
    public GameObject left5;
    public GameObject left6;

    public GameObject right;
    public GameObject right1;
    public GameObject right2;
    public GameObject right3;
    public GameObject right4;
    public GameObject right5;
    public GameObject right6;*/

    // Make sure these two have the same lenght 
    public GameObject[] footstepsleft;
    public GameObject[] footstepsRight;

    public Vector3[] path;
    public Vector3 startPosLeft;
    public Vector3 startPosRight;

    private bool playWalk;
    private int step;
    private int itteration;


    void Start () {
        playWalk = false;
        PlayWalk();

        for (int i = 0; i < footstepsleft.Length; i++)
        {
            footstepsleft[i].transform.position = startPosLeft;
            footstepsRight[i].transform.position = startPosRight;
        }

        InvokeRepeating("Walk", 0, 1.0f);

    }
	
	void Update () {
        
    }

    private void Walk()
    {
        if (playWalk)
        {
            if (step < path.Length)
            {
                if (itteration >= footstepsleft.Length)
                    itteration = 0;

                Vector3 posLeft = path[step] + new Vector3(0f, 0f, 0.07f);
                Vector3 posRight = path[step] + new Vector3(0f, 0f, -0.07f);
                footstepsleft[itteration].transform.position = posLeft;
                footstepsRight[itteration].transform.position = posRight;
                step++;
                itteration++;

            }
            else
                playWalk = false;
        }
    }

    public void PlayWalk()
    {
        if(!playWalk)
        {
            step = 0;
            itteration = 0;
            playWalk = true;
        }
    }
}
