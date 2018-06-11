using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyUpAndDownAI : MonoBehaviour {
    public Slider healthBar;
    private bool invulnerable = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
    private bool whichWay = true; //true = up, false = down

    public float secondsUntilTurn;
    IEnumerator playerIsInvulnerableForFewSeconds()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1.5F);
        invulnerable = false;
    }

    // Use this for initialization
    void Start () {

    }


    // Update is called once per frame
    void Update () {
        if (whichWay == true) {
            gameObject.GetComponent<Transform>().Translate(new Vector2(0,0.03F));
            StartCoroutine(giveTime());
        }
        if (whichWay == false)
        {
            gameObject.GetComponent<Transform>().Translate(new Vector2(0, -0.03F));
            StartCoroutine(giveTime());
        }

    }
    IEnumerator giveTime() {
        if (whichWay == false)
        {
            yield return new WaitForSeconds(secondsUntilTurn);
            whichWay = true; ;
        }
        if (whichWay == true)
        {  
            yield return new WaitForSeconds(secondsUntilTurn);
            whichWay = false;
        }
    }
}
