using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShootScript : MonoBehaviour {
    private bool canShoot = true;
    public GameObject projectile;
    public Vector2 offset = new Vector2(-0.4F, -0.1F);
    public Vector2 velocity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canShoot)
        {
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.name = "EnemyBullet";
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x, velocity.y);
            StartCoroutine(ShootingCooldown());
        }
    }

    IEnumerator ShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.5F);
        canShoot = true;
    }
}
