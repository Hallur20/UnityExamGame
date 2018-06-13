using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used for the canon
public class AutoShootScript : MonoBehaviour {
    private bool canShoot = true; //is true when the canon can shoot, false when he cannot shoot.
    public GameObject projectile;
    public Vector2 offset = new Vector2(-0.4F, -0.1F); //this is for where the bullet will spawn, offset gives a start-position that makes sense (the canon-hole).
    public Vector2 velocity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canShoot) //while we can shoot ...
        {
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.name = "EnemyBulletCanon";
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x, velocity.y); //make the bullet shoot to the left.
            StartCoroutine(ShootingCooldown());
        }
    }

    IEnumerator ShootingCooldown()
    {
        canShoot = false; //set canshoot to false so canon has to wait to shoot again.
        yield return new WaitForSeconds(1.5F); //wait 1.5 seconds.
        canShoot = true; //set canshoot to true so canon can shoot again.
    }
}
