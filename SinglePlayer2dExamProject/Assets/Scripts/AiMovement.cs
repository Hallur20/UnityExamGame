using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiMovement : MonoBehaviour {

    bool whichWay = true; //is true when the skeleton ai moves to the right, and false when it moves to the left
    public float speed;
    private Animator animator;
    public Slider healthBar;
    private bool invulnerable = false; //is false when characterrobotbuy has not been hit, is true when he is hit

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when the ai (this could be the skeletonman or the boss) touches a gameobject with the name StopRight,
        //set whichway to be false and turn the ai 180 degrees (to the left)
        if (collision.gameObject.name.Equals("StopRight") && whichWay == true) {
            Debug.Log("going left");
            gameObject.transform.Rotate(new Vector3(0, 180, 0));
            whichWay = false;

        }
        //when the ai (this could be the skeletonman or the boss) touches a gameobject with the name StopRight,
        //set whichway to be true and turn the ai -180 degrees (to the right)
        if (collision.gameObject.name.Equals("StopLeft") && whichWay == false)
        {
            Debug.Log("going right");
            gameObject.transform.Rotate(new Vector3(0, -180, 0));
            whichWay = true;

        }
        //if characterrobotboy touches the ai, make him lose 0.5 hp and also make him invulnerable for 1.5 seconds,
        //if characterrobotboy has zero health destroy him (he dies)
        if (collision.gameObject.name.Equals("CharacterRobotBoy")) {
            if (invulnerable != true) {
                if (healthBar.value != 0) {
                    healthBar.value -= 0.5F;
                    StartCoroutine(playerIsInvulnerableForFewSeconds());
                } else
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    //set invulnerable to true, then after 1.5 seconds set it to false (true = characterrobotboy can't take damage, false = he can take dmg)
    IEnumerator playerIsInvulnerableForFewSeconds() {
        invulnerable = true;
        yield return new WaitForSeconds(1.5F);
        invulnerable = false;
    }

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update() {
        //use skill1 every frame (only works for the skeletonman, because only he has that animation)
        animator.SetTrigger("skill_1");
        if (whichWay == true) {      
            gameObject.transform.Translate(new Vector3(0.04F, 0, 0)); //if whichway is true, walk to the right
        }
        if (whichWay == false) {

            gameObject.transform.Translate(new Vector3(0.04F, 0, 0));//if whichway is false, walk to the left
        }
            


    }
}
