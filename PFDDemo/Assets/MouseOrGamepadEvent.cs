//To the extent possible under law, 
//Tim Glasser 
//tim_glasser@hotmail.com
//has waived all copyright and related or neighboring rights and responsibilties to
//Unity MouseOrGamepadEvent C# Classes.
//This work is published from:
//California.

// As indicated by the Creative Commons, the text on this page may be copied, 
// modified and adapted for your use, without any other permission from the author.

// Please do not remove this notice
using UnityEngine;
using System.Collections;

public class MouseOrGamepadEvent : MonoBehaviour {

	public GameObject chestController;
	public GameObject headController;
	public float DirectionDampTime = .25f;
//	private MovementMotor motor;
	private Transform character;
	public GameObject cursor;
	private Rigidbody rb;
	private Animator avatar;


	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseX;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float fireSensitivity = 0.2f;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

//	public Sender keyDownEvent;
//	public Sender keyUpEvent ;
	private bool state = false;
	
	#if UNITY_IPHONE || UNITY_ANDROID
	private CNJoystick[] joysticks;
	private CNJoystick joystickRight, joystickLeft;

	void Start () {

		// set layer weights
		avatar = GetComponent<Animator> ();
		avatar.SetLayerWeight(1,0);


		joysticks = FindObjectsOfType <CNJoystick>() as CNJoystick [];	
		joystickRight = joysticks [0];
		joystickLeft = joysticks [1];

		if (!character)
			character = transform;
		
		if (!motor)
			motor = GetComponent<ShooterMovementMotor> ();
		
		motor.movementDirection = Vector3.zero;
		motor.facingDirection = Vector3.zero;


		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}


	void OnDisable () {
		if (joystickLeft) 
			joystickLeft.enabled = false;
		
		if (joystickRight)
			joystickRight.enabled = false;
	}
	
	void OnEnable () {
		if (joystickLeft) 
			joystickLeft.enabled = true;
		
		if (joystickRight)
			joystickRight.enabled = true;
	}
	void Awake ()
	{

	}	

	void Update () {
		// HANDLE CHARACTER MOVEMENT DIRECTION
		#if UNITY_IPHONE || UNITY_ANDROID

		var movement = new Vector3(
			joystickLeft.GetAxis("Horizontal"),
			0f,
			joystickLeft.GetAxis("Vertical"));


		Vector3 heading_fore = transform.TransformDirection (Vector3.forward * movement.z);
		Vector3 heading_right = transform.TransformDirection (Vector3.right * movement.x);
		motor.movementDirection = heading_right + heading_fore;

		// transform to local coordinate system to control state engine
		Vector3 curVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		avatar.SetFloat ("run", curVel.z);

//		Debug.Log( "strafe " + curVel.x);
		avatar.SetFloat ("strafe", curVel.x);
		avatar.SetFloat ("speed", (curVel.x + curVel.z)*(curVel.x + curVel.z));//, DirectionDampTime, Time.deltaTime);

		/*
		if (state == false && joystickRight.tapCount > 0) {
			mouseDownEvent.Send (this);
			state = true;
		}
		else if (joystickRight.tapCount <= 0) {
			mouseUpEvent.Send (this);
			state = false;
		}	
*/
		/* HANDLE ROTATION
		float rotationX = transform.localEulerAngles.y + joystickRight.GetAxis("Horizontal") * sensitivityX;
		rotationY +=  joystickRight.GetAxis("Vertical") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		*/
		var moveright = new Vector3(
			joystickRight.GetAxis("Horizontal"),
			0f,
			joystickRight.GetAxis("Vertical"));

		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + joystickRight.GetAxis("Horizontal") * sensitivityX;
			
			rotationY += joystickRight.GetAxis("Vertical") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, joystickRight.GetAxis("Horizontal") * sensitivityX, 0);

			// fire weapn on right keypad vertical
			if (state == false && joystickRight.GetAxis("Vertical") > fireSensitivity) {
				keyDownEvent.Send(this);
				avatar.SetLayerWeight(1,1);
				avatar.SetBool ("shoot", true);
				Debug.Log ("weapon on");
				state = true;
			}
			
			else if (state == true && joystickRight.GetAxis("Vertical") <= fireSensitivity) {
				keyUpEvent.Send (this);
				avatar.SetLayerWeight(1,0);
				avatar.SetBool ("shoot", false);
				Debug.Log ("weapon off");
				state = false;
			}
		}
		else
		{
			rotationY += joystickRight.GetAxis("Vertical") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}


		#else
		motor.movementDirection = Input.GetAxis ("Horizontal") * screenMovementRight + Input.GetAxis ("Vertical") * screenMovementForward;
		#endif
	}

	#endif
	/*
	void Update () {
	#if UNITY_IPHONE || UNITY_ANDROID

	#else	
		#if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3)
		// On consoles use the right trigger to fire
		var fireAxis : float = Input.GetAxis("TriggerFire");
		if (state == false && fireAxis >= 0.2) {
			keyDownEvent.Send (this);
			state = true;
		}
		else if (state == true && fireAxis < 0.2) {
			keyUpEvent.Send (this);
			state = false;
		}

	#else
		if (state == false && Input.GetButtonDown ("Fire1")) {
			keyDownEvent.Send(this);
			state = true;
		}
		
		else if (state == true && Input.GetButtonUp ("Fire1")) {
			keyUpEvent.Send (this);
			state = false;
		}
	#endif
#endif
}	
*/
}
	