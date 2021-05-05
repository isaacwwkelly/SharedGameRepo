using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject newShadowPlayer;

    // for player movement
    public Rigidbody2D rb2D;
    public Transform rb2DT;
    [SerializeField] public bool isGrounded;
    [SerializeField] private bool isClimbing = false;
    [SerializeField] public GameObject jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2DT = GetComponent<Transform>();

        // Double check if the GameController.cs is not attached to the player in Unity
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!newShadowPlayer)
            newShadowPlayer = GameObject.FindGameObjectWithTag("shadowPlayer");

    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        Move();
        Jump();
        // Disable movement whenever the game is over
        if (game_Manager.gameEnded)
        {
            this.enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * gameController.playerSpeed; 
        rb2D.velocity = new Vector2(moveBy, rb2D.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (transform.parent != null)
                transform.parent = null;

            if (isGrounded || (isGrounded && isClimbing))
            {
                AudioSource s = jumpSound.GetComponent<AudioSource>();
                s.Play();
                rb2D.velocity = new Vector2(rb2D.velocity.x, gameController.jumpIntensity);

            }
            else if (isClimbing)
            {

                rb2D.velocity = new Vector2(rb2D.velocity.x, Input.GetAxisRaw("Vertical") * gameController.climbIntensity);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "button")
            isGrounded = true;
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -2, 0);
            isClimbing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "button")
            isGrounded = false;
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
            isClimbing = false;
        }
    }
}
