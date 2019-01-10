using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof (Rigidbody))]
//[RequireComponent (typeof (MovementMotor))]

public class PlayerInput : MonoBehaviour {

//	private MovementMotor motor;
//	private HeliAnim helimotor;

    public Sender keyDownEvent;
    public Sender keyUpEvent;
	
	// Use this for initialization
	void Start () 
	{
	

	//	if (!motor)
		//	motor = GetComponent<MovementMotor> ();
		//	helimotor = GetComponent<HeliAnim> ();
	//		motor.MovementDirection = Vector3.zero;
	//		motor.FacingDirection = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            keyDownEvent.Send(this);
        }

        else if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            keyUpEvent.Send(this);
        }

	//	helimotor.Controls(CrossPlatformInputManager.GetAxis("Vertical"), CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical2"), CrossPlatformInputManager.GetAxis("Horizontal2"));
	}
}
