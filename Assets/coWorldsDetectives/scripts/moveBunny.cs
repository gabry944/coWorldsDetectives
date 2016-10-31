using UnityEngine;
using System.Collections;

public class touchTrigger : MonoBehaviour {

    public Vector3 goalPos;
    public float speed;

    private Animator anim;
    private Vector3 direction;
    private float totalDistance = 0;
    private float walkedDistance = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //update animator
        anim.SetFloat("Moving", true);
        direction = goalPos - gameObject.transform.position;
        distance = direction.magnitude();
        direction = direction.normalize();
	}
	
	// Update is called once per frame
	void Update () {
        while(moving){
            //move bunny
            walkedDistance += speed * Time.deltaTime;
            if(walkedDistance < totalDistance)
                gameObject.transform.position = gameObject.transform.position + direction * speed * Time.deltaTime;
            else
            {
                //update animator
                anim.SetFloat("Moving", false);
                gameObject.transform.position = goalPos;     
            }
        }
	}
}
