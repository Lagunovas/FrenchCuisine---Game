using UnityEngine;

public class SnailMenu : MonoBehaviour {

	public float movementSpeed = 3f;

	public Animator animator;

	private void Update() {
		animator.SetInteger("state", 1);
		NormalMovement();
	}

	private void NormalMovement() {
		var z = Time.deltaTime * movementSpeed;

		transform.Translate(0, 0, z);
		if (transform.position.z >= 30) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -30);
		}
	}

}