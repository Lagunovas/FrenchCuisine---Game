using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSystem : MonoBehaviour {

    [SerializeField] private Transform generator;
    [SerializeField] private float delay;
    [SerializeField] private GameObject trailPrefab;

    private float timeSinceLastTrail = 0;

    private void Update()
    {
        timeSinceLastTrail += Time.deltaTime;
        if (delay <= timeSinceLastTrail)
        {
            timeSinceLastTrail = 0;
            Instantiate(trailPrefab, generator.position, generator.rotation);
        }
    }


}
