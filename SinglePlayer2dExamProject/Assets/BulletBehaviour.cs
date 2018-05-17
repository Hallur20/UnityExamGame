using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public Vector2 velocity;
    public Vector3 startPosition;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("AI")) {
            Destroy(collision.gameObject);
            Destroy(gameObject); //if a bullet hits an ai, the ai and the bullet is destroyed
            
        }
        if (collision.gameObject.name.Contains("Platform"))
        {
            Destroy(gameObject); //if bullet hits a platform, the bullet is destroyed
        }
        if (collision.gameObject.name.Contains("Pickup"))
        {
            StartCoroutine(GoThroughPickup(collision));

        }

    }

    // Use this for initialization
    void Start () {
        startPosition = gameObject.transform.position;
        StartCoroutine(DestroyBulletAfter2Seconds());
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.transform.eulerAngles.z);
	}

    IEnumerator DestroyBulletAfter2Seconds() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    IEnumerator GoThroughPickup(Collision2D collision) {
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (gameObject.transform.position.x > startPosition.x) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + 2, velocity.y);
        }
        if (gameObject.transform.position.x < startPosition.x)
        {
            Debug.Log("bullet go left?");
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x - 18, velocity.y);
        }
        yield return new WaitForSeconds(0.5F);
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
