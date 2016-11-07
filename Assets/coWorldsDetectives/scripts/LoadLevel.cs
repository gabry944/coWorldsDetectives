using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    public string level;
    public correctKeystonePossition keystoneScript;

    private bool[] activated;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        activated = keystoneScript.activated;
        for (int i = 0; i < 4; i++)
            if (activated[i])
                Teleport();
    }

    public void Teleport()
    {
        Application.LoadLevel(level);        
    }
}
