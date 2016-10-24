﻿using UnityEngine;
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
    private int step;
    private int itteration;
    private Vector3 rotaionAxis;
    private List<Vector3> stepsLeft;
    private List<Vector3> stepsRight;
    private List<float> stepRotation;
    private float startRot; // rakt fram är negative x
    private bool left;

    void Start()
    {
        playWalk = false;
        //PlayWalk();

        startRot = getCurrentAngle(new Vector2(-1, 0), new Vector2(path[1].x- path[0].x, path[1].z- path[0].z));
        Vector3 startPosLeft = new Vector3(path[0].x, path[0].y + 0.001f, path[0].z - stepWith/2);
        Vector3 startPosRight = new Vector3(path[0].x, path[0].y + 0.001f, path[0].z + stepWith/2);

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

        calculateRoute();
        InvokeRepeating("Walk", 0, 1.0f);
    }

    void Update()
    {

    }

    private void calculateRoute()
    {
        float rotation;
        if (step == 0)
        {
            rotation = startRot;
            float dist = Vector2.Distance(new Vector2(path[step].x, path[step].z), new Vector2(path[step + 1].x, path[step + 1].z));
            Vector3 direction = path[step + 1] - path[step];
            int noOfParts = (int)(dist / stepSize);
            int part = 0;
            while (part < noOfParts - 1)
            {
                Vector3 posLeft = path[step] + (stepSize * part) * direction + new Vector3(0f, 0.001f, -stepWith / 2);
                stepsLeft.Add(posLeft);
                stepRotation.Add(rotation);
                part++;

                Vector3 posRight = path[step] + (stepSize * (part + 1)) * direction + new Vector3(0f, 0.001f, stepWith / 2);
                stepsRight.Add(posRight);
                stepRotation.Add(rotation);
                part++;
            }
            step++;
        }

        while (step < trackLength-1)
        {
            rotation = getCurrentAngle(new Vector2(path[step].x - path[step-1].x, path[step].z - path[step-1].z), new Vector2(path[step+1].x - path[step].x, path[step+1].z - path[step].z));
            float dist = Vector2.Distance(new Vector2(path[step].x, path[step].z), new Vector2(path[step+1].x, path[step+1].z));
            Vector3 direction = path[step + 1] - path[step];
            int noOfParts =(int) (dist/stepSize);
            int part = 0;
            while (part < noOfParts-1)
            {
                Vector3 posLeft = path[step]+ (stepSize*part)*direction + new Vector3(0f, 0.001f, -stepWith / 2);
                stepsLeft.Add(posLeft);
                stepRotation.Add(rotation);
                part++;

                Vector3 posRight = path[step] + (stepSize*(part+1))*direction + new Vector3(0f, 0.001f, stepWith / 2);
                stepsRight.Add(posRight);
                stepRotation.Add(rotation);
                part++;
            }
            step++;
        }
    }

    private void Walk()
    {
        if (playWalk)
        {
            if (itteration >= trackLength)
                itteration = 0;

            if (left)
            {
                if (stepsLeft.Count > 0 )
                {
                    footstepsLeft[itteration].transform.position = stepsLeft[0];
                    footstepsLeft[itteration].transform.Rotate(rotaionAxis, stepRotation[0]);
                    stepsLeft.RemoveAt(0);
                    stepRotation.RemoveAt(0);
                }

            }
            else
            {
                if (stepsRight.Count > 0)
                {
                    footstepsRight[itteration].transform.position = stepsRight[0];
                    footstepsRight[itteration].transform.Rotate(rotaionAxis, stepRotation[0]);
                    stepsRight.RemoveAt(0);
                }
            }

            itteration++;            
        }
    }

    public void PlayWalk()
    {
        if (!playWalk)
        {
            step = 1;
            itteration = 0;
            calculateRoute();
            playWalk = true;
        }
    }

    public float getCurrentAngle(Vector2 a, Vector2 b)
    {
        Debug.Log("Angle(): " + Vector2.Angle(a, b));
        return Vector2.Angle(a, b);
    }
}
