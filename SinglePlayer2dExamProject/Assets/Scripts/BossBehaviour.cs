using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//this script is for boss only.
public class BossBehaviour : MonoBehaviour {

    bool whichWay = true; //true when boss moves to the right, false when moves to the left.
    public GameObject point;
    public float speedMove;
    public GameObject bullet;
    private bool canShoot = true; //is true when boss can shoot, false when he cannot.
    public Vector2 velocity;
    public Vector2 offset = new Vector2(-0.4F, -0.1F); //bullet spawn-start-position.
    GameObject saveGo;
    public Slider healthBar; //healthbar on the ui (belonging to robotcharacterboy)
    private bool invulnerable = false; //false when characterrobotboy can take damage, true we he can.
    public static int lifes;
    public GameObject lifesText;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if boss touches gameobject named stopright, set whichway to false, also rotate him 180 degrees (to the left)
        if (collision.gameObject.name.Contains("StopRight") && whichWay == true)
        {
            offset = new Vector2(-0.05F, 0.05F);
            Debug.Log("going left");
            gameObject.transform.Rotate(new Vector3(0, 180, 0));
            GameObject.Find("New Text").transform.Rotate(new Vector3(0,-180,0)); //revert text (how many hits are left to kill boss) again, because then the player can see what is being said.
            whichWay = false;
        }
        //if boss touches gameobject named stopleft, set whichway to true, also rotate him -180 degrees (to the right)
        if (collision.gameObject.name.Contains("StopLeft") && whichWay == false)
        {
            offset = new Vector2(0.10F, 0.05F);
            Debug.Log("going right");
            gameObject.transform.Rotate(new Vector3(0, -180, 0));
            GameObject.Find("New Text").transform.Rotate(new Vector3(0, 180, 0)); //revert text (how many hits are left to kill boss) again, because then the player can see what is being said.
            whichWay = true;

        }
        //if robotboy has been touched by the boss, make him lose 0.5 hp, if his hp is zero destroy him (you die).
        if (collision.gameObject.name.Equals("CharacterRobotBoy"))
        {
            if (invulnerable != true)
            {
                if (healthBar.value != 0)
                {
                    healthBar.value -= 0.5F;
                    StartCoroutine(playerIsInvulnerableForFewSeconds()); //robotboy is invulnerable for 1.5 seconds, then he can get hit again.
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
        invulnerable = true; //set invulnerable to true, making robotboy unable to take damage.
        yield return new WaitForSeconds(1.5F); //wait 1.5 seconds.
        invulnerable = false; //then set invulnerable to true, now robotboy can take damage again.
    }

    // Use this for initialization
    void Start () {
        lifes = 8;
        offset = new Vector2(0.10F, 0.05F); //the start-position for boss-bullet.
        StartCoroutine(whenToJump()); //make boss jump every 2 seconds.
    }
 
	// Update is called once per frame
	void Update () {
        if (whichWay == true)
        {
            gameObject.transform.Translate(new Vector3(speedMove, 0, 0)); //if whichway is true walk to the right. (speedmove = how fast he walks)
        }
        if (lifes == 0) {
            Destroy(gameObject); //if boss has lost every life (starts with 8), then destroy him (he dies).
        }
        if (whichWay == false)
        {

            gameObject.transform.Translate(new Vector3(speedMove, 0, 0)); //if whichway is false walk to the left. (speedmove = how fast he walks)
        }
        if (canShoot && whichWay == false) //if boss can shoot and is moving to the left.
        {
            if (saveGo == null) //if the bullet doesnt exist in the hierachy
            {
                GameObject go = (GameObject)Instantiate(bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity); //instantiate bullet and make it go forward.
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x - 40, velocity.y); //bullet goes to the left with -40 speed.
                saveGo = go; //save bullet for later use (saveGo is not null anymore)
                saveGo.name = "EnemyBullet";
                StartCoroutine(ShootingCooldown()); //boss can shoot again after 1.5 seconds.
            }
            else
            {

                saveGo.SetActive(true); //use the same bullet by setting it active again.
                saveGo.transform.position = (gameObject.transform.position); //give it a new start-position, the same position as boss
                saveGo.transform.position += new Vector3(-6.5F, 3.5F, 0); //and then give the position an offset (so the bullet doesnt spawn inside the boss)
                saveGo.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x - 40, velocity.y); //shoot bullet to the left with -40 speed.
                saveGo.name = "EnemyBullet";
                StartCoroutine(ShootingCooldown()); //boss can shoot again after 1.5 seconds.
            }
        }
        if (canShoot && whichWay == true) //if boss can shoot and is moving to the right.
        {
            if (saveGo == null) //if the bullet doesnt exist in the hierachy
            {  
            GameObject go = (GameObject)Instantiate(bullet, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity); //instantiate bullet and make it go forward.
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + 40, velocity.y); //bullet goes to the right with 40 speed.
                saveGo = go; //save bullet for later use (saveGo is not null anymore)
                saveGo.name = "EnemyBullet";
                StartCoroutine(ShootingCooldown()); //boss can shoot again after 1.5 seconds.
            } else
            {

                saveGo.SetActive(true); //use the same bullet by setting it active again.
                saveGo.transform.position =(gameObject.transform.position); //give it a new start-position, the same position as boss
                saveGo.transform.position += new Vector3(6.5F, 3.5F, 0); //and then give the position an offset (so the bullet doesnt spawn inside the boss)
                saveGo.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x+40, velocity.y); //shoot bullet to the right with 40 speed.
                saveGo.name = "EnemyBullet";
                StartCoroutine(ShootingCooldown()); //boss can shoot again after 1.5 seconds.
            }
        }
        

            
    }

    IEnumerator whenToJump() {
        while (true) { //endless loop, because boss jumps endlessly until he dies.
            yield return new WaitForSeconds(2F); //wait 2 seconds before jumping.
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 18), ForceMode2D.Impulse); //then jump with the speed of 18 in the y-axis (in the air).
            yield return new WaitForSeconds(0.6F); //then after 0.6 seconds
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20), ForceMode2D.Impulse); //make boss jump down with the speed for -20 in the y-axis (from the air)
        }
       
        
    }
    IEnumerator ShootingCooldown()
    {
        canShoot = false; //boss cannot shoow, he has to wait (canshoot = false)
        yield return new WaitForSeconds(1.5F); //he has to wait 1.5 seconds.
        canShoot = true; //then set to true, so boss can shoot again.
    }

    //when boss dies.
    private void OnDestroy()
    {
        GameObject.Find("StopRightBoss").SetActive(false); //remove the wall to the left, so that robotboy can access the last point and win the game.
    }
}
