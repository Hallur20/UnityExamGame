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
    private bool isDead = false;
    private bool isClimbing;
    public float distance;
    public LayerMask whatIsLadder;


    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4F, 0.1F);
    public float cooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Lava") || collision.gameObject.name.Equals("Ice")) {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Pick Up"))
        {
            collision.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
   pickupSound.Play();
}
        if (collision.gameObject.name.Equals("Ladder"))
        {
            Debug.Log("qwekpoqwjoip");
        }
    }



    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        Debug.Log(lifes);
        backgroundMusic.Play();
        winText.text = "";
        count = 0;
        SetLifesText();
        SetCountText();

    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 2) {
            winText.text = "You win!";
            Time.timeScale = 0;
            GameObject.Find("AI").SetActive(false);
            youWonButton.gameObject.SetActive(true);
        }
    }

    private void SetLifesText() {
        lifesText.text = "Lifes: " + lifes.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canShoot) {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
            StartCoroutine(ShootingCooldown());
        }
    }
    void FixedUpdate() {
        RaycastHit2D hitInto = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if (hitInto.collider != null) {
            if (Input.GetKeyDown(KeyCode.W)) {
                isClimbing = true;
            }
        }
        if (isClimbing == true) {
            Debug.Log("climb test");
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
        isDead = true;
        deadSound.Play();
        panel.SetActive(true);
        backgroundMusic.Stop();
        
        lifes--;
        if (lifes > -1) {
            winText.text = "you died!";
            tryAgainButton.gameObject.SetActive(true);
        } else
        {
            winText.text = "game over!";
        }
        isDead = false;
    }
   
}
