using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailBehavior : MonoBehaviour {

    public float shrinkCoef = 0.45f;
    public float sizeToDecress;

    private void Start()
    {
        sizeToDecress = transform.localScale.x * shrinkCoef;
    }


    void Update () {
        float p = sizeToDecress * Time.deltaTime;
        transform.localScale = transform.localScale - new Vector3(p, p, p);
        if (transform.localScale.x < 0.01)
        {
            Destroy(gameObject);
        }
	}
}
