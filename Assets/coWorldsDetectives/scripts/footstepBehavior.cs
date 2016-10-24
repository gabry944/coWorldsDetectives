using UnityEngine;
using System.Collections;

public class footstepBehavior : MonoBehaviour
{

    public GameObject leftFoot;
    public GameObject rightFoot;

    public Vector3[] path;
    public Vector3 startPosLeft;
    public Vector3 startPosRight;
    public float startRot; // rakt fram är negative x
    public int trackLength; // the number of footsteps pairs that will be after each other in the animation
    public float stepSize;
    public float stepWith;

    private GameObject[] footstepsLeft;
    private GameObject[] footstepsRight;
    private bool playWalk;
    private int step;
    private int itteration;
    private Vector3 rotaionAxis;

    void Start()
    {
        playWalk = false;
        PlayWalk();

        getCurrentAngle(new Vector2 (startPosLeft.x, startPosLeft.z), new Vector2(startPosRight.x, startPosRight.z));

        footstepsLeft = new GameObject[trackLength];
        footstepsRight = new GameObject[trackLength];
        rotaionAxis = new Vector3(0f, 1f, 0f);

        for (int i = 0; i < trackLength; i++)
        {
            GameObject left = Instantiate(leftFoot);
            left.transform.position = startPosLeft;
            left.transform.Rotate(rotaionAxis, startRot);
            footstepsLeft[i] = left;

            GameObject right = Instantiate(rightFoot);
            right.transform.position = startPosRight;
            left.transform.Rotate(rotaionAxis, startRot);
            footstepsRight[i] = right;
        }

        InvokeRepeating("Walk", 0, 1.0f);
    }

    void Update()
    {

    }

    private void Walk()
    {
        if (playWalk)
        {
            if (step < path.Length)
            {
                if (itteration >= trackLength)
                    itteration = 0;

                //rakt fram är negative x


                Vector3 posLeft = path[step] + new Vector3(0f, 0.001f, -stepWith / 2);
                Vector3 posRight = path[step] + new Vector3(0f, 0.001f, stepWith / 2);
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
        if (!playWalk)
        {
            step = 0;
            itteration = 0;
            playWalk = true;
        }
    }

    public float getCurrentAngle(Vector2 a, Vector2 b)
    {
        Debug.Log("Amgle(): " + Vector2.Angle(a, b));
       // Debug.Log("Amgle(a-b): " + Vector2.Angle(a -b));
        Debug.Log("Math: " + System.Math.Atan2(b.y - a.y, b.x - a.x));
        return (float) System.Math.Atan2(b.y - a.y, b.x - a.x);
    }
}
