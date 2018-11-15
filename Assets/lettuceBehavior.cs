using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lettuceBehavior : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this);

        }
    }
}
