
//To the extent possible under law, 
//Tim Glasser 
//tim_glasser@hotmail.com
//has waived all copyright and related or neighboring rights to
//Unity MyCache C# Classes.
//This work is published from:
//California.

// As indicated by the Creative Commons, the text on this page may be copied, 
// modified and adapted for your use, without any other permission from the author.

// Please do not remove this notice
using UnityEngine;
using System.Collections.Generic;

public class MyCache : MonoBehaviour {
   
    public static MyCache spawner;

    public ObjectCache[] caches;
 
    public Dictionary<GameObject, bool> activeCachedObjects;

    // objectCache contained within SpawnerCS
    [System.Serializable]
    public class ObjectCache
    {
        public GameObject prefab;
        public int cacheSize = 10;

        private GameObject[] objects;
        private int cacheIndex = 0;

        public void Initialize()
        {
            objects = new GameObject[cacheSize];

            // Instantiate the objects in the array and set them to be inactive
            for (var i = 0; i < cacheSize; i++)
            {
                objects[i] = Instantiate(prefab) as GameObject;
                objects[i].SetActive(false);
                objects[i].name = objects[i].name + i; // rename object based on position in the cache
            }
        }

        public GameObject GetNextObjectInCache()
        {
            GameObject obj = null;

            // The cacheIndex starts out at the position of the object created
            // the longest time ago, so that one is usually free,
            // but in case not, loop through the cache until we find a free one.
            for (int i = 0; i < cacheSize; i++)
            {
                obj = objects[cacheIndex];

                // If we found an inactive object in the cache, use that.
                if (!obj.active)
                    break;

                // If not, increment index and make it loop around
                // if it exceeds the size of the cache
                cacheIndex = (cacheIndex + 1) % cacheSize;
            }

            // The object should be inactive. If it's not, log a warning and use
            // the object created the longest ago even though it's still active.
            if (obj.active)
            {
                Debug.LogWarning(
                    "Spawn of " + prefab.name +
                    " exceeds cache size of " + cacheSize +
                    "! Reusing already active object.", obj);
                MyCache.Destroy(obj);
            }

            // Increment index and make it loop around
            // if it exceeds the size of the cache
            cacheIndex = (cacheIndex + 1) % cacheSize;

            return obj;
        }
    }

    // end of ObjectCaches class


    // create object
    static public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        ObjectCache cache = null;

        // Find the cache for the specified prefab
        if (spawner)
        {
            for (int i = 0; i < spawner.caches.Length; i++)
            {
                if (spawner.caches[i].prefab == prefab)
                {
                    cache = spawner.caches[i];
                }
            }
        }

        // If there's no cache for this prefab type, just instantiate normally
        if (cache == null)
        {
            return Instantiate(prefab, position, rotation) as GameObject;
        }

        // Find the next object in the cache
        GameObject obj = cache.GetNextObjectInCache();

        // Set the position and rotation of the object
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // Set the object to be active
        obj.SetActive(true);
        spawner.activeCachedObjects[obj] = true;

        return obj;
    }

    static public void Destroy(GameObject objectToDestroy)
    {
        if (spawner && spawner.activeCachedObjects.ContainsKey(objectToDestroy))
        {
            objectToDestroy.SetActive(false);
            spawner.activeCachedObjects[objectToDestroy] = false;
        }
        else {
            objectToDestroy.SetActive(false);
        }
    }
    // Use this for initialization
    void Awake()
    {
        // Set the Singleton
        spawner = this;

        // Total number of cached objects
        int amount = 0;

        // Loop through the caches
        for (int i = 0; i < caches.Length; i++)
        {
            // Initialize each cache
            caches[i].Initialize();

            // Count
            amount += caches[i].cacheSize;
        }

        // Create a hashtable for active/nonactive objects
        activeCachedObjects = new Dictionary<GameObject, bool>();
    }

}