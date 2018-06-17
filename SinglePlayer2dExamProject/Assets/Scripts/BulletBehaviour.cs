using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBehaviour : MonoBehaviour {
    public Vector2 velocity;
    public Vector3 startPosition;
    public Slider healthBar;
    private bool bossCooldown = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("AI") ) {
            Destroy(gameObject); //if a bullet hits an ai, the ai and the bullet is destroyed
            Destroy(collision.gameObject);
        }
        //if the robot-boy-bullet hits the boss, make the boss unable to take damage for 1.5seconds (this is so the bullet doesnt kill him instantly because of how oncollision works)
        //also make the boss lose 1 life and update the text that shows that.
        if (collision.gameObject.name.Equals("BOSS") && bossCooldown == false) {
            StartCoroutine(makeBossGetHitOnlyOnceByBullet());
            Destroy(gameObject);
            BossBehaviour.lifes -= 1;
            GameObject.Find("New Text").GetComponent<TextMesh>().text = "hits left: " + BossBehaviour.lifes;
        }
        if (collision.gameObject.name.Contains("Platform"))
        {
            Destroy(gameObject); //if bullet hits a platform, the bullet is destroyed
        }
        //if bullet hits a point then
        if (collision.gameObject.name.Contains("Pickup"))
        {
            StartCoroutine(GoThroughPickup(collision)); //make bullet go through point by removing collider, give velocity again, and after 0.5 seconds give the collider back.

        }
        //if bullet hits a gameobject containing the string stop
        if (collision.gameObject.name.Contains("Stop")) {
            gameObject.SetActive(false); //then make the bullet in-active, so that boss can reuse it.
            gameObject.name = "InActiveBullet"; //also rename bullet to enemybullet, because we need to know if the bullet was fired by robotboy or an enemy.
        }
        //if enemybullet hits robotboy
        if (gameObject.name.Contains("EnemyBullet") && collision.gameObject.name.Contains("CharacterRobotBoy")) {
            if (GameObject.Find("HealthBar").GetComponent<Slider>().value != 0) { //if robotboy does not have zero hp.
                GameObject.Find("HealthBar").GetComponent<Slider>().value -= 0.5F; //make robotboy lose 0.5hp.
                //gameObject.name = "InActiveBullet";
                gameObject.SetActive(false); //then make bullet in-active, so that boss can reuse it.
            } else
            {
                Destroy(collision.gameObject); //if robotboy was hit by bullet and has no hp, he dies.
            }
            
        }
    }
  
    //this method makes boss invulnerable for 1 seconds, so that no bugs appear when he gets shot (for example he gets shot 3 times by 1 bullet)
    IEnumerator makeBossGetHitOnlyOnceByBullet() {
        bossCooldown = true; //when bosscooldown is true, he cannot be shot.
        yield return new WaitForSeconds(1F); //then after 1 second.
        bossCooldown = false; //set bosscooldown to false, so that he can get shot again.
    }

    // Use this for initialization
    void Start () {
        startPosition = gameObject.transform.position; //save the start position of the bullet, so that we can use the GoThroughPickup method to find out if the bullet has been moved to the left or right
        StartCoroutine(DestroyBulletAfter2Seconds()); //after 2 seconds the bullet is destroyed if it was fired from canon or robotboy.
	}

    IEnumerator DestroyBulletAfter2Seconds() {
        if ( gameObject.name.Equals("EnemyBulletCanon") || gameObject.name.Equals("Bullet(Clone)")) {//if the bullet was fired from canon or robotboy.
            yield return new WaitForSeconds(2);
            Destroy(gameObject); //then after 2 seconds destroy the bullet.
        }
    }
    IEnumerator GoThroughPickup(Collision2D collision) {
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false; //the bullet loses it's collider so it can go through the point.
        //if the bullet has gone to the right (x.position is higher than start.x position)
        if (gameObject.transform.position.x > startPosition.x) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + 2, velocity.y); //then continue the bullet with the same speed to the right
        }
        //if the bullet has gone to the left (x.position is lower than start.x position)
        if (gameObject.transform.position.x < startPosition.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x - 18, velocity.y); //then continue the bullet with the same speed to the left
        }
        yield return new WaitForSeconds(0.5F); //then wait for half a second.
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true; //then give the collider back.
    }
}
