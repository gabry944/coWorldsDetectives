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
                float headDist = (playerHead.transform.position - teleporterTransform.position).magnitude;
                float handDist = (playerHand.transform.position - teleporterTransform.position).magnitude;
                if(headDist < teleportRadius && handDist < teleportRadius)
                    Teleport();
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
        if(alpha > 0.99)
            Application.LoadLevel(level);        
    }
}
