using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class ControlPlayer : MonoBehaviour {

    public GameObject projectile;
    public Vector2 velocity;
    public Text countText;
    public Text winText;
    public Button tryAgainButton;
    public Button youWonButton;
    public Text lifesText;
    public GameObject panel;
    private int count;
    public AudioSource pickupSound;
    public AudioSource backgroundMusic;
    public AudioSource deadSound;
    static int lifes = 3;
    private bool isClimbing;
    public float distance;
    public LayerMask whatIsLadder;


    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4F, 0.1F);
    public float cooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player touches lava or ice, then destroy player (he dies)
        if (collision.gameObject.name.Contains("Lava") || collision.gameObject.name.Equals("Ice")) {
            Destroy(gameObject);
        }
        //if player picks up a point, set the counter to +1, update the ui text and play a point sound.
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            collision.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
   pickupSound.Play();
}
    }

    int pointCount; //counter for amount of points on the map.

    // Use this for initialization
    void Start () {
        pointCount = GameObject.FindGameObjectsWithTag("Pick Up").Length; //set the length of the gameobjects with tagname pick up to pointcount.
        Time.timeScale = 1; //unpause game, because at the end of a stage the game is paused.
        backgroundMusic.Play();
        winText.text = ""; //reset the ui wintext.
        count = 0; //reset count
        SetLifesText();
        SetCountText();

    }

    //this method is for when a point is picked up and then we find out if all points are picked up.
    // if all are picked up and whole is complete, then pause game and show game complete.
    // else if all are picked up, but there are more stages, pause game and show a button to go to next level with.
    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= pointCount) {
            if (SceneManager.GetActiveScene().name.Equals("_scene3")) {
                winText.text = "Game Complete!";
                Time.timeScale = 0;
            } else
            {
                winText.text = "You win!";
                Time.timeScale = 0;
                GameObject.Find("AI").SetActive(false);
                youWonButton.gameObject.SetActive(true);
            }

        }
    }
    //set ui text to amonut of lifes, this is for when a player loses a life then we need to update that in the ui.
    private void SetLifesText() {
        lifesText.text = "Lifes: " + lifes.ToString();
    }

    // Update is called once per frame
    void Update() {
        //if player pushes f and canshoot is true, then play a shooting sound and spawn a bullet that has a velocity, also make bullet have a cooldown.
        if (Input.GetKeyDown(KeyCode.F) && canShoot) {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
            StartCoroutine(ShootingCooldown());
        }
    }
    void FixedUpdate() {
        /*RaycastHit2D hitInto = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if (hitInto.collider != null) {
            if (Input.GetKeyDown(KeyCode.W)) {
                isClimbing = true;
            }
        }*/
        //maybe delete this later, we dont think we need this in order to climb.
    }

    IEnumerator ShootingCooldown()
    {//set canshoot to false so player cant spam bullets
        canShoot = false;
        yield return new WaitForSeconds(1.5F); //wait 1.5 seconds
        canShoot = true; //then allow player to shoot again.
    }

    
    private void OnDestroy()
    {
        deadSound.Play(); //play die sound
        panel.SetActive(true); //show panel
        backgroundMusic.Stop(); //stop background music
        
        lifes--; //player loses 1 life
        if (lifes > -1) { //if player has more lifes
            winText.text = "you died!"; //ui text you died
            tryAgainButton.gameObject.SetActive(true); //make player able to try again with button
        } else
        {
            winText.text = "game over!"; //if player has no lifes left only show game over
        }
    }
}
