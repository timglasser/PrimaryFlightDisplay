using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    // Tweak to adjust top speed:

    public float accel = 30.0f; // meters/second. This might be way off
    public float lifeTime = 3.0f;
    public GameObject explosion;
    private float spawnTime = 0.0f;

    void OnEnabled()
    {
        spawnTime = Time.time;
    }

    void FixedUpdate()
    {
        // Thrust straight at target:
        Vector3 thrust = (transform.forward * accel) * Time.deltaTime;
        // now apply friction and thrust:
        GetComponent<Rigidbody>().AddForce(thrust, ForceMode.VelocityChange);
    }


    void Explode()
    {
        explosion.transform.position = this.transform.position;
        var exp = explosion.GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Heli1")
        {            
            explosion.SetActive(true);
            Debug.Log("triggered by " + other.name);
            Explode();
        }
    }

    // Use this for initialization
    void Start () {
      
    }

    void Update()
    {
        if (Time.time > (spawnTime + lifeTime) && spawnTime != 0.0f)
        {
            Explode();// self destruct the missile if too old
        }
    }
}
