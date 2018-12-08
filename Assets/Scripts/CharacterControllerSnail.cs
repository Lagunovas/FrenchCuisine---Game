using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerSnail : MonoBehaviour {

	public float movementSpeed = 3f;
	public float rotationSpeed = 150f;
	public float rollingDuration = 0.7f;
	public float slowCoefficient = 0.7f;
	public GameObject GameOver;
	public GameObject Victory1;
	private bool slowed = false;
	public Animator animator;
	public bool available = true;
	public Rigidbody rb;
	public GameObject Snail;
	private TrailSystem TS;
	bool gameOver;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		TS = GetComponent<TrailSystem>();
	}

	private void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Salt") {
			slowed = true;
		}

		// Updated upstream
		else if (col.gameObject.tag == "Lettuce") {
			GameState.IncrementScore();
			Destroy(col.gameObject);
		}
	}

	private void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Salt") {
			slowed = false;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "ennemy") {
			StartCoroutine(Die());
		}

		if (collision.gameObject.name == "BlueClinderFX") {
			OnVictory(Victory1);
			OnSnail(Snail);
		}
	}

	private void FixedUpdate() {
		if (gameOver) {
			Reset();
			OnSnail(Snail);
			OnGameOver(GameOver);
		}

		if (available) {
			if (Input.GetButtonDown("Jump")) {
				available = false;
				TS.enabled = false;
				animator.SetInteger("state", 2);
				StartCoroutine(RollingMovement());
			} else if ((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0)) {
				animator.SetInteger("state", 1);
				if (!slowed) {
					NormalMovement();
				} else {
					SlowedMovement();
				}
			} else {
				animator.SetInteger("state", 0);
			}
		}
	}

	private void NormalMovement() {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}

	IEnumerator RollingMovement() {
		float elapsedTime = 0f;
		while (elapsedTime < rollingDuration) {
			elapsedTime += Time.deltaTime;
			var z = Time.deltaTime * movementSpeed * 1.5f;
			transform.Translate(0, 0, z);
			yield return null;
		}
		available = true;
		TS.enabled = true;
	}

	private void SlowedMovement() {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed * slowCoefficient;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}

	IEnumerator Die() {
		available = false;
		gameObject.GetComponent<AudioSource>().Play();
		animator.SetBool("isDead", true);
		yield return null;
		animator.SetBool("wasDead", true);
		yield return new WaitForSeconds(3);
		Reset();
		OnSnail(Snail);
		OnGameOver(GameOver);
	}

	private void OnSnail(GameObject Snail) {
		if (Snail.gameObject.name == "Snail") {
			Snail.SetActive(false);
			gameOver = true;
		}
	}

	private void OnGameOver(GameObject GameOver) {
		if (GameOver.gameObject.name == "GameOver") {
			GameOver.SetActive(true);
		}
		gameOver = true;
	}

	private void OnVictory(GameObject Victory1) {
		if (Victory1.gameObject.name == "Victory1") {
			Victory1.SetActive(true);
		}
		gameOver = true;
	}

	private void Reset() {
		if (Input.GetKeyDown(KeyCode.R)) {
			int index = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(index);
		}
		gameOver = true;
	}

}