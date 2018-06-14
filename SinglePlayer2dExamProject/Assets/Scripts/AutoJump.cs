using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for the trampoline.
public class AutoJump : MonoBehaviour {

    public AudioSource bounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if trampoline touches characterrobotboy, then make him auto-jump high in the air
        if (collision.gameObject.name == "CharacterRobotBoy") {
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 38), ForceMode2D.Impulse);
            bounce.Play();
        }
    }
}
