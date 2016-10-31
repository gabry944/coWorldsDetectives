using UnityEngine;
using System.Collections;

public class moveBunny : MonoBehaviour {

    public Vector3 goalPos;
    public float speed;

    private Animator anim;
    private bool moving;
    private Vector3 direction;
    private float totalDistance = 0;
    private float walkedDistance = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //update animator
        moving = true;
        anim.SetBool("Moving", moving);

        direction = goalPos - gameObject.transform.position;
        totalDistance = direction.magnitude;
        direction.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
        if(moving){
            //move bunny
            walkedDistance += speed * Time.deltaTime;
            if (walkedDistance < totalDistance)
                gameObject.transform.position = gameObject.transform.position + direction * speed * Time.deltaTime;
            else
            {
                //update animator
                moving = false;
                anim.SetBool("Moving", moving);
                //gameObject.transform.position = goalPos;     
            }
        }
	}
}
