using UnityEngine;
using System.Collections;

public class GuidedMissile : MonoBehaviour {

    public Transform myTarget;

    // Tweak to adjust top speed:
    float friction = 0.985f; // applied each frame 
    float accel = 30.0f; // meters/second. This might be way off
    void FixedUpdate()
    {
        Vector3 targPos = myTarget.position;

        // OPTIONAL lead target if not very close to it:
        if ((targPos - transform.position).sqrMagnitude > 100)
        {
            targPos = myTarget.transform.position + myTarget.GetComponent<Rigidbody>().velocity;
         
            // if somewhere with gravity, looks cool to aim above, during approach:
            targPos += Vector3.up * 2;
        }
        // Thrust straight at target:
        Vector3 thrust = (targPos - transform.position).normalized
                          * accel * Time.deltaTime;
        // now apply friction and thrust:
        GetComponent<Rigidbody>().AddForce(  thrust, ForceMode.VelocityChange);
        // set decorative facing based on current velocity:
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter()
    {


    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
