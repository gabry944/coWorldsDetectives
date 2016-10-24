using UnityEngine;
using System.Collections;

public class correctKeystonePossition : MonoBehaviour {
    
    public SphereCollider[] trigger;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider other)
    {
        //if(Player not holding object)
        //{
        //place object at center of the closest sphere collider        
            Vector3 pos = other.gameObject.transform.position;
            for (int i = 0; i < trigger.Length; i++) {
                Vector3 spherePos = transform.position - transform.rotation * trigger[i].center;
                Vector3 dist = spherePos - pos;
                float length = dist.magnitude;
                Debug.Log("dist: " + length);
                if (length <= trigger[i].radius)
                {
                    Debug.Log("Found closest");
                    Vector3 rot = getRotForSphere(trigger[i]);
                    other.gameObject.transform.position.Set(spherePos.x, spherePos.y, spherePos.z);
                    other.gameObject.transform.eulerAngles = rot;
                    Debug.Log("Repositioned");
                    break;
                }
            }
        //}
    }

    private Vector3 getRotForSphere(SphereCollider sphere)
    {
        if (sphere.center.z < 0)
            return new Vector3(0, 0, 0);
        else if (sphere.center.z > 0)
            return new Vector3(0,180,0);
        else if(sphere.center.x > 0)
            return new Vector3(0, -90, 0);
        else
            return new Vector3(0, 90, 0);
    }
}
