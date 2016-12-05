using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class moveWater : MonoBehaviour {

    public GameObject water;
    public GameObject tree;

    private bool moveTree;
    private bool lowerWater;

    private Vector3 deltaWaterMovement;
    private Vector3 deltaTreeMovement;

    private AudioSource sound;
    private bool soundStarted = false;

    // Use this for initialization
    void Start () {
        moveTree = false;
        lowerWater = false;
        deltaWaterMovement = new Vector3(0, 0.001f, 0);
        deltaTreeMovement = new Vector3(0, 0, 0.2f);
        sound = GetComponent<AudioSource>();

        int id = GameState.Instance.roomId;
        if (GameState.Instance.solved[id])
        {
            tree.transform.eulerAngles = new Vector3( tree.transform.eulerAngles.x, tree.transform.eulerAngles.y, 29);
            water.transform.position = new Vector3(water.transform.position.x, -0.12f, water.transform.position.z);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(moveTree)
        {
            Debug.Log("tree.transform.eulerAngles" + tree.transform.eulerAngles);
            tree.transform.eulerAngles = tree.transform.eulerAngles + deltaTreeMovement;
            if (tree.transform.eulerAngles.z > 26 && tree.transform.eulerAngles.z < 30)
                moveTree = false;
        }
        else if(lowerWater)
        {
            Debug.Log("water.transform.position " + water.transform.position);
            water.transform.position = water.transform.position - deltaWaterMovement;

            if (!soundStarted)
            {
                sound.Play();
                soundStarted = true;
            }

            if (water.transform.position.y < -0.12)
            {
                lowerWater = false;
                sound.Stop();
            }
        }
	
	}

    public void Unlock()
    {
        Debug.Log("Move water to reveal stone");
        moveTree = true;
        lowerWater = true;
        water.GetComponent<Renderer>().material.SetFloat("_MoveSpeedU", 1);
    }
}
