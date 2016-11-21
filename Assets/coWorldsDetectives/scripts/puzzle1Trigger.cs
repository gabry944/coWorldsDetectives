using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class puzzle1Trigger : MonoBehaviour {

    public GameObject stone1;
    public GameObject stone2;
    public moveWater script;
    
	void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("collide " +other.gameObject.name);
        if(other.gameObject.name == stone1.name || other.gameObject.name == stone2.name)
        {
            script.Unlock();
        }
    }
}
