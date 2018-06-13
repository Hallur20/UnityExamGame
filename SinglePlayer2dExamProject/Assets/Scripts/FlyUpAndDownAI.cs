using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is for flying ai (flappy bird)
public class FlyUpAndDownAI : MonoBehaviour {
    public Slider healthBar;
    private bool invulnerable = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if bird hits robotboy
        if (collision.gameObject.name.Equals("CharacterRobotBoy"))
        {
            if (invulnerable != true) //if player is not invulnerable
            {
                if (healthBar.value != 0) //if player does not have zero hp.
                {
                    healthBar.value -= 0.5F; //make player lose 0.5 hp
                    StartCoroutine(playerIsInvulnerableForFewSeconds()); //then make player invulnerable for 1.5 seconds
                }
                else
                { //if player has no hp, he dies.
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private bool whichWay = true; //true = up, false = down

    public float secondsUntilTurn;
    IEnumerator playerIsInvulnerableForFewSeconds()
    {
        invulnerable = true; //true = invulnerable
        yield return new WaitForSeconds(1.5F); //wait 1.5 seconds
        invulnerable = false; //false =  vulnerable
    }

    // Update is called once per frame
    void Update () {
        if (whichWay == true) { //if bird is flying upwards
            gameObject.GetComponent<Transform>().Translate(new Vector2(0,0.03F)); //then transform.translate +0,03 in y-axis until given time.
            StartCoroutine(giveTime());
        }
        if (whichWay == false) //if bird is flying downwards
        {
            gameObject.GetComponent<Transform>().Translate(new Vector2(0, -0.03F)); //then transform.translate -0.03 in y-axis until given time.
            StartCoroutine(giveTime());
        }

    }
    IEnumerator giveTime() {
        if (whichWay == false)
        { //f bird is moving down, wait given time, then set whichway to true (so that next time he flies up)
            yield return new WaitForSeconds(secondsUntilTurn);
            whichWay = true; ;
        }
        if (whichWay == true)
        {  //f bird is moving up, wait given time, then set whichway to false (so that next time he flies down)
            yield return new WaitForSeconds(secondsUntilTurn);
            whichWay = false;
        }
    }
}
