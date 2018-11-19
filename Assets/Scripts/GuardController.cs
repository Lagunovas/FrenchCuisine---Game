using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class GuardController : MonoBehaviour {
    public GameObject gameoverUL;

    [SerializeField] private Transform target;
	private int targetIndex;
	[SerializeField] private bool moveMode; // false = 0 - 1 - 2 - 3 [->] 0, true = 0 - 1 - 2 - 3 (->) 3 - 2 - 1 - 0
	private bool direction; // necessary for moveMode = true

	private bool playerDetected; // Snail box trigger collider must be bigger than the cylinder collider.

	private NavMeshAgent agent;
	private Transform raycastOrigin;
    
    bool gameisover;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		raycastOrigin = transform.Find("RaycastPosition");

		if (!target) {
			Debug.LogError("No target has been set for: " + this);
		}
	}

	private void Update() {
		if (playerDetected) {
            Time.timeScale = 0;
            if (gameisover) {
                if (Input.GetKeyDown(KeyCode.R))
                {

                    SceneManager.LoadScene(2);
                    Time.timeScale = 1;
                }


            }
            onGameOver(gameoverUL);
           
			Debug.Log("Player detected!");
		} else {
			if (target) {
				agent.SetDestination(target.GetChild(targetIndex).position);
				if (!agent.hasPath && !agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
					if (targetIndex == target.childCount - 1) {
						direction = !direction;
					}

					if (direction) {
						if (moveMode) {
							if (targetIndex > 0) {
								targetIndex--;
							} else {
								direction = !direction;
							}
						} else {
							targetIndex = 0;
							direction = !direction;
						}
					} else {
						targetIndex++;
					}
				}

			}
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
					//Debug.Log("Guard sees: " + raycastHit.collider);
					if (!playerDetected) {
						playerDetected = true;
					}
					break;
				default:
					break;
			}
		}

	}

	private void OnTriggerExit(Collider other) {
		switch (other.tag) {
			case "Player":
				if (playerDetected) {
					playerDetected = false;
				}
				break;
			default:
				break;
		}
	}
    void onGameOver(GameObject GameOver) {
        if(GameOver.gameObject.name=="GameOver")
           GameOver.SetActive(true);
        gameisover = true;
        

    }

}