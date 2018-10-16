using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSnail : MonoBehaviour {
    
	public float movementSpeed = 3f;
    public float rotationSpeed = 150f;

    // Update is called once per frame
    void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
