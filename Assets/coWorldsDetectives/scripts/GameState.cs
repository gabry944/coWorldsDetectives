using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public bool start = true;
    public bool arrived_with_teleporter = false;
    public static GameState Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //Object.DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
