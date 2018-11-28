using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerSnail : MonoBehaviour
{

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
    bool gameisover;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TS = GetComponent<TrailSystem>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Salt")
        {
            slowed = true;
        }
        // Updated upstream
        else if (col.gameObject.tag == "Lettuce")
        {
            Destroy(col.gameObject);
        }


        // Stashed changes
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Salt")
        {
            slowed = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ennemy")
        {
            StartCoroutine(die());
           
        }
        if (collision.gameObject.name == "BlueClinderFX")
        {
            onVictory(Victory1);
            onSnail(Snail);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameisover) {
        Reset();
        onSnail(Snail);
        onGameOver(GameOver);
        }
        if (available)
        {
          
            if (Input.GetButtonDown("Jump"))
            {
                available = false;
                TS.enabled = false;
                animator.SetInteger("state", 2);
                StartCoroutine(rollingMovement());
            }
            else if ((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0))
            {
                animator.SetInteger("state", 1);
                if (!slowed) { normalMovement(); }
                else { slowedMovement(); }

            }
            else 
            {
                animator.SetInteger("state", 0);
            }
            
        }
       
    }

    void normalMovement()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, x, 0);
        //rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + z));
        transform.Translate(0, 0, z);
    }
    IEnumerator rollingMovement()
    {
        float elapsedTime = 0f;
        while (elapsedTime < rollingDuration)
        {
            elapsedTime += Time.deltaTime;
            var z = Time.deltaTime * movementSpeed * 1.5f;
            transform.Translate(0, 0, z);
            yield return null;
        }
        available = true;
        TS.enabled = true;

    }
    void slowedMovement()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed * slowCoefficient;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
   IEnumerator die()
    {
        available = false;
        gameObject.GetComponent<AudioSource>().Play();
        animator.SetBool("isDead", true);
        yield return null;
        animator.SetBool("wasDead", true);
        yield return new WaitForSeconds(3);
        Reset();
        onSnail(Snail);
        onGameOver(GameOver);







        //Time.timeScale = 0f;

    }
    void onSnail(GameObject Snail)
    {
        if (Snail.gameObject.name == "Snail")
        {
            Snail.SetActive(false);
            gameisover = true;


        }
    }
    void onGameOver(GameObject GameOver)
    {
        if (GameOver.gameObject.name == "GameOver")
            GameOver.SetActive(true);
        gameisover = true;


    }

    void onVictory(GameObject Victory1)
    {
        if (Victory1.gameObject.name == "Victory1")
            Victory1.SetActive(true);
        gameisover = true;
    }
   void Reset()
    {
        
        
           if (Input.GetKeyDown(KeyCode.R))
            {
                
                int index = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(index);
            }
        gameisover = true;
        }
    }


   

