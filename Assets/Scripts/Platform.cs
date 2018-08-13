using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Takes care of an individual platform
 */
public class Platform : MonoBehaviour {

    public bool touchedByPlayer = false;
    public Game_Manager manager;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }

    public bool GetTouchedByPlayer()
    {
        return touchedByPlayer;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            manager.UpdateScore(gameObject);
            touchedByPlayer = true;
        }
    }
}
