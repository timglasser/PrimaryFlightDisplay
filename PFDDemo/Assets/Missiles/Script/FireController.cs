using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    private Missile[] missiles;
 //   private Weapon[] guns;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    #region Messages

    public void OnFireEvent()
    {
        missiles = gameObject.GetComponentsInChildren<Missile>();

        // release a child missile, but first unparent it from the rack and enable the rigidbody
        if (missiles.Length > 0) {
            rb=missiles[0].GetComponent<Rigidbody>();
            missiles[0].gameObject.transform.parent = null;
            rb.isKinematic = false;
            missiles[0].enabled = true;
        }
    }

    public void OnMGFireEvent()
    {
        /*
        guns = gameObject.GetComponentsInChildren<Weapon>();

        // set the fire input parameter 
        if (guns.Length > 0)
        {
            rb = guns[0].GetComponent<Rigidbody>();
            Animator sm = guns[0].GetComponent<Animator>();
            sm.SetBool("Fire", true);
            rb.isKinematic = false;
            //guns[0].enabled = true;
        }
        */
    }

    public void OnMGFireOffEvent()
    {
        /*
        guns = gameObject.GetComponentsInChildren<Weapon>();

        // set the fire input parameter 
        if (guns.Length > 0)
        {
            rb = guns[0].GetComponent<Rigidbody>();
            Animator sm = guns[0].GetComponent<Animator>();
            sm.SetBool("Fire", false);
            rb.isKinematic = true;
            //guns[0].enabled = true;
        }
        */
    }
    #endregion
}
