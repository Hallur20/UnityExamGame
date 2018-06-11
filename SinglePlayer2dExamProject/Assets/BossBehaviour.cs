using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour {

    bool whichWay = true;
    public GameObject point;
    public float speedMove;
    public GameObject bullet;
    private bool canShoot = true;
    public Vector2 velocity;
    public Vector2 offset = new Vector2(-0.4F, -0.1F);
    GameObject saveGo;
    public Slider healthBar;
    private bool invulnerable = false;
    public static int lifes;
    public GameObject lifesText;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("StopRight") && whichWay == true)
        {
            offset = new Vector2(-0.05F, 0.05F);
            Debug.Log("going left");
            gameObject.transform.Rotate(new Vector3(0, 180, 0));
            whichWay = false;

        }
        if (collision.gameObject.name.Equals("StopLeft") && whichWay == false)
        {
            offset = new Vector2(0.10F, 0.05F);
            Debug.Log("going right");
            gameObject.transform.Rotate(new Vector3(0, -180, 0));
            whichWay = true;

        }
        if (collision.gameObject.name.Equals("CharacterRobotBoy"))
        {
            if (invulnerable != true)
            {
                if (healthBar.value != 0)
                {
                    healthBar.value -= 0.5F;
                    StartCoroutine(playerIsInvulnerableForFewSeconds());
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    IEnumerator playerIsInvulnerableForFewSeconds()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1.5F);
        invulnerable = false;
    }

    // Use this for initialization
    void Start () {
        lifes = 8;
        offset = new Vector2(0.10F, 0.05F);
        StartCoroutine(whenToJump());
    }
	
	// Update is called once per frame
	void Update () {
        if (whichWay == true)
        {
            gameObject.transform.Translate(new Vector3(speedMove, 0, 0));
        }
        if (lifes == 0) {
            Destroy(gameObject);
        }
        if (whichWay == false)
        {

            gameObject.transform.Translate(new Vector3(speedMove, 0, 0));
        }
        if (canShoot && whichWay == false)
        {
            if (saveGo == null)
            {
                GameObject go = (GameObject)Instantiate(bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x - 40, velocity.y);
                saveGo = go;
                StartCoroutine(ShootingCooldown());
            }
            else
            {

                saveGo.SetActive(true);
                saveGo.transform.position = (gameObject.transform.position);
                saveGo.transform.position += new Vector3(-6.5F, 3.5F, 0);
                saveGo.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x - 40, velocity.y);
                StartCoroutine(ShootingCooldown());
            }
        }
        if (canShoot && whichWay == true)
        {
            if (saveGo == null) { 
            GameObject go = (GameObject)Instantiate(bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + 40, velocity.y);
            saveGo = go;
            StartCoroutine(ShootingCooldown());
            } else
            {

                saveGo.SetActive(true);
                saveGo.transform.position =(gameObject.transform.position);
                saveGo.transform.position += new Vector3(6.5F, 3.5F, 0);
                saveGo.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + 40, velocity.y);
                StartCoroutine(ShootingCooldown());
            }
        }
        


    }

    IEnumerator whenToJump() {
        while (true) {
            yield return new WaitForSeconds(2F);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 18), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.6F);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20), ForceMode2D.Impulse);
        }
       
        
    }
    IEnumerator ShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.5F);
        canShoot = true;
    }

    private void OnDestroy()
    {
        point.SetActive(true);
    }
}
