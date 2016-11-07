using UnityEngine;
using System.Collections;

public class correctKeystonePossition : MonoBehaviour {
    
    public SphereCollider[] trigger;
    public string[] nextRooms;
    //called with Application.LoadLevel("HighScore");

    public bool[] activated;

    // Use this for initialization
    void Start () {
        activated = new bool[4];
        for (int i = 0; i < 4; i++)
            activated[i] = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider other)
    {
        //if the keystone isn't held by the player
        if (other.gameObject.GetComponent<NewtonVR.NVRInteractableItem>().AttachedHand == null)
        {
            //Activate portal and place keystone at center of the closest sphere collider
            Vector3 teleporterPos = other.gameObject.transform.position;
            for (int i = 0; i < trigger.Length; i++) {
                //get the distance from this sphere trigger
                Vector3 fromCenterPos = new Vector3(transform.localScale.x * trigger[i].center.x, transform.localScale.y * trigger[i].center.y, transform.localScale.z * trigger[i].center.z);
                Vector3 spherePos = transform.position + transform.rotation * fromCenterPos;
                Vector3 dist = spherePos - teleporterPos;
                float length = dist.magnitude;

                //if the keystone is inside this sphere
                if (length <= trigger[i].radius)
                {
                    //activate the direction of the portal
                    activated[i] = true;

                    //put it in position
                    //TODO: Animation
                    Vector3 rot = getRotForSphere(trigger[i]);
                    other.gameObject.transform.position.Set(spherePos.x, spherePos.y, spherePos.z);
                    other.gameObject.transform.eulerAngles = rot;
                    break;
                }
            }
        }
    }

    //get the rotation of the keystone depending on which slot it is placed in
    private Vector3 getRotForSphere(SphereCollider sphere)
    {
        if (sphere.center.y > 0)
            return new Vector3(0, 0, 0);
        else if (sphere.center.y < 0)
            return new Vector3(0,180,0);
        else if(sphere.center.x > 0)
            return new Vector3(0, -90, 0);
        else
            return new Vector3(0, 90, 0);
    }
}
