using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJump : MonoBehaviour {

    public AudioSource bounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "CharacterRobotBoy") {
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 38), ForceMode2D.Impulse);
            bounce.Play();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
