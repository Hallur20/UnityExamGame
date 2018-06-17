using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerClimb : MonoBehaviour {
    public float speed = 5;
 
    // Use this for initialization
 
	
	// Update is called once per frame
	

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (gameObject.name.Contains("Ladder")) {

            if (collision.tag == "Player" && Input.GetKey(KeyCode.W))
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

            } else if (collision.tag == "Player" && Input.GetKey(KeyCode.S)) {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
        } 

        if (gameObject.name.Contains("Waterfall")) {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
    }
}
