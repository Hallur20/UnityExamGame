using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBehaviour : MonoBehaviour {
    public Vector2 velocity;
    public Vector3 startPosition;
    public Slider healthBar;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("AI") ) {
            Destroy(gameObject); //if a bullet hits an ai, the ai and the bullet is destroyed
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.name.Equals("BOSS") && bossCooldown == false) {
            Debug.Log("are we printing 10 times?");
            StartCoroutine(makeBossGetHitOnlyOnceByBullet());
            Destroy(gameObject);
            BossBehaviour.lifes -= 1;
            GameObject.Find("New Text").GetComponent<TextMesh>().text = "hits left: " + BossBehaviour.lifes;
        }
        if (collision.gameObject.name.Contains("Platform"))
        {
            Destroy(gameObject); //if bullet hits a platform, the bullet is destroyed
        }
        if (collision.gameObject.name.Contains("Pickup"))
        {
            StartCoroutine(GoThroughPickup(collision));

        }
        if (collision.gameObject.name.Contains("Stop")) {
            gameObject.SetActive(false);
            gameObject.name = "EnemyBullet";
        }

        if (gameObject.name.Contains("EnemyBullet") && collision.gameObject.name.Contains("CharacterRobotBoy")) {
            if (GameObject.Find("HealthBar").GetComponent<Slider>().value != 0) {
                GameObject.Find("HealthBar").GetComponent<Slider>().value -= 0.5F;
                gameObject.SetActive(false);
            } else
            {
                Destroy(collision.gameObject);
            }
            
        }
    }
    private bool bossCooldown = false;
    IEnumerator makeBossGetHitOnlyOnceByBullet() {
        bossCooldown = true;
        yield return new WaitForSeconds(1F);
        bossCooldown = false;
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
        if (gameObject.name.Contains("CharacterRobotBoy") || gameObject.name.Contains("canon")) {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        } else
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
            gameObject.name = "InActiveBullet";
        }
        
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
