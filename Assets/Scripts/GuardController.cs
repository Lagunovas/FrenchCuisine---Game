using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

	[SerializeField] private Transform target;
	private NavMeshAgent agent;
	private Transform raycastOrigin;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		raycastOrigin = transform.Find("RaycastPosition");
	}

	private void Update() {
		if (target) {
			agent.SetDestination(target.position);
		}
	}

	private void OnTriggerEnter(Collider other) {
		RaycastHit raycastHit;
		var raycastPosition = raycastOrigin.position;
		var direction = other.transform.position - raycastPosition;

		if (Physics.Raycast(raycastPosition, direction, out raycastHit)) { // Need to cast multiple rays incase object (collider) center is obstructed by other objects, etc.. (This will do for now)
			Debug.DrawRay(raycastPosition, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);

			switch (raycastHit.collider.tag) {
				case "Trail": // Decrease the size of the trail collider?
				case "Player": // Player - (snail "model" parts get detected seperately, create a collider which covers player?)
					Debug.Log("Guard sees: " + raycastHit.collider);
					break;
				default:
					break;
			}
		}

	}

}