using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Takes care of the Lava flow
 */
public class Lava : MonoBehaviour {

    public float lavaSpeed = 2F;
    public Rigidbody2D rigidbody2D;
    public PlatformGenerator platformGenerator;
    public bool allowLavaFlow = false;

    void Start()
    {
        // When game starts have the lava start rising
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(!allowLavaFlow)
        {
            return;
        }

        var rbVelocity = rigidbody2D.velocity;

        // Takes care of horizontal player movement
        rbVelocity.y = 1 * lavaSpeed;

        rigidbody2D.velocity = rbVelocity;
    }

    public void StartLavaFlow()
    {
        allowLavaFlow = true;
        InvokeRepeating("AccelerateLavaRising", 0f, 0.5f);
    }

    public void StopLavaFlow()
    {
        allowLavaFlow = false;
        CancelInvoke("AccelerateLavaRising");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            Destroy(collision.gameObject);
            platformGenerator.numOfBlocksPresent--;
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().Death();
        }
    }

    void AccelerateLavaRising()
    {
        lavaSpeed += 0.1F;
    }
}
