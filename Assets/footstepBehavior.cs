using UnityEngine;
using System.Collections;

public class footstepBehavior : MonoBehaviour {

    public GameObject leftFoot;
    public GameObject rightFoot;

    public Vector3[] path;
    public Vector3 startPosLeft;
    public Vector3 startPosRight;
    public int trackLength; // the number of footsteps pairs that will be after each other in the animation
    public int stepSize;


    private GameObject[] footstepsLeft;
    private GameObject[] footstepsRight;
    private bool playWalk;
    private int step;
    private int itteration;


    void Start () {
        playWalk = false;
        PlayWalk();

        footstepsLeft = new GameObject[trackLength];
        footstepsRight = new GameObject[trackLength];

        for (int i = 0; i < trackLength; i++)
        {
            GameObject left = Instantiate(leftFoot);
            left.transform.position = startPosLeft;
            footstepsLeft[i] = left;

            GameObject right = Instantiate(rightFoot);
            right.transform.position = startPosRight;
            footstepsLeft[i] = right;
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
                if (itteration >= footstepsLeft.Length)
                    itteration = 0;

                Vector3 posLeft = path[step] + new Vector3(0f, 0.001f, -0.07f);
                Vector3 posRight = path[step] + new Vector3(0f, 0.001f, 0.07f);
                footstepsLeft[itteration].transform.position = posLeft;
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
