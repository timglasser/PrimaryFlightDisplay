using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 1.0f; 
    private float spawnTime = 0.0f;
 
    void OnEnable()
    {     
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time > spawnTime + lifeTime )
        {
            MyCache.Destroy(gameObject);// get rid of the entity if too old
        }
    }
    // Use this for initialization
    void Start()
    {

    }
}
