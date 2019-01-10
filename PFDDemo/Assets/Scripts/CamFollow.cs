using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public Transform target;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, target.transform.position.y + 4.75f, transform.position.z);
		transform.LookAt (target);

	}
}
