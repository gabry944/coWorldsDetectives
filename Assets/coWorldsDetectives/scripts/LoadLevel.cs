using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start () {
	    teleporterTransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
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
                if(headDist < teleportRadius && handDist < teleportRadius)
                    Teleport();
                else
                    Debug.Log("headDist: " + headDist + " handDist: " + handDist);
            }
        }
    }

    public void Teleport()
    {
        float alpha = whiteScreen.GetComponent<MeshRenderer>().material.color.a + Time.deltaTime * alphaSpeed;
        Color newColor = whiteScreen.GetComponent<MeshRenderer>().material.color;
        newColor.a = alpha;
        whiteScreen.GetComponent<MeshRenderer>().material.color = newColor;

        //when the user sees only white, load next level
        if (alpha > 0.99)
        {
            Debug.Log("Teleport");
            Application.LoadLevel(level);
        }
        else
            Debug.Log("alpha: " + alpha);
    }
}
