using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

    [SerializeField] private GameObject gameOver;
	private bool isGameOver;

	private Transform pathTarget;
	private int targetIndex;

	[SerializeField] private bool moveMode; // false = 0 - 1 - 2 - 3 [->] 0, true = 0 - 1 - 2 - 3 (->) 3 - 2 - 1 - 0
	private bool direction; // necessary for moveMode = true

	private bool playerDetected; // Snail box trigger collider must be bigger than the cylinder collider.
	private bool followingTrail;
	private uint followedTrailId;
	private Transform followee;

	private NavMeshAgent agent;
	private Transform raycastOrigin;

	private int playerRaycastLayerMask;
	private int trailRaycastLayerMask;
    
	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		raycastOrigin = transform.Find("RaycastPosition");

		int environmentLayerMask = 1 << LayerMask.NameToLayer("Environment");
		playerRaycastLayerMask = (1 << LayerMask.NameToLayer("Player")) | environmentLayerMask;
		trailRaycastLayerMask = (1 << LayerMask.NameToLayer("Trail")) | environmentLayerMask;

		var environment = GameObject.Find("Environment");

		if (environment) {
			pathTarget = environment.transform.Find(name + "-Target");
		}

		if (!pathTarget) {
			Debug.LogError("No path target has been set for: " + this);
		}
	}

	// NOT TESTED - will test later (lazy me)
	private int getClosestPathIndex() {
		var navMeshPath = new NavMeshPath();
		var childCount = pathTarget.childCount;
		float[] pathLengths = new float[childCount];

		for (int i = 0; i < childCount; i++) {
			pathLengths[i] = -1;

			var exist = NavMesh.CalculatePath(transform.position, pathTarget.GetChild(i).position, NavMesh.AllAreas, navMeshPath);

			if (exist) {
				float pathLength = 0;

				var currentPosition = transform.position;
				foreach (var corner in navMeshPath.corners) {
					pathLength += Vector3.Distance(currentPosition, corner);
					currentPosition = corner;
				}

				pathLengths[i] = pathLength;
			}
		}

		int returnIndex = -1;
		float leastDistance = float.MaxValue;

		for (int i = 0; i < pathLengths.Length; i++) {
			float pathLength = pathLengths[i];

			if (pathLength > -1 && pathLength <= leastDistance) {
				leastDistance = pathLength;
				returnIndex = i;
			}
		}

		return returnIndex;
	}

	private void Update() {
		if (playerDetected) {
			/*Time.timeScale = 0;
            if (isGameOver) {
                if (Input.GetKeyDown(KeyCode.R)) {
                    SceneManager.LoadScene(2);
                    Time.timeScale = 1;
                }
            }
            OnGameOver(gameoverUL);*/

			if (IsTargetVisible(followee, trailRaycastLayerMask) && followee) {
				agent.SetDestination(followee.position);
			} else {
				playerDetected = false;
			}
		} else if (followingTrail) {
			if (followee) {
				agent.SetDestination(followee.position);
			} else {
				followingTrail = false;
				followedTrailId = 0;
			}
		} else {
			if (pathTarget) {
				agent.SetDestination(pathTarget.GetChild(targetIndex).position);
				if (!agent.hasPath && !agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
					if (targetIndex == pathTarget.childCount - 1) {
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

	private Transform GetRaycastHit(Transform targetTransform, int layerMask) {
		if (targetTransform) {
			RaycastHit raycastHit;
			var raycastPosition = raycastOrigin.position;
			var direction = targetTransform.position - raycastPosition;

			if (Physics.Raycast(raycastPosition, direction, out raycastHit, Mathf.Infinity, layerMask)) {
				return raycastHit.collider.transform;
			}
		}
		return null;
	}

	private bool IsTargetVisible(Transform targetTransform, int layerMask) {
		if (targetTransform) {
			var hitTransform = GetRaycastHit(targetTransform, layerMask);
			if (hitTransform) {
				return hitTransform.CompareTag(targetTransform.tag);
			}
		}
		return false;
	}

	private void OnTriggerStay(Collider other) {
		if (!playerDetected) {
			if (other.CompareTag("Trail")) {
				uint trailId = other.GetComponent<TrailBehaviour>().id;

				if (trailId > followedTrailId && IsTargetVisible(other.transform, trailRaycastLayerMask)) {
					followingTrail = true;
					followedTrailId = trailId;
					followee = other.transform;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (!playerDetected) {
			if (other.CompareTag("Player")) {
				if (playerDetected = IsTargetVisible(other.transform, playerRaycastLayerMask)) {
					followee = other.transform;
					followingTrail = false;
					followedTrailId = 0;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (playerDetected) {
			if (other.CompareTag("Player")) {
				bool isTargetSeen = IsTargetVisible(followee, trailRaycastLayerMask);
				if (!isTargetSeen) {
					playerDetected = false;
					followee = null;
				}
			}
		}
	}

	private void OnGameOver() {
		if (gameOver) {
			gameOver.SetActive(true);
			isGameOver = true;
		}
    }

}