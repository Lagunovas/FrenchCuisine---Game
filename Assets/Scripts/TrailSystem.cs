using UnityEngine;

public class TrailSystem : MonoBehaviour {

    [SerializeField] private Transform generator;
    [SerializeField] private float delay;
    [SerializeField] private GameObject trailPrefab;

	private float timeSinceLastTrail;
	private uint trailIdCounter;

	private void Start() {
		trailIdCounter = 1;
	}

	private void Update() {
        timeSinceLastTrail += Time.deltaTime;
        if (delay <= timeSinceLastTrail) {
            timeSinceLastTrail = 0;
			Instantiate(trailPrefab, generator.position, generator.rotation).GetComponent<TrailBehaviour>().id = trailIdCounter++;
		}
    }

}