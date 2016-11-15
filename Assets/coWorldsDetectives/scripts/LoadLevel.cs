using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LoadLevel : MonoBehaviour {

    public string level;
    public correctKeystonePossition keystoneScript;
    public float teleportRadius = 2;
    public GameObject playerHead;
    public GameObject playerHand;
    public GameObject whiteScreen;
    public float alphaSpeed = 0.1f;

    private bool[] activated;
    private Transform teleporterTransform;
    private bool start = true;
    private bool teleport = false;
    private AudioSource sound;
    private bool soundStarted = false;

    // Use this for initialization
    void Start () {
	    teleporterTransform = GetComponent<Transform>();
        sound = GetComponent<AudioSource>();
        if (GameState.Instance.arrived_with_teleporter)
        {
            Color newColor = whiteScreen.GetComponent<MeshRenderer>().material.color;
            newColor.a = 1;
            whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            if (GameState.Instance.arrived_with_teleporter)
            {
                float alpha = whiteScreen.GetComponent<MeshRenderer>().material.color.a - Time.deltaTime * alphaSpeed;
                Color newColor = whiteScreen.GetComponent<MeshRenderer>().material.color;
                newColor.a = alpha;
                whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;

                if (alpha < 0.01)
                {
                    newColor.a = 0;
                    whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;
                    Debug.Log("Teleported");
                    start = false;
                }
            }
            else
                start = false;
        }

        activated = keystoneScript.activated;
        for (int i = 0; i < 4; i++)
        {
            if (activated[i])
            {
                Vector2 headPos = new Vector2(playerHead.transform.position.x, playerHead.transform.position.z);
                Vector2 handPos = new Vector2(playerHand.transform.position.x, playerHand.transform.position.z);
                Vector2 teleportPos = new Vector2(teleporterTransform.position.x, teleporterTransform.position.z);
                float headDist = (headPos - teleportPos).magnitude;
                float handDist = (handPos - teleportPos).magnitude;
                if (headDist < teleportRadius && handDist < teleportRadius)
                {
                    teleport = true;
                }
            }
        }

        if (teleport)
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        if (!soundStarted)
        {
            Debug.Log("Sound");
            sound.Play();
            soundStarted = true;
        }

        float alpha = whiteScreen.GetComponent<MeshRenderer>().material.color.a + Time.deltaTime * alphaSpeed;
        Color newColor = whiteScreen.GetComponent<MeshRenderer>().material.color;
        newColor.a = alpha;
        whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;

        //when the user sees only white, load next level
        if (alpha > 0.99)
        {
            Debug.Log("Teleport");
            GameState.Instance.arrived_with_teleporter = true;
            teleport = false;
            Application.LoadLevelAsync(level);
            start = true;
            newColor.a = 1;
            whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }
}
