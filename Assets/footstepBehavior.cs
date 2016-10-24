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

    private Vector3 startPosLeft;
    private Vector3 startPosRight;
    private bool playWalk;

    void Start () {
        startPosLeft = new Vector3(-1.846f, 2.193111f, -2.272608f);
        startPosRight = new Vector3(-1.846f, 2.193111f, -2.126f);
        playWalk = false;

        for (int i = 0; i < footstepsleft.Length; i++)
        {
            footstepsleft[i].transform.position = startPosLeft;
            footstepsRight[i].transform.position = startPosRight;
        }

    }
	
	void Update () {
        if (playWalk)
        {
            int j = 0;
            do
            {
                for (int i = 0; i < footstepsleft.Length; i++)
                {
                    Vector3 posLeft = path[i] + new Vector3(0f, 0f, 0.07f);
                    Vector3 posRight = path[i] + new Vector3(0f, 0f, -0.07f);
                    footstepsleft[i].transform.position = posLeft;
                    footstepsRight[i].transform.position = posRight;
                    j++;
                    if (j >= path.Length)
                        break;
                }
            } while (j < path.Length);
        }
    }

    public void PlayWalk()
    {
        playWalk = true;
    }
}
