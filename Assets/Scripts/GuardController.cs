using UnityEngine;

public class GuardController : MonoBehaviour {

	private float timer;
	private Rigidbody rb;
	private float startingRotation;

	void Start() {
		rb = GetComponent<Rigidbody>();
		startingRotation = rb.rotation.eulerAngles.y;
	}

	private void FixedUpdate() {
		timer += Time.fixedDeltaTime;

		if (timer >= 5f) {
			var targetRotation = (startingRotation + 90f) % 360f;
			var nextRotation = Mathf.MoveTowardsAngle(rb.rotation.eulerAngles.y, targetRotation, 90f * Time.fixedDeltaTime);
			rb.MoveRotation(Quaternion.Euler(new Vector3(0, nextRotation, 0)));

			if (nextRotation == targetRotation) {
				timer = 0;
				startingRotation = startingRotation + 90f;
			}
			return;
		}

		rb.MovePosition(transform.position + (transform.forward * Time.fixedDeltaTime));
	}

}