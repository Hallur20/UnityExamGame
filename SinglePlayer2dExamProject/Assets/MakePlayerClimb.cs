using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerClimb : MonoBehaviour {
    public float speed = 5;
    private Animator m_Anim;
    // Use this for initialization
    void Start () {
        m_Anim = GameObject.Find("CharacterRobotBoy").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

        } else if (collision.tag == "Player" && Input.GetKey(KeyCode.S)) {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
    }
}
