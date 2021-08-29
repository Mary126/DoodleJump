using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public PlatformGenerator generator;
    public int index = 0;

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.collider == true)
        {
            generator.PlatformsMove();
        }
    }
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
