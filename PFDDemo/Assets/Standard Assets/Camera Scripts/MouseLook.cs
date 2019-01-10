using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;


    //var footstepSignals : SignalSender;

    private Transform tr;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 localVelocity = Vector3.zero;
    private float speed = 0;
    private float angle = 0;
    private float lowerBodyDeltaAngle = 0;
    private float idleWeight = 0;
    private Vector3 lowerBodyForwardTarget = Vector3.forward;
    private Vector3 lowerBodyForward = Vector3.forward;

    public float YRot;




    /*
        void LateUpdate()
        {

            //float idle =Mathf.InverseLerp(minWalkSpeed, maxIdleSpeed, speed);



            // Calculate target angle to rotate lower body by in order
            // to make feet run in the direction of the velocity
            float lowerBodyDeltaAngleTarget = Mathf.DeltaAngle(
                HorizontalAngle(tr.rotation * animatedLocalVelocity),
                HorizontalAngle(velocity)
            );

            // Lerp the angle to smooth it a bit
            lowerBodyDeltaAngle = Mathf.LerpAngle(lowerBodyDeltaAngle, lowerBodyDeltaAngleTarget, Time.deltaTime * 10);

            // Update these so they're ready for when we go into idle
            lowerBodyForwardTarget = tr.forward;
            lowerBodyForward = Quaternion.Euler(0, lowerBodyDeltaAngle, 0) * lowerBodyForwardTarget;


            // Turn the lower body towards it's target direction
            lowerBodyForward = Vector3.RotateTowards(lowerBodyForward, lowerBodyForwardTarget, Time.deltaTime * 520 * Mathf.Deg2Rad, 1);

            // Calculate delta angle to make the lower body stay in place
            lowerBodyDeltaAngle = Mathf.DeltaAngle(
                HorizontalAngle(tr.forward),
                HorizontalAngle(lowerBodyForward)
            );

            // If the body is twisted more than 80 degrees,
            // set a new target direction for the lower body, so it begins turning
            if (Mathf.Abs(lowerBodyDeltaAngle) > 80)
                lowerBodyForwardTarget = tr.forward;


            // Create a Quaternion rotation from the rotation angle
            var lowerBodyDeltaRotation : Quaternion = Quaternion.Euler(0, lowerBodyDeltaAngle, 0);

            // Rotate the whole body by the angle
            rootBone.rotation = lowerBodyDeltaRotation * rootBone.rotation;

            // Counter-rotate the upper body so it won't be affected
            upperBodyBone.rotation = Quaternion.Inverse(lowerBodyDeltaRotation) * upperBodyBone.rotation;
        }

      */  



    void Update ()
	{
        
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY = YRot * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
        
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
       
		{
			rotationY = YRot * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(0.0f,rotationY, 0);
		}
        

       transform.localEulerAngles= new Vector3(0.0f, YRot * sensitivityX, 0.0f);
    }
	
	void Start ()
	{
		// Make the rigid body not change rotation
	//	if (GetComponent<Rigidbody>())
		//	GetComponent<Rigidbody>().freezeRotation = true;
	}
}