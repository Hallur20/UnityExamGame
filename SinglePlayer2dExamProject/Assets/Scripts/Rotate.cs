using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        //this is for pick up points, for a special effect making it rotate for every frame.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
