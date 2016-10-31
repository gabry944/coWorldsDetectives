using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class footstepBehavior : MonoBehaviour
{

    public GameObject leftFoot;
    public GameObject rightFoot;

    public Vector3[] path; // måste ha minst 2 object!!!!!
    public int trackLength; // the number of footsteps pairs that will be after each other in the animation
    public float stepSize;
    public float stepWith;

    private GameObject[] footstepsLeft;
    private GameObject[] footstepsRight;
    private bool playWalk;
    private int LeftStepIt;
    private int RightStepIt;
    private Vector3 rotaionAxis;
    private List<Vector3> stepsLeft;
    private List<Vector3> stepsRight;
    private List<Quaternion> stepRotation;
    private float startRot; // rakt fram är negative z
    private bool left;

    void Start()
    {
        playWalk = false;

        Vector2 line = new Vector2(path[1].x - path[0].x, path[1].z - path[0].z);
        startRot = Vector2.Angle(new Vector2(0,-1), line);
        Debug.Log("StartRot = " + startRot);
        Vector2 ortLine = PerpendicularClockwise(line);

        Vector3 startPosLeft = new Vector3(path[0].x + ortLine.x * stepWith / 2, path[0].y + 0.001f, path[0].z + ortLine.y *stepWith /2);
        Vector3 startPosRight = new Vector3(path[0].x - ortLine.x * stepWith / 2, path[0].y + 0.001f, path[0].z - ortLine.y * stepWith /2);

        stepsLeft = new List<Vector3>();
        stepsRight = new List<Vector3>();
        stepRotation = new List<Quaternion>();

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
            right.transform.Rotate(rotaionAxis, startRot);
            footstepsRight[i] = right;
        }

        Debug.Log("footstepsRight[0].transform.rotation = " + footstepsRight[0].transform.rotation +" "+ footstepsRight[0].transform.rotation.eulerAngles);

        leftFoot.SetActive(false);
        rightFoot.SetActive(false);

        calculateRoute();
        InvokeRepeating("Walk", 0, 1.0f);
        PlayWalk();
    }

    void Update()
    {

    }

    private void calculateRoute()
    {
        int step = 0;
        Quaternion rotation;

        while (step < path.Length -1)
        {
            Vector2 line = new Vector2(path[step + 1].x - path[step].x, path[step + 1].z - path[step].z);
            Vector2 ortLine = PerpendicularClockwise(line);
            rotation = getCurrentAngle(line);
            float dist = Vector2.Distance(new Vector2(path[step].x, path[step].z), new Vector2(path[step+1].x, path[step+1].z));

            Vector3 direction = path[step + 1] - path[step];
            int noOfParts = (int) (dist/stepSize);
            int part = 0;
            
            Vector3 posLeft = path[step] + new Vector3(ortLine.x * stepWith / 2, 0.001f, ortLine.y * stepWith / 2);
            stepsLeft.Add(posLeft);
            stepRotation.Add(rotation);
            Vector3 posRight = path[step] + new Vector3(-ortLine.x * stepWith / 2, 0.001f, -ortLine.y * stepWith / 2);
            stepsRight.Add(posRight);
            stepRotation.Add(rotation);
            part++;


            while (part < noOfParts-1)
            {
                posLeft = path[step]+ (stepSize*part)*direction + new Vector3(ortLine.x * stepWith / 2, 0.001f, ortLine.y * stepWith / 2);
                stepsLeft.Add(posLeft);
                stepRotation.Add(rotation);
                part++;

                posRight = path[step] + (stepSize*(part+1))*direction + new Vector3(-ortLine.x * stepWith / 2, 0.001f, -ortLine.y * stepWith / 2);
                stepsRight.Add(posRight);
                stepRotation.Add(rotation);
                part++;
            }
            Debug.Log("step " + step + ", noOfParts " + noOfParts + ", stepsLeft.Count " + stepsLeft.Count + ", stepsRight.Count " + stepsRight.Count + ", rotation " + rotation  +" "+ rotation.eulerAngles);
            step++;
        }
    }

    private void Walk()
    {
        if (playWalk)
        {
            if (left)
            {
                if (LeftStepIt >= trackLength)
                    LeftStepIt = 0;
                if (stepsLeft.Count > 0 )
                {
                    footstepsLeft[LeftStepIt].transform.position = stepsLeft[0];
                    footstepsLeft[LeftStepIt].transform.rotation = stepRotation[0];
                    stepsLeft.RemoveAt(0);
                    stepRotation.RemoveAt(0);
                    LeftStepIt++;
                }
                left = false;

            }
            else
            {
                if (RightStepIt >= trackLength)
                    RightStepIt = 0;
                if (stepsRight.Count > 0)
                {
                    footstepsRight[RightStepIt].transform.position = stepsRight[0];
                    footstepsRight[RightStepIt].transform.rotation = stepRotation[0];
                    stepsRight.RemoveAt(0);
                    stepRotation.RemoveAt(0);
                    RightStepIt++;
                }
                left = true;
            }
            Debug.Log(" walk stepsLeft.Count = " + stepsLeft.Count + ", stepsRight.Count = " + stepsRight.Count);
            if (stepsLeft.Count == 0 && stepsRight.Count == 0)
                playWalk = false;
        }
    }

    public void PlayWalk()
    {
        if (!playWalk)
        {
            LeftStepIt = 0;
            RightStepIt = 0;
            playWalk = true;
            left = true;
            Debug.Log("playwalk");
        }
    }

    public Quaternion getCurrentAngle(Vector2 a)
    {
        Vector2 startRot = new Vector2(0, -1);
        float angle = Vector2.Angle(startRot, a);
        Debug.Log("Angle: " + angle);
        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(0f, angle, 0f);
        return quat;
    }

    public Vector2 PerpendicularClockwise(Vector2 v2)
    {
        return new Vector2(-v2.y, v2.x);
    }
}
