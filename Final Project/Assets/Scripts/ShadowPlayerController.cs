using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerController : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameController gameController;
    [SerializeField] private PlayerBehavior playerBehavior;
    private Rigidbody2D shadowRb2D;
    private Transform shadowRb2DT;

    // Shadow Player Movement
    [SerializeField] private bool isClimbing = false;
    [SerializeField] private bool isGrounded = false;


    // Time travel stuff
    [SerializeField] public bool recordingMovements = false;
    [SerializeField] public bool replayingMovements = false;
    private bool startOrStopTimeTravel = true;
    private List<Vector2> movements = new List<Vector2>();
    private List<double> movement_times = new List<double>();
    private int index_mvmnt = 1;



    // Start is called before the first frame update
    void Start()
    {
        shadowRb2D = GetComponent<Rigidbody2D>();
        shadowRb2DT = GetComponent<Transform>();

        // Double check if the GameController.cs is not attached to the player in Unity
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!playerBehavior)
            playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        // Right now the time travel button is Return, can be changed later
        // Time Travel Controller logic right here
        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (startOrStopTimeTravel) // if it's the first time pressing the time travel button, which will trigger the recording FOR the shadow
            {

                BeginTimeTravel();

                //Debug.Log("Begin Recording...");

                startOrStopTimeTravel = !startOrStopTimeTravel;
            }

            else // If it's the second time hitting the time travel button, which will trigger the replay OF the shadow
            {
                // Stop recording
                recordingMovements = false;


                // Move the shadow to the initial position
                transform.position = movements[0];

                // Start the replay
                // DO

                replayingMovements = true;

                startOrStopTimeTravel = !startOrStopTimeTravel;
            }
        }
    }

    private void FixedUpdate()
    {
        if (recordingMovements)
            RecordMovements();

        else if (replayingMovements)
            ReplayMovements();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
            isGrounded = true;
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -2, 0);
            isClimbing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
            isGrounded = false;
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
            isClimbing = false;
        }
    }

    // This is the first function called when the time travel RECORDING phase is initiated
    //  It will record the initial position of the PLAYER and set the relative time to 0
    public void BeginTimeTravel()
    {
        // Record the initial position and time tied to it
        movements.Add(playerBehavior.rb2DT.position);
        movement_times.Add(Time.time);

        // Start recording the player movements by setting this to true
        // This will have an effect in FixedUpdate()
        recordingMovements = true;

    }

    private void RecordMovements()
    {
        movements.Add(playerBehavior.rb2DT.position);
        movement_times.Add(Time.time);
    }

    private void ReplayMovements()
    {
        if (movements.Count > index_mvmnt + 1)
        {

            // Simulate PlayerBehavior.Move() with a quick calculation
            float x = movements[index_mvmnt].x - movements[index_mvmnt - 1].x;
            if (x < 0)
                x = -1;
            else if (x > 0)
                x = 1;

            float y = movements[index_mvmnt].y - movements[index_mvmnt - 1].y;
            if (y > 0.0099f && isGrounded)
            {
                shadowRb2D.velocity = new Vector2(shadowRb2D.velocity.x, gameController.jumpIntensity);
                Debug.Log(y);
            }
            shadowRb2D.velocity = new Vector2(x * gameController.playerSpeed, shadowRb2D.velocity.y);


            index_mvmnt += 1;
        }
        else
        {
            // Reset the shadow player and his place
            shadowRb2D.velocity = new Vector2(0, 0);
            shadowRb2DT.position = new Vector2(-3, 1);

            // Reset Game Logic
            movements.Clear();
            movement_times.Clear();
            index_mvmnt = 1;
            replayingMovements = false;
        }

    }

}
