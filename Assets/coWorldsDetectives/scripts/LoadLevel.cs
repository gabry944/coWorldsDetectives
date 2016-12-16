using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LoadLevel : MonoBehaviour {

    public string NorthLevel;
    public string WestLevel;
    public string SouthLevel;
    public string EastLevel;
    public correctKeystonePosition keystoneScript;
    public float teleportRadius = 2;
    public GameObject playerHead;
    public GameObject playerHand;
    public GameObject whiteScreenPerson;
    public GameObject whiteScreenSpirit;
    public float alphaSpeed = 0.1f;

    private string nextLevel;
    private bool[] activated;
    private Transform teleporterTransform;
    private bool start = true;
    private bool teleport = false;
    private AudioSource sound;
    private bool soundStarted = false;
    private bool loading = false;

    // Use this for initialization
    void Start () {
	    teleporterTransform = GetComponent<Transform>();
        sound = GetComponent<AudioSource>();
        if (GameState.Instance.arrived_with_teleporter)
        {
            Color newColor = whiteScreenPerson.GetComponent<MeshRenderer>().material.color;
            newColor.a = 1;
            whiteScreenPerson.GetComponent<MeshRenderer>().material.color = newColor;
            whiteScreenSpirit.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            if (GameState.Instance.arrived_with_teleporter)
            {
                float alpha = whiteScreenPerson.GetComponent<MeshRenderer>().material.color.a - Time.deltaTime * alphaSpeed;
                Color newColor = whiteScreenPerson.GetComponent<MeshRenderer>().material.color;
                newColor.a = alpha;
                whiteScreenPerson.GetComponent<MeshRenderer>().material.color = newColor;
                whiteScreenSpirit.GetComponent<MeshRenderer>().material.color = newColor;

                if (alpha < 0.01)
                {
                    newColor.a = 0;
                    whiteScreenPerson.GetComponent<MeshRenderer>().material.color = newColor;
                    whiteScreenSpirit.GetComponent<MeshRenderer>().material.color = newColor;
                    start = false;
                }
            }
            else
                start = false;
        }

        activated = keystoneScript.activated;
        for (int i = 0; i < activated.Length; i++)
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
                    //set teleport destination
                    if (i == 1)
                        nextLevel = NorthLevel;
                    else if (i == 2)
                        nextLevel = WestLevel;
                    else if (i == 3)
                        nextLevel = SouthLevel;
                    else //if (i == 4)
                        nextLevel = EastLevel;

                    //activate teleport
                    teleport = true;
                }
            }
        }

        if (teleport && !loading)
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        if (!soundStarted)
        {
            sound.Play();
            soundStarted = true;
        }

        float alpha = whiteScreenPerson.GetComponent<MeshRenderer>().material.color.a + Time.deltaTime * alphaSpeed;
        Color newColor = whiteScreenPerson.GetComponent<MeshRenderer>().material.color;
        newColor.a = alpha;
        whiteScreenPerson.GetComponent<MeshRenderer>().material.color = newColor;
        whiteScreenSpirit.GetComponent<MeshRenderer>().material.color = newColor;

        //when the user sees only white, load next level
        if (alpha > 0.99)
        {
            GameState.Instance.arrived_with_teleporter = true;
            GameState.Instance.startedMusicInRoom = false;
            GameState.Instance.changeRoom(nextLevel);
            teleport = false;
            loading = true;
            Application.LoadLevelAsync(nextLevel);
            start = true;
            newColor.a = 1;
            whiteScreenPerson.GetComponent<MeshRenderer>().material.color = newColor;
            whiteScreenSpirit.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }
}
